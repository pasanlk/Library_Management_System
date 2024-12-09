using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementViewModel
{
    public class StaffModel
    {
        
        public int staffId { get; set; }
        public string name { get; set; }
        public string email { get; set; }

        public string phone { get; set; }
        public string password { get; set; }

        




        public BookModel books { get; set; }

        public ReportModel reports { get; set; }
        
        public StaffReaderModel staffReader { get; set; }
    }
}

