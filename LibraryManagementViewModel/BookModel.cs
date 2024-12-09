using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementViewModel
{
    public class BookModel
    {


        [Required(ErrorMessage = "please enter your the ISBN Number Of The Book ")]
        [DisplayName("ISBN Number Of The Book")]
        public int isbnNumber { get; set; }

        [Required(ErrorMessage="please enter your name")]
        [DisplayName("Author Number")]
        public string authNo { get; set; }

        [Required(ErrorMessage = "please enter the Book Title")]
        //[StringLength(50, ErrorMessage = "Please do not enter values over 50 characters")]
        [DisplayName("Title")]
        public string title { get; set; }

        [Required(ErrorMessage = "please enter the book edition")]
        [DisplayName("Book Edition")]
        public string edition { get; set; }

        [Required(ErrorMessage = "please enter the category")]
        [DisplayName("Book Category")]
        public string category { get; set; }

        [Required(ErrorMessage = "please enter your name")]
        [DisplayName("Staff Id")]
        public int? staffId { get; set; }

        [Required(ErrorMessage = "please enter the publisher's id")]
        [DisplayName("Publisher Id")]
        public int? publisherId { get; set; }

        [DisplayName("Release Date")]
        public DateTime? releaseDate { get; set; }

        [DisplayName("Book Price")]
        public double? price { get; set; }

        //Do some research are we need these two
        public BookReservationModel BookReservation { get; set; }
        public virtual PublisherModel Publisher { get; set; }


    }
}
