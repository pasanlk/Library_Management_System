using LibraryManagementBusinessLogic.Facade;
using LibraryManagementBusinessLogic.Services;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class PublisherController : Controller
    {



        private PublisherFacade _publisherFacade;

        public PublisherController()
        {
            _publisherFacade = new PublisherFacade();
        }



        // GET: Publisher
        public ActionResult Index()
        {
            var publishers = _publisherFacade.GetAllPublishers();
            return View(publishers);
        }

        

        // GET: Publisher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publisher/Create
        [HttpPost]
        public ActionResult Create(PublisherModel publisher)
        {
            try
            {
                // TODO: Add insert logic here

                _publisherFacade.AddPublisher(publisher);
                return RedirectToAction("Index"); ;
            }
            catch
            {
                return View();
            }
        }

        // GET: Publisher/Edit/5
        public ActionResult Edit(int id)
        {
            var publisher = _publisherFacade.GetPublisher(id);
            return View(publisher);
        }

        // POST: Publisher/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PublisherModel publisher)
        {
            try
            {
                // TODO: Add update logic here
                publisher.publisherId = id;
                _publisherFacade.updatePublisher(publisher);
                return RedirectToAction("Index");

                
            }
            catch
            {
                return View();
            }
        }

        //GET: Publisher/Delete/5
        public ActionResult Delete(int id)
        {
            var publisher = _publisherFacade.GetPublisher(id);
            return View(publisher);
        }

        // POST: Publisher/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, PublisherModel publisherModel)
        {
            try
            {
                // TODO: Add delete logic here
                _publisherFacade.DeletePublisher(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
