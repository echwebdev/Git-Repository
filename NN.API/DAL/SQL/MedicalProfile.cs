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
    
    public partial class MedicalProfile
    {
        public long Id { get; set; }
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<long> GenderSD { get; set; }
        public Nullable<long> MaritalStatusSD { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<long> BloodTypeSD { get; set; }
        public Nullable<long> WeightSD { get; set; }
        public Nullable<long> HeightSD { get; set; }
        public string Occupation { get; set; }
        public Nullable<bool> HaveInsurance { get; set; }
        public Nullable<bool> HaveNSSF { get; set; }
        public Nullable<bool> Smoker { get; set; }
        public Nullable<bool> HaveChildren { get; set; }
        public Nullable<bool> CaffeineDrinker { get; set; }
        public string CaffeinePerDay { get; set; }
        public Nullable<bool> TakeMedication { get; set; }
        public Nullable<bool> HavePreviousSurgeries { get; set; }
        public Nullable<bool> HaveMedicationAllergy { get; set; }
        public string HaveOtherAllergy { get; set; }
        public string FamilyMedicalHistory { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string CreateUserID { get; set; }
        public string UpdateUserID { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
