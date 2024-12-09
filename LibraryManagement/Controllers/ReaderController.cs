using LibraryManagementBusinessLogic.Facade;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class ReaderController : Controller
    {

        private ReaderFacade _readerFacade;

        public ReaderController()
        {
            _readerFacade = new ReaderFacade();
        }
        // GET: Reader
        public ActionResult Index()
        {
            var readers = _readerFacade.GetAllReaders();
            return View(readers);
        }

        // GET: Reader/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Reader/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reader/Create
        [HttpPost]
        public ActionResult Create(ReaderModel readerModel)
        {
            try
            {
                // TODO: Add insert logic here
                _readerFacade.AddReader(readerModel);

                return RedirectToAction("Index");
            }
            
            catch
            {
                return View();
            }
        }

        // GET: Reader/Edit/5
        public ActionResult Edit(int id)
        {
            var reader = _readerFacade.GetReader(id);
            return View(reader);
        }

        // POST: Reader/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, ReaderModel readerModel)
        {
            try
            {
                // TODO: Add update logic here
                readerModel.readerId = id;
                _readerFacade.updateReader(readerModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reader/Delete/5
        public ActionResult Delete(int id)
        {
            var reader = _readerFacade.GetReader(id);
            return View(reader);
        }

        // POST: Reader/Delete/5
        [HttpPost,ActionName("Delete")]
        public ActionResult Delete(int id, ReaderModel readerModel)
        {
            try
            {
                // TODO: Add delete logic here
                _readerFacade.DeleteReader(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
