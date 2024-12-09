using LibraryManagementData.DataModel;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Diagnostics;

namespace LibraryManagementBusinessLogic.Services
{
    public class BookReservationService
    {

        private static LibraryEntities BookReservationDbContext = null;

        public BookReservationService()
        {
            BookReservationDbContext = new LibraryEntities();
        }


        /*
         * All CRUD operations of the Publisher entity
         * BookReservationDbContext
         */
        public void AddBookReservation(BookReservationModel newBookReservation)
        {
            try
            {
                if (newBookReservation != null)
                {
                    Book_Reservation newAddingBookReservation = new Book_Reservation()
                    {
                        //reservation_id = newBookReservation.reservationId,
                        reader_id = newBookReservation.readerId,
                        isbn_number = newBookReservation.isbnNumber,
                        reserve_date = newBookReservation.reserveDate,
                        due_date = newBookReservation.dueDate,
                        return_date = newBookReservation.dueDate,
                        fine = 0,// initialize fine to 0
                    };

                    // calculate the difference between the due date and return date
                    TimeSpan diff = (TimeSpan)(newAddingBookReservation.return_date - newAddingBookReservation.due_date);

                    // if the book is returned after the due date, calculate the fine
                    if (diff > TimeSpan.Zero)
                    {
                        // calculate the number of days late
                        int daysLate = (int)diff.TotalDays;

                        // calculate the fine (e.g. $1 per day late)
                        double fineAmount = daysLate * 1.0;

                        // set the fine in the reservation object
                        newAddingBookReservation.fine = fineAmount;
                    }

                    BookReservationDbContext.Book_Reservation.Add(newAddingBookReservation);
                    BookReservationDbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateBookReservation(BookReservationModel newBookReservation)
        {
            try
            {

                var existBookReservation = BookReservationDbContext.Book_Reservation.Find(newBookReservation.reservationId);
                if (existBookReservation != null)
                {

                    //existBookReservation.reservation_id = newBookReservation.reservationId;
                    existBookReservation.reader_id = newBookReservation.readerId;
                    existBookReservation.isbn_number = newBookReservation.isbnNumber;
                    existBookReservation.reserve_date = newBookReservation.reserveDate;
                    existBookReservation.due_date = newBookReservation.dueDate;
                    existBookReservation.return_date = newBookReservation.returnDate;
                    //existBookReservation.fine = newBookReservation.fine;


                    // Calculate fine based on return date and due date
                    if (existBookReservation.return_date > existBookReservation.due_date)
                    {
                        TimeSpan delay = (TimeSpan)(existBookReservation.return_date - existBookReservation.due_date);
                        double daysDelayed = Math.Ceiling(delay.TotalDays);
                        double fine = daysDelayed * 0.5; // Assuming a fine of 50 cents per day of delay
                        existBookReservation.fine = fine;
                    }
                    else
                    {
                        existBookReservation.fine = 0; // No fine if returned before or on due date
                    }

                    BookReservationDbContext.SaveChanges();





                    //publisherDbContext.Publishers.Append(newUpdatingPublisher);
                    //publisherDbContext.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }


        public BookReservationModel GetBookReservation(int reservationId)
        {
            try
            {
                //return publisherDbContext.Publishers.Find(searchId);

                var bookReservationModel = new BookReservationModel();
                Book_Reservation item = BookReservationDbContext.Book_Reservation.Find(reservationId);

                if (item != null)
                {
                    bookReservationModel.reservationId = item.reservation_id;
                    bookReservationModel.readerId = item.reader_id;
                    bookReservationModel.isbnNumber = item.isbn_number;
                    bookReservationModel.reserveDate = item.reserve_date;
                    bookReservationModel.dueDate = item.due_date;
                    bookReservationModel.returnDate = item.return_date;
                    bookReservationModel.fine = item.fine;

                }

                return bookReservationModel;


            }
            catch (Exception ex) { throw ex; }
        }

        public void DeleteBookReservation(int reservationId)
        {
            try
            {
                Book_Reservation existing = BookReservationDbContext.Book_Reservation.Find(reservationId);
                if (existing != null)
                {
                    BookReservationDbContext.Book_Reservation.Remove(existing);
                    BookReservationDbContext.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public List<BookReservationModel> GetAllBookReservations()
        {
            var BookReservationModelList = new List<BookReservationModel>();
            var BookReservationList = BookReservationDbContext.Book_Reservation.ToList();
            if (BookReservationList != null)
            {
                foreach (var item in BookReservationList)
                {
                    var bookReservationModel = new BookReservationModel();

                    bookReservationModel.reservationId = item.reservation_id;
                    bookReservationModel.readerId = item.reader_id;
                    bookReservationModel.isbnNumber = item.isbn_number;
                    bookReservationModel.reserveDate = item.reserve_date;
                    bookReservationModel.dueDate = item.due_date;
                    bookReservationModel.returnDate = item.return_date;
                    bookReservationModel.fine = item.fine;


                    BookReservationModelList.Add(bookReservationModel);


                }


            }

            return BookReservationModelList;
        }

        public List<SelectListItem> GetReaderIdList()
        {
            var readers = BookReservationDbContext.Readers.Select(p => new SelectListItem { Value = p.reader_id.ToString(), Text = p.first_name }).ToList();
            return readers;
        }

        public List<SelectListItem> GetBookIsbnList()
        {
            var isbnNumbers = BookReservationDbContext.Books.Select(s => new SelectListItem { Value = s.isbn_number.ToString(), Text = s.title }).ToList();
            return isbnNumbers;
        }



    }
}
