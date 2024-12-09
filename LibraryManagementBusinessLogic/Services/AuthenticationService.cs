//using System;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using LibraryManagementData.DataModel;
//using LibraryManagementViewModel;

//namespace LibraryManagementService
//{
//    public class AuthenticationService
//    {
//        private readonly LibraryEntities _dbContext;

//        public AuthenticationService(LibraryEntities dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public bool Login(StaffModel staff)
//        {
//            var user = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email && u.password == staff.password);

//            if (user != null)
//            {
//                // Set authentication cookie or session here
//                return true;
//            }

//            return false;
//        }

//        public bool Signup(StaffModel staff)
//        {
//            var existingUser = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

//            if (existingUser != null)
//            {
//                return false;
//            }

//            var newUser = new Staff
//            {
//                staff_id = staff.staffId,
//                name = staff.name,
//                email = staff.email,
//                phone = staff.phone,
//                password = staff.password
//            };

//            _dbContext.Staffs.Add(newUser);
//            _dbContext.SaveChanges();
//            return true;
//        }

//        public void Logout()
//        {
//            // Clear authentication cookie or session here
//        }
//    }
//}

using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using LibraryManagementData.DataModel;
using LibraryManagementViewModel;

namespace LibraryManagementService
{
    public class AuthenticationService
    {
        private readonly LibraryEntities _dbContext;

        // Initialize the encryption key and IV
        private byte[] _key = Encoding.UTF8.GetBytes("1234567890123456");
        private byte[] _iv = Encoding.UTF8.GetBytes("abcdefghijklmnop");

        public AuthenticationService(LibraryEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Login(StaffModel staff)
        {
            var user = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

            if (user != null)
            {
                // Decrypt the password and compare it to the user input
                var decryptedPassword = Decrypt(user.password);
                if (staff.password == decryptedPassword)
                {
                    // Set authentication cookie or session here
                    SendEmail(user.email, "Staff LoggedIn Successfully", $"Dear {user.name},\n\nYou have been logged in  to our library system.\n\nThank you!");
                    return true;

                }
                
            }

            return false;
        }

        public bool Signup(StaffModel staff)
        {
            var existingUser = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

            if (existingUser != null)
            {
                return false;
            }

            // Encrypt the password before saving it to the database
            var encryptedPassword = Encrypt(staff.password);

            var newUser = new Staff
            {
                //staff_id = staff.staffId,
                name = staff.name,
                email = staff.email,
                phone = staff.phone,
                password = encryptedPassword
            };

            _dbContext.Staffs.Add(newUser);
            _dbContext.SaveChanges();
            SendEmail(staff.email, "Staff Added Successfully", $"Dear {staff.name},\n\nYou have been added as a Staff to our library system.\n\nThank you!");
            return true;
        }

        public void Logout()
        {
            // Clear authentication cookie or session here
            
        }

        // Encrypt a string using AES encryption
        private string Encrypt(string input)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = _key;
                aes.IV = _iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        // Decrypt a string using AES encryption
        private string Decrypt(string input)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = _key;
                aes.IV = _iv;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] inputBytes = Convert.FromBase64String(input);
                byte[] decryptedBytes = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }

        //email sending function
        public void SendEmail(string toAddress, string subject, string body)
        {
            string fromAddress = "cmind488@gmail.com"; // Replace with your email address
            string password = "lbajohpwvcypmrxw"; // Replace with your email password

            MailMessage message = new MailMessage(fromAddress, toAddress, subject, body);
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587); // Replace with your email provider's SMTP server and port number

            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(fromAddress, password);

            smtpClient.Send(message);
        }
    }
}


//using System;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using LibraryManagementData.DataModel;
//using LibraryManagementViewModel;

//namespace LibraryManagementService
//{
//    public class AuthenticationService
//    {
//        private const string PasswordHash = "JZvM8hOwTW3f3/JroaNS9X1JbbErhG2fOQjxRb5InPQ="; // Replace with your own password hash
//        private const string PasswordSalt = "LdNQ6UtrZc9Xv/8IHjWq3A=="; // Replace with your own password salt

//        private readonly LibraryEntities _dbContext;

//        public AuthenticationService(LibraryEntities dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public bool Login(StaffModel staff)
//        {
//            var user = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

//            if (user != null)
//            {
//                // Verify the password
//                var passwordHash = GetPasswordHash(staff.password, user.PasswordSalt);
//                if (passwordHash == user.PasswordHash)
//                {
//                    // Set authentication cookie or session here
//                    return true;
//                }
//            }

