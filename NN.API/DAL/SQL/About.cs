//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.SQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class About
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public long CreateUserID { get; set; }
        public Nullable<long> UpdateUserID { get; set; }
    }
}
