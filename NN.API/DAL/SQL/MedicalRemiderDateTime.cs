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
    
    public partial class MedicalRemiderDateTime
    {
        public long ID { get; set; }
        public long MedicalReminderID { get; set; }
        public Nullable<System.DateTime> RemiderDate { get; set; }
        public System.TimeSpan RemiderTime { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
    
        public virtual MedicalReminder MedicalReminder { get; set; }
    }
}