//            return false;
//        }

//        public bool Signup(StaffModel staff)
//        {
//            var existingUser = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

//            if (existingUser != null)
//            {
//                return false;
//            }

//            // Generate salt and hash
//            var passwordSalt = GeneratePasswordSalt();
//            var passwordHash = GetPasswordHash(staff.password, passwordSalt);

//            var newUser = new Staff
//            {
//                staff_id = staff.staffId,
//                name = staff.name,
//                email = staff.email,
//                phone = staff.phone,
//                PasswordHash = passwordHash,
//                PasswordSalt = passwordSalt
//            };

//            _dbContext.Staffs.Add(newUser);
//            _dbContext.SaveChanges();
//            return true;
//        }

//        public void Logout()
//        {
//            // Clear authentication cookie or session here
//        }

//        private string GetPasswordHash(string password, string salt)
//        {
//            var saltedPassword = password + salt;
//            var sha256 = SHA256.Create();
//            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
//            return Convert.ToBase64String(hashBytes);
//        }

//        private string GeneratePasswordSalt()
//        {
//            var saltBytes = new byte[16];
//            using (var rng = new RNGCryptoServiceProvider())
//            {
//                rng.GetBytes(saltBytes);
//            }
//            return Convert.ToBase64String(saltBytes);
//        }
//    }
//}





//using System.Security.Cryptography;
//using System.Text;
//using System.Linq;
//using LibraryManagementData.DataModel;
//using LibraryManagementViewModel;
//using System;

//namespace LibraryManagementService
//{
//    public class AuthenticationService
//    {
//        private readonly LibraryEntities _dbContext;

//        public AuthenticationService(LibraryEntities dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public bool Login(StaffModel staff)
//        {
//            var user = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

//            if (user != null && VerifyPassword(staff.password, user.password))
//            {
//                // Set authentication cookie or session here
//                return true;
//            }

//            return false;
//        }

//        public bool Signup(StaffModel staff)
//        {
//            var existingUser = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

//            if (existingUser != null)
//            {
//                return false;
//            }

//            var newUser = new Staff
//            {
//                staff_id = staff.staffId,
//                name = staff.name,
//                email = staff.email,
//                phone = staff.phone,
//                password = HashPassword(staff.password)
//            };

//            _dbContext.Staffs.Add(newUser);
//            _dbContext.SaveChanges();
//            return true;
//        }

//        public void Logout()
//        {
//            // Clear authentication cookie or session here
//        }

//        //private string HashPassword(string password)
//        //{
//        //    using (var sha256 = SHA256.Create())
//        //    {
//        //        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//        //        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
//        //    }
//        //}


//        private string HashPassword(string password)
//        {
//            if (string.IsNullOrEmpty(password))
//            {
//                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
//            }

//            using (var sha256 = SHA256.Create())
//            {
//                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//                return Convert.ToBase64String(hashedBytes);
//            }
//        }
//        //private bool VerifyPassword(string enteredPassword, string storedPassword)
//        //{
//        //    return HashPassword(enteredPassword) == storedPassword;
//        //}

//        private bool VerifyPassword(string enteredPassword, string storedPassword)
//        {
//            if (string.IsNullOrEmpty(enteredPassword))
//            {
//                return false;
//            }

//            using (var sha256 = SHA256.Create())
//            {
//                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
//                var enteredPasswordHash = Convert.ToBase64String(hashedBytes);
//                return enteredPasswordHash == storedPassword;
//            }
//        }
//    }
//}





//using System.Security.Cryptography;
//using System.Text;
//using System.Linq;
//using LibraryManagementData.DataModel;
//using LibraryManagementViewModel;
//using System;
//using System.Web.Security;

//namespace LibraryManagementService
//{
//    public class AuthenticationService
//    {
//        private readonly LibraryEntities _dbContext;

//        public AuthenticationService(LibraryEntities dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        //public bool Login(StaffModel staff)
//        //{
//        //    var user = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

//        //    if (user != null && VerifyPassword(staff.password, user.password))
//        //    {
//        //        // Set authentication cookie or session here
//        //        return true;
//        //    }

//        //    return false;
//        //}

//        //The new method for Login:

//        public bool Login(StaffModel staff)
//        {
//            var user = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

//            if (user != null && VerifyPassword(staff.password, user.password))
//            {
//                FormsAuthentication.SetAuthCookie(user.email, false); // Set authentication cookie with user email
//                return true;
//            }

