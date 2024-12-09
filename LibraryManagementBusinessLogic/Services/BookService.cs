using LibraryManagementData.DataModel;
using LibraryManagementViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace LibraryManagementBusinessLogic.Services
{
    public class BookService
    {

        //here we create a object from the LibrarySystemEntities class and assign it to null
        public static LibraryEntities bookDbContext = null;

        /// <summary>
        /// This is necessary because it creates a new session with the database, which is required to perform any operations on the data.
        /// Without this initialization, any attempts to access the database would result in a NullReferenceException because bookDbContext would be null.
        /// </summary>
        public BookService()
        {

            bookDbContext = new LibraryEntities();

        }


        public void AddBook(BookModel newBook)
        {

            try
            {

                if (newBook != null)
                {

                    //this is not actuaaly nessesary
                    //Publisher bookPublisher = new Publisher();
                    //bookPublisher.publisher_id = newBook.publisherId.Value;
                    //bookPublisher.name = newBook.Publisher.name;


                    //create a new object from the book class in the data model folder and assign values to it via the bookmodel
                    Book newAddingBook = new LibraryManagementData.DataModel.Book()
                    {
                        //isbn_number = newBook.isbnNumber,
                        title = newBook.title,
                        authno = newBook.authNo,
                        edition = newBook.edition,
                        category = newBook.category,
                        staff_id = newBook.staffId,
                        publisher_id = newBook.publisherId,
                        release_date = newBook.releaseDate,
                        price = newBook.price,

                    };


                    /*bookDbContext is a LibrarySystemEntities type object and 
                     * from that we call books's entity's Add method and 
                     * pass the Book type newAddingBook object as a parameter 
                     * to the add method 
                     * After that we call SaveChanges method to save the changes to the entities
                     */
                    bookDbContext.Books.Add(newAddingBook);
                    bookDbContext.SaveChanges();
                }





            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                var x = 5;
            }








        }

        public void UpdateBook(BookModel newBook)
        {

            try
            {
                //Publisher bookPublisher = new Publisher();
                //bookPublisher.publisher_id = newBook.publisherId.Value;
                //bookPublisher.name = newBook.Publisher.name;


                var existBook = bookDbContext.Books.Find(newBook.isbnNumber);
                if (existBook != null)
                {
                    //existBook.isbn_number = newBook.isbnNumber;
                    existBook.title = newBook.title;
                    existBook.authno = newBook.authNo;
                    existBook.edition = newBook.edition;
                    existBook.category = newBook.category;
                    existBook.staff_id = newBook.staffId;
                    existBook.publisher_id = newBook.publisherId;
                    existBook.release_date = newBook.releaseDate;
                    existBook.price = newBook.price;

                    bookDbContext.SaveChanges();

                }







            }
            catch (Exception ex) { throw ex; }

        }

        public BookModel GetBook(int searchId)
        {
            try
            {
                /*the first method to find the searchId
                 * 
                 * return bookDbContext.books.Find(searchId);
                 * 
                 */



                //The other method to find the  searchId

                /*the FirstOrDefault method use for the Where method return an array of corresponding isbn_numbers and 
                 * this return the first element of it or if the array is empty it returns null
                */
                BookModel bookModel = new BookModel();
                Book item = bookDbContext.Books.Where(a => a.isbn_number == searchId).FirstOrDefault();

                if (item != null)
                {
                    bookModel.price = item.price;
                    bookModel.title = item.title;
                    bookModel.authNo = item.authno;
                    bookModel.edition = item.edition;
                    bookModel.category = item.category;
                    bookModel.staffId = item.staff_id;
                    bookModel.releaseDate = item.release_date;
                    bookModel.publisherId = item.publisher_id;
                    bookModel.isbnNumber = item.isbn_number;

                }
                return bookModel;
            }
            catch (Exception ex) { throw ex; }
        }

        public void DeleteBook(int searchId)
        {
            try
            {
                Book existing = bookDbContext.Books.Find(searchId);

                //check for the corresponding searchId existing or not 
                if (existing != null)
                {
                    bookDbContext.Books.Remove(existing);
                    bookDbContext.SaveChanges();
                }
            }
            catch (Exception ex) { throw ex; }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<BookModel> GetAllBooks()
        {

            List<BookModel> bookModelList = new List<BookModel>();
            List<Book> bookList = bookDbContext.Books.ToList();

            if (bookList != null)
            {
                foreach (var item in bookList)
                {
                    BookModel bookModel = new BookModel();

                    bookModel.price = item.price;
                    bookModel.title = item.title;
                    bookModel.authNo = item.authno;
                    bookModel.edition = item.edition;
                    bookModel.category = item.category;
                    bookModel.staffId = item.staff_id;
                    bookModel.releaseDate = item.release_date;
                    bookModel.publisherId = item.publisher_id;
                    bookModel.isbnNumber = item.isbn_number;

                    bookModelList.Add(bookModel);

                }

            }

            return bookModelList;



        }

        //Geting id methods
        /// <summary>
        /// SelectListItem objects that can be used to populate a dropdown list in a user interface.
        /// The method starts by selecting all publishers from the Publishers table using the Select()
        /// method.For each publisher, a new SelectListItem object is created with the publisher's ID set as
        /// the Value property and the publisher's name set as the Text property.The ToString() method is used
        /// to convert the publisher_id value to a string for the Value property.
        /// Finally, the list of SelectListItem objects is returned to the calling method, which can then use it to populate a dropdown list
        /// /// </summary>
        /// <returns></returns>
        public List<SelectListItem> GetPublisherSelectIdList()
        {
            var publishers = bookDbContext.Publishers.Select(p => new SelectListItem { Value = p.publisher_id.ToString(), Text = p.name }).ToList();
            return publishers;
        }

        public List<SelectListItem> GetStaffSelectIdList()
        {
            var staffMembers = bookDbContext.Staffs.Select(s => new SelectListItem { Value = s.staff_id.ToString(), Text = s.name }).ToList();
            return staffMembers;
        }


    }
}
