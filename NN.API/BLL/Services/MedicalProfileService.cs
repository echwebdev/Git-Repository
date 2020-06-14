using BLL.Common;
using DAL.Repositories;
using DAL.SQL;
using DataModels.Services.Auth;
using DataModels.Services.MedicalProfile;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace BLL.Services
{
    public class MedicalProfileService : BaseService, Interfaces.IMedicalProfileService
    {
        #region Init
        private GetUserSessionByAccessTokenOutput _apiSession;
        private MedicalProfileRepository _medicalProfileRepository;
        public MedicalProfileService(GetUserSessionByAccessTokenOutput ApiSession)
        {
            _apiSession = ApiSession;
            _medicalProfileRepository = new MedicalProfileRepository();
        }

        #endregion

        public GetAllMedicalProfilesByUserIdOutput GetAllMedicalProfilesByUserId(GetAllMedicalProfilesByUserIdInput input)
        {
            GetAllMedicalProfilesByUserIdOutput outputToReturn = new GetAllMedicalProfilesByUserIdOutput();
            try
            {
                var medicalProfiles = _medicalProfileRepository.GetMedicalProfileByUserId(input.UserId);

                if(medicalProfiles != null && medicalProfiles.Any())
                {
                    outputToReturn.MedicalProfiles = new List<MedicalProfileModel>();
                    foreach(var entity in medicalProfiles)
                    {
                        MedicalProfileModel model = new MedicalProfileModel();
                        model.Id = entity.Id;
                        model.UserId = entity.UserID;
                        model.FirstName = entity.FirstName;
                        model.LastName = entity.LastName;
                        model.GenderSD = entity.GenderSD;
                        model.MaritalStatusSD = entity.MaritalStatusSD;
                        model.Age = entity.Age;
                        model.BloodType = entity.BloodTypeSD;
                        model.WeightSD = entity.WeightSD;
                        model.HeightSD = entity.HeightSD;
                        model.Occupation = entity.Occupation;
                        model.HaveInsurance = entity.HaveInsurance;
                        model.HaveNSSF = entity.HaveNSSF;
                        model.Smoker = entity.Smoker;
                        model.HaveChildren = entity.HaveChildren;
                        model.CaffeineDrinker = entity.CaffeineDrinker;
                        model.CaffeinePerDay = entity.CaffeinePerDay;
                        model.TakeMedication = entity.TakeMedication;
                        model.HavePreviousSurgeries = entity.HavePreviousSurgeries;
                        model.HaveMedicationAllergy = entity.HaveMedicationAllergy;
                        model.HaveOtherAllergy = entity.HaveOtherAllergy;
                        model.FamilyMedicalHistory = entity.FamilyMedicalHistory;

                        outputToReturn.MedicalProfiles.Add(model);
                    }
                }

                return outputToReturn;
            }
            catch(Exception ex)
            {
                WriteLogFile.Append("GetAllMedicalProfilesByUserId : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
                return null;
            }
        }

        public GetMedicalProfileOutput GetMedicalProfile(GetMedicalProfileInput input)
        {
            GetMedicalProfileOutput outputToReturn = new GetMedicalProfileOutput();
            try
            {
                MedicalProfile medicalProfile = _medicalProfileRepository.GetMedicalProfileById(input.Id);

                if (medicalProfile != null)
                {
                    outputToReturn.MedicalProfile = new MedicalProfileModel();
                    MedicalProfileModel model = new MedicalProfileModel();
                    model.Id = medicalProfile.Id;
                    model.UserId = medicalProfile.UserID;
                    model.FirstName = medicalProfile.FirstName;
                    model.LastName = medicalProfile.LastName;
                    model.GenderSD = medicalProfile.GenderSD;
                    model.MaritalStatusSD = medicalProfile.MaritalStatusSD;
                    model.Age = medicalProfile.Age;
                    model.BloodType = medicalProfile.BloodTypeSD;
                    model.WeightSD = medicalProfile.WeightSD;
                    model.HeightSD = medicalProfile.HeightSD;
                    model.Occupation = medicalProfile.Occupation;
                    model.HaveInsurance = medicalProfile.HaveInsurance;
                    model.HaveNSSF = medicalProfile.HaveNSSF;
                    model.Smoker = medicalProfile.Smoker;
                    model.HaveChildren = medicalProfile.HaveChildren;
                    model.CaffeineDrinker = medicalProfile.CaffeineDrinker;
                    model.CaffeinePerDay = medicalProfile.CaffeinePerDay;
                    model.TakeMedication = medicalProfile.TakeMedication;
                    model.HavePreviousSurgeries = medicalProfile.HavePreviousSurgeries;
                    model.HaveMedicationAllergy = medicalProfile.HaveMedicationAllergy;
                    model.HaveOtherAllergy = medicalProfile.HaveOtherAllergy;
                    model.FamilyMedicalHistory = medicalProfile.FamilyMedicalHistory;
                }

                return outputToReturn;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("GetMedicalProfile : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
                return null;
            }
        }

        // add new medical record by current user
        public CreateMedicalProfileOutput CreateMedicalProfile(CreateMedicalProfileInput input)
        {
            CreateMedicalProfileOutput outputToReturn = new CreateMedicalProfileOutput();
            try
            {
                MedicalProfile medicalProfile = new MedicalProfile();
                medicalProfile.UserID = _apiSession.CurrentUserID;
                medicalProfile.FirstName = input.FirstName;
                medicalProfile.LastName = input.LastName;
                medicalProfile.GenderSD = input.GenderSD;
                medicalProfile.MaritalStatusSD = input.MaritalStatusSD;
                medicalProfile.Age = input.Age;
                medicalProfile.BloodTypeSD = input.BloodType;
                medicalProfile.WeightSD = input.WeightSD;
                medicalProfile.HeightSD = input.HeightSD;
                medicalProfile.Occupation = input.Occupation;
                medicalProfile.HaveInsurance = input.HaveInsurance;
                medicalProfile.HaveNSSF = input.HaveNSSF;
                medicalProfile.Smoker = input.Smoker;
                medicalProfile.HaveChildren = input.HaveChildren;
                medicalProfile.CaffeineDrinker = input.CaffeineDrinker;
                medicalProfile.CaffeinePerDay = input.CaffeinePerDay;
                medicalProfile.TakeMedication = input.TakeMedication;
                medicalProfile.HavePreviousSurgeries = input.HavePreviousSurgeries;
                medicalProfile.HaveMedicationAllergy = input.HaveMedicationAllergy;
                medicalProfile.HaveOtherAllergy = input.HaveOtherAllergy;
                medicalProfile.FamilyMedicalHistory = input.FamilyMedicalHistory;
                medicalProfile.CreateUserID = _apiSession.CurrentUserID;
                medicalProfile.DateCreated = DateTime.UtcNow;
                
                long Id = _medicalProfileRepository.CreateMedicalProfile(medicalProfile);

                outputToReturn.Id = Id;
                outputToReturn.ReturnStatus.OK();
                return outputToReturn;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("CreateMedicalProfile : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
                return null;
            }
        }

        // get medical record by its id and update it
        public UpdateMedicalProfileOutput UpdateMedicalProfile(UpdateMedicalProfileInput input)
        {
            UpdateMedicalProfileOutput outputToReturn = new UpdateMedicalProfileOutput();
            try
            {
                MedicalProfile medicalProfile = _medicalProfileRepository.GetMedicalProfileById(input.Id);
                medicalProfile.FirstName = input.FirstName;
                medicalProfile.LastName = input.LastName;
                medicalProfile.GenderSD = input.GenderSD;
                medicalProfile.MaritalStatusSD = input.MaritalStatusSD;
                medicalProfile.Age = input.Age;
                medicalProfile.BloodTypeSD = input.BloodType;
                medicalProfile.WeightSD = input.WeightSD;
                medicalProfile.HeightSD = input.HeightSD;
                medicalProfile.Occupation = input.Occupation;
                medicalProfile.HaveInsurance = input.HaveInsurance;
                medicalProfile.HaveNSSF = input.HaveNSSF;
                medicalProfile.Smoker = input.Smoker;
                medicalProfile.HaveChildren = input.HaveChildren;
                medicalProfile.CaffeineDrinker = input.CaffeineDrinker;
                medicalProfile.CaffeinePerDay = input.CaffeinePerDay;
                medicalProfile.TakeMedication = input.TakeMedication;
                medicalProfile.HavePreviousSurgeries = input.HavePreviousSurgeries;
                medicalProfile.HaveMedicationAllergy = input.HaveMedicationAllergy;
                medicalProfile.HaveOtherAllergy = input.HaveOtherAllergy;
                medicalProfile.FamilyMedicalHistory = input.FamilyMedicalHistory;
                medicalProfile.CreateUserID = _apiSession.CurrentUserID;
                medicalProfile.DateCreated = DateTime.UtcNow;

                _medicalProfileRepository.UpdateMedicalProfile(medicalProfile);

                outputToReturn.ReturnStatus.OK();
                return outputToReturn;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("UpdateMedicalProfile : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
                return null;
            }
        }

        public DeleteMedicalProfileOutput DeleteMedicalProfile(DeleteMedicalProfileInput input)
        {
            DeleteMedicalProfileOutput outputToReturn = new DeleteMedicalProfileOutput();
            try
            {
                long recordDeletedId = outputToReturn.Id = _medicalProfileRepository.DeleteMedicalProfile(input.Id);

                outputToReturn.Id = recordDeletedId;
                outputToReturn.ReturnStatus.OK();
                return outputToReturn;
            }
            catch (Exception ex)
            {
                WriteLogFile.Append("DeleteMedicalProfile : ");
                WriteLogFile.Append(ex.Message);
                WriteLogFile.Append(ex.StackTrace);
                return null;
            }
        }
    }
}
