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
    
    public partial class Relationship
    {
        public long ID { get; set; }
        public long LabelGroupID { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<long> IsPublished { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    }
}
