//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibraryManagementData.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report
    {
        public int reg_no { get; set; }
        public int staff_id { get; set; }
        public int reader_id { get; set; }
        public Nullable<int> book_no { get; set; }
        public Nullable<System.DateTime> issue_date { get; set; }
        public Nullable<System.DateTime> return_date { get; set; }
    
        public virtual Reader Reader { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
