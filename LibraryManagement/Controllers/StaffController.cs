using LibraryManagementBusinessLogic.Facade;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class StaffController : Controller
    {

        private StaffFacade _staffFacade;
        public StaffController()
        {
            _staffFacade= new StaffFacade();
        }
        // GET: Staff
        public ActionResult Index()
        {
            return View(_staffFacade.GetAllStaffs());
        }

        // GET: Staff/Details/5
        ////public ActionResult Details(int id)
        ////{
        ////    return View();
        ////}

        // GET: Staff/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        public ActionResult Create(StaffModel staffModel)
        {
            try
            {
                // TODO: Add insert logic here
                
                _staffFacade.AddStaff(staffModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int id)
        {
            var staff= _staffFacade.GetStaffs(id);
            return View(staff);
        }

        // POST: Staff/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, StaffModel staffModel)
        {
            try { 
            

                // TODO: Add update logic here
                staffModel.staffId = id;
                _staffFacade.UpdateStaff(staffModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int id)
        {
            var staff=_staffFacade.GetStaffs(id);
            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                _staffFacade.DeleteStaff(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
