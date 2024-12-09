using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementViewModel
{
    public class ReportModel
    {
        public int regNo { get; set; }
        public int staffId { get; set; }
        public int readerId { get; set; }
        public int bookNo { get; set; }
        public DateTime issueDate { get; set; }
        public DateTime returnDate { get; set; }

        public  ReaderModel Reader { get; set; }
        public StaffModel Staff { get; set; }
    }
}
