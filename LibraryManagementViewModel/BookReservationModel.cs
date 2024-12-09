using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementViewModel
{
    public  class BookReservationModel
    {
        public int reservationId { get; set; }
        public int readerId { get; set; }
        public int isbnNumber { get; set; }
        public DateTime? reserveDate { get; set; }
        public DateTime? dueDate { get; set; }
        public DateTime? returnDate { get; set; }
        public double? fine { get; set; }

        public  BookModel Book { get; set; }
        public  ReaderModel Reader { get; set; }
    }
}
