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
    
    public partial class MyVaccination
    {
        public long ID { get; set; }
        public string UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long IsStandard { get; set; }
        public Nullable<long> StandardVaccinationID { get; set; }
        public Nullable<System.DateTime> VaccinationDueDate { get; set; }
        public bool IsVaccinated { get; set; }
        public Nullable<System.DateTime> VaccinatedDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string CreateUserID { get; set; }
        public string UpdateUserID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
