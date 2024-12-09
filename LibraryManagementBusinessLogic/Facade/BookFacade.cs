using LibraryManagementBusinessLogic.Services;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraryManagementBusinessLogic.Facade
{
    public  class BookFacade
    {
        public List<BookModel> GetAllBooks()
        {
            BookService bookService = new BookService();
            return bookService.GetAllBooks();

            
        }
        public BookModel GetBook(int searchId) {
            BookService bookService=new BookService();
            return bookService.GetBook(searchId);
        }

        public void AddBook(BookModel bookModel)
        {
            BookService bookService = new BookService();
            bookService.AddBook(bookModel);
        }

        public void UpdateBook(BookModel newBook) {
            BookService bookService = new BookService();  
            bookService.UpdateBook(newBook);

        }

        public void DeleteBook(int id)
        {
            BookService bookService = new BookService();
            bookService.DeleteBook(id);
        }

        public List<SelectListItem> GetPublisherSelectIdList()
        {
            BookService bookService = new BookService();
            return bookService.GetPublisherSelectIdList();
        }

        public List<SelectListItem> GetStaffSelectIdList() {
            BookService bookService = new BookService();
            return bookService.GetStaffSelectIdList();
        }
    }
}
