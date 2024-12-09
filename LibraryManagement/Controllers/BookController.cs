using LibraryManagementBusinessLogic.Facade;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private BookFacade _bookFacade;

       
        public BookController()
        {
            _bookFacade = new BookFacade();
        }

        // GET: Book
        /// <summary>
        /// The Index() method returns a view containing a list of all the books in the database. 
        /// summery
        public ActionResult Index()
        {
            return View(_bookFacade.GetAllBooks());
        }


        /// <summary>
        /// The Create() method is used to handle HTTP GET request and returns a view to display a 
        /// form to create a new book. It sets ViewBag.PublisherList and ViewBag.StaffList properties 
        /// using the GetPublisherSelectIdList() and GetStaffSelectIdList() methods in the BookFacade.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {

            
            ViewBag.PublisherList = _bookFacade.GetPublisherSelectIdList();
            ViewBag.StaffList = _bookFacade.GetStaffSelectIdList();
            return View();
        }

        // POST: Book/Create
        /// <summary>
        /// The Create(BookModel bookModel) method is used to handle the HTTP POST request
        /// to create a new book. The method receives a BookModel object as a parameter and uses the 
        /// _bookFacade.AddBook(bookModel) method to save the new book to the database.
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        [HttpPost]
        //public ActionResult Create(FormCollection collection)

        public ActionResult Create(BookModel bookModel)
        {



            //"ModelState.IsValid" this is the responsible for the entities relationship
            try
            {



                _bookFacade.AddBook(bookModel);
                ViewBag.PublisherList = _bookFacade.GetPublisherSelectIdList();
                ViewBag.StaffList = _bookFacade.GetStaffSelectIdList();
                return RedirectToAction("Index");


            }
            //still didnt get an proper idea
            catch (Exception ex)
            {
                return View();
            }


        }

        // GET: Book/Edit/5
        /// <summary>
        /// The Edit(int id) method is used to handle HTTP GET request to edit a book. It receives 
        /// an id parameter and returns a view containing a form to edit a specific book. It also sets ViewBag.PublisherList and ViewBag.StaffList properties using 
        /// the GetPublisherSelectIdList() and GetStaffSelectIdList() methods in the BookFacade.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        public ActionResult Edit(int id)
        {
            ViewBag.PublisherList = _bookFacade.GetPublisherSelectIdList();
            ViewBag.StaffList = _bookFacade.GetStaffSelectIdList();
            return View(_bookFacade.GetBook(id));
        }

        /// <summary>
        /// The Edit(int id, BookModel bookModel) method is used to handle HTTP POST request to update an existing book. 
        /// It receives the id of the book to be edited and a BookModel object containing the updated information of the book.
        /// The _bookFacade.UpdateBook(bookModel) method is used to update the book in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        // POST: Book/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, BookModel bookModel)
        {
            try
            {
                // TODO: Add update logic here
                bookModel.isbnNumber = id;
                _bookFacade.UpdateBook(bookModel);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        /// <summary>
        /// The Delete(int id) method is used to handle HTTP GET request to delete a book.
        /// It receives the id of the book to be deleted and returns a view to confirm the deletion.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            return View(_bookFacade.GetBook(id));
        }

        // POST: Book/Delete/5
        /// <summary>
        /// The DeleteConfirmed(int id) method is used to handle the HTTP POST request to delete a book.
        /// It receives the id of the book to be deleted and uses the _bookFacade.DeleteBook(id) method to delete the book from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here
                _bookFacade.DeleteBook(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
