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
    
    public partial class FamilyMember
    {
        public long ID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public long RelationshipSD { get; set; }
        public Nullable<long> GenderSD { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public Nullable<long> BloodTypeSD { get; set; }
        public string MedicationAllergy { get; set; }
        public string Remark { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string CreateUserID { get; set; }
        public string UpdateUserID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
