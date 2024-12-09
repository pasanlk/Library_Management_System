using System;
using System.Web.Mvc;
using LibraryManagementData.DataModel;
using LibraryManagementService;
using LibraryManagementViewModel;

namespace LibraryManagementWebApp.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationService _authenticationService;

        public AuthenticationController()
        {
            _authenticationService = new AuthenticationService(new LibraryEntities());
        }

        public AuthenticationController(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(StaffModel staff)
        {
            if (_authenticationService.Login(staff))
            {
                Session["loggedInUser"] = staff;
                return RedirectToAction("Admin_Pannel", "Home");
            }

            ViewBag.ErrorMessage = "Invalid email or password.";
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(StaffModel staff)
        {
            if (ModelState.IsValid)
            {
                if (_authenticationService.Signup(staff))
                {
                    return RedirectToAction("Login");
                }

                ModelState.AddModelError("Email", "Email already exists.");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Remove("loggedInUser");
            return RedirectToAction("Login", "Authentication");
        }
    }
}



////////new one////////////////////////
//using LibraryManagementData.DataModel;
//using LibraryManagementService;
//using LibraryManagementViewModel;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;

//namespace LibraryManagement.Controllers
//{
//    public class AuthenticationController : Controller
//    {
//        private readonly LibraryEntities _dbContext;
//        private readonly AuthenticationService _authService;

//        public AuthenticationController()
//        {
//            _dbContext = new LibraryEntities();
//            _authService = new AuthenticationService(_dbContext);
//        }

//        [HttpGet]
//        public ActionResult Login()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Login(StaffModel staff)
//        {
//            if (_authService.Login(staff))
//            {
//                return RedirectToAction("Index", "Home");
//            }
//            else
//            {
//                ViewBag.ErrorMessage = "Invalid email or password.";
//                return View();
//            }
//        }

//        [HttpGet]
//        public ActionResult Signup()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Signup(StaffModel staff)
//        {
//            if (_authService.Signup(staff))
//            {
//                return RedirectToAction("Login");
//            }
//            else
//            {
//                ViewBag.ErrorMessage = "Email already exists.";
//                return View();
//            }
//        }

//        [HttpGet]
//        public ActionResult Logout()
//        {
//            _authService.Logout();
//            return RedirectToAction("Login");
//        }
//    }
//}
