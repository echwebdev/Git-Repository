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
    
    public partial class SeededData
    {
        public long ID { get; set; }
        public int Priority { get; set; }
        public long SeededDataGroupID { get; set; }
        public long LabelGroupID { get; set; }
        public long StatusFlag { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    
        public virtual LabelGroup LabelGroup { get; set; }
        public virtual SeededDataGroup SeededDataGroup { get; set; }
    }
}