//            return false;
//        }


//        public bool Signup(StaffModel staff)
//        {
//            var existingUser = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

//            if (existingUser != null)
//            {
//                return false;
//            }

//            var newUser = new Staff
//            {
//                staff_id = staff.staffId,
//                name = staff.name,
//                email = staff.email,
//                phone = staff.phone,
//                password = HashPassword(staff.password)
//            };

//            _dbContext.Staffs.Add(newUser);
//            _dbContext.SaveChanges();
//            return true;
//        }

//        public void Logout()
//        {
//            // Clear authentication cookie or session here
//        }

//        private string HashPassword(string password)
//        {
//            if (string.IsNullOrEmpty(password))
//            {
//                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
//            }

//            using (var sha256 = SHA256.Create())
//            {
//                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//                return Convert.ToBase64String(hashedBytes);
//            }
//        }

//        private bool VerifyPassword(string enteredPassword, string storedPassword)
//        {
//            if (string.IsNullOrEmpty(enteredPassword))
//            {
//                throw new ArgumentNullException(nameof(enteredPassword), "Password cannot be null or empty.");
//            }

//            if (string.IsNullOrEmpty(storedPassword))
//            {
//                throw new ArgumentNullException(nameof(storedPassword), "Stored password cannot be null or empty.");
//            }

//            using (var sha256 = SHA256.Create())
//            {
//                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
//                var hashedEnteredPassword = Convert.ToBase64String(hashedBytes);
//                return hashedEnteredPassword == storedPassword;
//            }
//        }
//    }
//}


////////////////////////////////////////////New code to check sessions///////////////////////////////////////////////////

//using LibraryManagementData.DataModel;
//using LibraryManagementViewModel;
//using System;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web;
//using System.Web.Security;

//namespace LibraryManagementService
//{
//    public class AuthenticationService
//    {
//        private readonly LibraryEntities _dbContext;

//        public AuthenticationService(LibraryEntities dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public bool Login(StaffModel staff)
//        {
//            var user = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

//            if (user != null && VerifyPassword(staff.password, user.password))
//            {
//                var authTicket = new FormsAuthenticationTicket(
//                    1,
//                    user.email,
//                    DateTime.Now,
//                    DateTime.Now.AddMinutes(20),
//                    false,
//                    user.staff_id.ToString());

//                var encryptedTicket = FormsAuthentication.Encrypt(authTicket);

//                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
//                HttpContext.Current.Response.Cookies.Add(authCookie);

//                return true;
//            }

//            return false;
//        }

//        public bool Signup(StaffModel staff)
//        {
//            var existingUser = _dbContext.Staffs.FirstOrDefault(u => u.email == staff.email);

//            if (existingUser != null)
//            {
//                return false;
//            }

//            var newUser = new Staff
//            {
//                staff_id = staff.staffId,
//                name = staff.name,
//                email = staff.email,
//                phone = staff.phone,
//                password = HashPassword(staff.password)
//            };

//            _dbContext.Staffs.Add(newUser);
//            _dbContext.SaveChanges();
//            return true;
//        }

//        public void Logout()
//        {
//            var httpCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
//            if (httpCookie != null)
//            {
//                httpCookie.Expires = DateTime.Now.AddDays(-1);
//                HttpContext.Current.Response.Cookies.Add(httpCookie);
//            }
//        }

//        private string HashPassword(string password)
//        {
//            if (string.IsNullOrEmpty(password))
//            {
//                throw new ArgumentNullException(nameof(password), "Password cannot be null or empty.");
//            }

//            using (var sha256 = SHA256.Create())
//            {
//                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
//                return Convert.ToBase64String(hashedBytes);
//            }
//        }

//        private bool VerifyPassword(string enteredPassword, string storedPassword)
//        {
//            if (string.IsNullOrEmpty(enteredPassword))
//            {
//                throw new ArgumentNullException(nameof(enteredPassword), "Password cannot be null or empty.");
//            }

//            if (string.IsNullOrEmpty(storedPassword))
//            {
//                throw new ArgumentNullException(nameof(storedPassword), "Stored password cannot be null or empty.");
//            }

//            using (var sha256 = SHA256.Create())
//            {
//                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
//                var hashedEnteredPassword = Convert.ToBase64String(hashedBytes);
//                return hashedEnteredPassword == storedPassword;
//            }
//        }
//    }
//}
