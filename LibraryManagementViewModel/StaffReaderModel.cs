using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementViewModel
{
    public class StaffReaderModel
    {
        public int staffReaderId { get; set; }
        public int staffId { get; set; }
        public int  readerId { get; set; }

        public ReaderModel Reader { get; set; }
        public  StaffModel Staff { get; set; }
    }
}
