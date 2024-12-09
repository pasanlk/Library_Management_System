using LibraryManagementBusinessLogic.Facade;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookReservationController : Controller
    {


        private BookReservationFacade _bookReservationFacade;

        //Normaly we dont create Controllers in the views
        public BookReservationController()
        {
            _bookReservationFacade = new BookReservationFacade();
            //Also we can call ViewBags in the Constructor
            //PopulateViewBags();
        }


        // GET: BookReservation
        public ActionResult Index()
        {
            List<BookReservationModel> bookReservations = null;
            //we can create _bookReservationFacade objects in the every action methods 
            //For  using(Using)we have to use Idisposable method
            //using (_bookReservationFacade = new BookReservationFacade())
            //{
            //    bookReservations = _bookReservationFacade.GetAllBookReservations();
            //}
            bookReservations = _bookReservationFacade.GetAllBookReservations();
            return View(bookReservations);
        }


        public ActionResult Create()
        {
            _bookReservationFacade = new BookReservationFacade();

            PopulateViewBags();

            return View();
        }

        // POST: BookReservation/Create
        [HttpPost]
        public ActionResult Create(BookReservationModel bookReservationModel)
        {
            try
            {
                _bookReservationFacade = new BookReservationFacade();
                // TODO: Add insert logic here
                _bookReservationFacade.AddBookReservation(bookReservationModel);
                //call viewbags create method 
                PopulateViewBags();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookReservation/Edit/5
        public ActionResult Edit(int id)
        {
            _bookReservationFacade = new BookReservationFacade();
            var bookReservation = _bookReservationFacade.GetBookReservation(id);
            PopulateViewBags();
            return View(bookReservation);
        }

        // POST: BookReservation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BookReservationModel bookReservationModel)
        {
            try
            {
                _bookReservationFacade = new BookReservationFacade();
                // TODO: Add update logic here
                bookReservationModel.reservationId = id;
                _bookReservationFacade.UpdateBookReservation(bookReservationModel);
                PopulateViewBags();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookReservation/Delete/5
        public ActionResult Delete(int id)
        {
            var bookReservation = _bookReservationFacade.GetBookReservation(id);
            return View(bookReservation);
        }

        // POST: BookReservation/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, BookReservationModel bookReservationModel)
        {
            try
            {
                // TODO: Add delete logic here
                bookReservationModel.reservationId = id;
                _bookReservationFacade.DeleteBookReservation(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //create view bags usinga method
        private void PopulateViewBags()
        {
            ViewBag.ReaderIdList = _bookReservationFacade.GetReaderIdList();
            ViewBag.BookIsbnList = _bookReservationFacade.GetBookIsbnList();
        }
    }
}
