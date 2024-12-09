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
    public class BookReservationFacade /*:IDisposable*/
    {
        public List<BookReservationModel> GetAllBookReservations()
        {
            BookReservationService bookReservationService = new BookReservationService();
            return bookReservationService.GetAllBookReservations();
        }

        public void AddBookReservation(BookReservationModel bookReservationModel)
        {
            BookReservationService bookReservationService = new BookReservationService();
            bookReservationService.AddBookReservation(bookReservationModel);
        }

        public BookReservationModel GetBookReservation(int searchId)
        {
            BookReservationService bookReservationService = new BookReservationService();
            return bookReservationService.GetBookReservation(searchId);
        }

        public void UpdateBookReservation(BookReservationModel newBookReservation)
        {

            BookReservationService bookReservationService = new BookReservationService();
            bookReservationService.UpdateBookReservation(newBookReservation);

        }

        public void DeleteBookReservation(int searchId)
        {
            BookReservationService bookReservationService = new BookReservationService();
            bookReservationService.DeleteBookReservation(searchId);
        }


        public List<SelectListItem> GetReaderIdList()
        {
            BookReservationService bookReservationService = new BookReservationService();
            return bookReservationService.GetReaderIdList();
        }

        public List<SelectListItem> GetBookIsbnList()
        {
            BookReservationService bookReservationService = new BookReservationService();
            return bookReservationService.GetBookIsbnList();
        }

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
