using DataModels.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Services.MedicalProfile
{
    public class GetAllMedicalProfilesInput
    {}

    public class GetAllMedicalProfilesOutput : BaseResponseModel
    {
        public GetAllMedicalProfilesOutput()
        {
            MedicalProfiless = new List<MedicalProfileModel>();
        }
        public List<MedicalProfileModel> MedicalProfiless { get; set; }
    }

    public class GetAllMedicalProfilesByUserIdInput
    {
        public string UserId { get; set; }
    }

    public class GetAllMedicalProfilesByUserIdOutput : BaseResponseModel
    {
        public GetAllMedicalProfilesByUserIdOutput()
        {
            MedicalProfiles = new List<MedicalProfileModel>();
        }
        public List<MedicalProfileModel> MedicalProfiles { get; set; }
    }

    public class GetMedicalProfileInput
    {
        public long Id { get; set; }
    }

    public class GetMedicalProfileOutput : BaseResponseModel
    {
        public MedicalProfileModel MedicalProfile { get; set; }
    }

    public class CreateMedicalProfileInput
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long GenderSD { get; set; }
        public long MaritalStatusSD { get; set; }
        public int Age { get; set; }
        public long BloodType { get; set; }
        public long WeightSD { get; set; }
        public long HeightSD { get; set; }
        public string Occupation { get; set; }
        public Boolean HaveInsurance { get; set; }
        public Boolean HaveNSSF { get; set; }
        public Boolean Smoker { get; set; }
        public Boolean HaveChildren { get; set; }
        public Boolean CaffeineDrinker { get; set; }
        public String CaffeinePerDay { get; set; }
        public Boolean TakeMedication { get; set; }
        public Boolean HavePreviousSurgeries { get; set; }
        public Boolean HaveMedicationAllergy { get; set; }
        public string HaveOtherAllergy { get; set; }
        public string FamilyMedicalHistory { get; set; }
    }

    public class CreateMedicalProfileOutput : BaseResponseModel
    {
        public long Id { get; set; }
    }

    public class UpdateMedicalProfileInput
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long GenderSD { get; set; }
        public long MaritalStatusSD { get; set; }
        public int Age { get; set; }
        public long BloodType { get; set; }
        public long WeightSD { get; set; }
        public long HeightSD { get; set; }
        public string Occupation { get; set; }
        public Boolean HaveInsurance { get; set; }
        public Boolean HaveNSSF { get; set; }
        public Boolean Smoker { get; set; }
        public Boolean HaveChildren { get; set; }
        public Boolean CaffeineDrinker { get; set; }
        public String CaffeinePerDay { get; set; }
        public Boolean TakeMedication { get; set; }
        public Boolean HavePreviousSurgeries { get; set; }
        public Boolean HaveMedicationAllergy { get; set; }
        public string HaveOtherAllergy { get; set; }
        public string FamilyMedicalHistory { get; set; }
    }

    public class UpdateMedicalProfileOutput : BaseResponseModel
    {}


    public class DeleteMedicalProfileInput
    {
        public long Id { get; set; }
    }

    public class DeleteMedicalProfileOutput : BaseResponseModel
    {
        public long Id { get; set; }
    }

    
    public class MedicalProfileModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? GenderSD { get; set; }
        public long? MaritalStatusSD { get; set; }
        public int? Age { get; set; }
        public long? BloodType { get; set; }
        public long? WeightSD { get; set; }
        public long? HeightSD { get; set; }
        public string Occupation { get; set; }
        public Boolean? HaveInsurance { get; set; }
        public Boolean? HaveNSSF { get; set; }
        public Boolean? Smoker { get; set; }
        public Boolean? HaveChildren { get; set; }
        public Boolean? CaffeineDrinker { get; set; }
        public String CaffeinePerDay { get; set; }
        public Boolean? TakeMedication { get; set; }
        public Boolean? HavePreviousSurgeries { get; set; }
        public Boolean? HaveMedicationAllergy { get; set; }
        public string HaveOtherAllergy { get; set; }
        public string FamilyMedicalHistory { get; set; }
        public string CreateUserId { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
