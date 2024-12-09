using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementViewModel
{
    public class PublisherModel
    {
        public int publisherId { get; set; }
        public string name { get; set; }
        public string yearOfPublication { get; set; }

        public BookModel Books { get; set; }

    }
}
