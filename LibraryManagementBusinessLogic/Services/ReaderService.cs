using LibraryManagementData.DataModel;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementBusinessLogic.Services
{
    public class ReaderService
    {

        private static LibraryEntities readerDbContext = null;

        public ReaderService()
        {
            readerDbContext = new LibraryEntities();
        }


        /*
         * All CRUD operations of the Reader entity
         */
        public void AddReader(ReaderModel newReader)
        {
            try
            {
                if (newReader != null)
                {
                    Reader newAddingReader = new Reader()
                    {
                        //reader_id = newReader.readerId,
                        first_name = newReader.firstName,
                        last_name = newReader.lastName,
                        email = newReader.email,
                        address = newReader.address,
                        phone = newReader.phone,

                    };
                    readerDbContext.Readers.Add(newAddingReader);
                    readerDbContext.SaveChanges();
                    SendEmail(newReader.email, "Reader Added", $"Dear {newReader.firstName},\n\nYou have been added as a reader to our library system.\n\nThank you!");


                }
               
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateReader(ReaderModel newReader)
        {
            try { 


                var existReader = readerDbContext.Readers.Find(newReader.readerId);
                if (existReader != null)
                {

                    //existReader.reader_id = newReader.readerId;
                    existReader.first_name = newReader.firstName;
                    existReader.last_name = newReader.lastName;
                    existReader.email = newReader.email;
                    existReader.address = newReader.address;
                    existReader.phone = newReader.phone;

                    
                    //readerDbContext.Readers.Append(UpdatingReader);
                    readerDbContext.SaveChanges();
                    SendEmail(newReader.email, "Reader Updated", $"Dear {newReader.firstName},\n\nYour information has been updated in our library system.\n\nThank you!");


                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }

        public ReaderModel GetReader(int searchId)
        {
            try
            {
                //return readerDbContext.Readers.Find(searchId);

                var readerModel = new ReaderModel();
                var item = readerDbContext.Readers.Find(searchId);
                
                if(item != null)
                {
                    readerModel.readerId = item.reader_id;
                    readerModel.firstName = item.first_name;
                    readerModel.lastName=item.last_name;
                    readerModel.email=item.email;
                    readerModel.address = item.address;
                    readerModel.phone = item.phone; 

                }

                return readerModel;


            }catch(Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteReader(int searchId) {
            try
            {
                Reader existing = readerDbContext.Readers.Find(searchId);
                if(existing != null)
                {
                    readerDbContext.Readers.Remove(existing);
                    readerDbContext.SaveChanges();
                    SendEmail(existing.email, "Reader Removed", $"Dear reader,\n\nYour account has been deleted from our library system.\n\nThank you!");
                }
                

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public List<ReaderModel> GetAllReaders()
        {
            List<ReaderModel> readerModelList= new List<ReaderModel>();
            var readerList = readerDbContext.Readers.ToList();

            

            if(readerList != null)
            {
                foreach(var item in readerList)
                {
                    var readerModel = new ReaderModel();

                    readerModel.readerId = item.reader_id;
                    readerModel.firstName = item.first_name;
                    readerModel.lastName = item.last_name;
                    readerModel.email = item.email;
                    readerModel.address = item.address;
                    readerModel.phone = item.phone;

                    readerModelList.Add(readerModel);
                }
            }

            return readerModelList;
            
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
