using BLL.Common;
using BLL.Services;
using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace NNResourceServer.API.Controllers
{   
    [Authorize]
    [RoutePrefix("MedicalProfile")]
    [EnableCors("*")]
    public class MedicalProfileController : BaseApiController
    {
        private MedicalProfileService _medicalProfileSvc;

        //testing git

        private void Init()
        {
            BaseInit();
            WriteLogFile.Append("Init between");
            _medicalProfileSvc = new MedicalProfileService(ApiSession);
        }

        ////[Route("GetContactsByRecordOwnerID")]
        //[HttpPost]
        //[ResponseType(typeof(List<DataModels.Services.Contact.DTO.GetContactsByRecordOwnerIDOutput>))]
        //[Extensions.Auth]
        //public IHttpActionResult GetContactsByRecordOwnerID(DataModels.Services.Contact.DTO.GetContactsByRecordOwnerIDInput Input)
        //{
        //    LogFile.Append("GetContactsByRecordOwnerID START");
        //    Init();
        //    LogFile.Append("GetContactsByRecordOwnerID FINSIDH INIT");
        //    return Ok(_contactSvc.GetContactsByRecordOwnerID(Input));
        //}

        ////[Route("GetContactsByRecordOwnerID")]
        //[HttpPost]
        //[ResponseType(typeof(List<DataModels.Services.Contact.DTO.GetRecentlyAccessedContactsByRecordOwnerIDOutput>))]
        //[Extensions.Auth]
        //public IHttpActionResult GetRecentlyAccessedContactsByRecordOwnerID(DataModels.Services.Contact.DTO.GetRecentlyAccessedContactsByRecordOwnerIDInput Input)
        //{
        //    Init();
        //    return Ok(_contactSvc.GetRecentlyAccessedContactsByRecordOwnerID(Input));
        //}

        //[HttpPost]
        //[ResponseType(typeof(DataModels.Services.Contact.DTO.GetContactDetailsOutput))]
        //[Extensions.Auth]
        //public IHttpActionResult GetContactDetails(DataModels.Services.Contact.DTO.GetContactDetailsInput Input)
        //{
        //    Init();
        //    return Ok(_contactSvc.GetContactDetails(Input));
        //}

        [HttpPost]
        [ResponseType(typeof(List<DataModels.Services.MedicalProfile.GetAllMedicalProfilesByUserIdOutput>))]
        [Extensions.Auth]
        public IHttpActionResult GetAllMedicalProfilesByUserId(DataModels.Services.MedicalProfile.GetAllMedicalProfilesByUserIdInput Input)
        {
            Init();
            return Ok(_medicalProfileSvc.GetAllMedicalProfilesByUserId(Input));
        }

        [HttpPost]
        [ResponseType(typeof(DataModels.Services.MedicalProfile.CreateMedicalProfileOutput))]
        [Extensions.Auth]
        public IHttpActionResult CreateMedicalProfile(DataModels.Services.MedicalProfile.CreateMedicalProfileInput Input)
        {
            Init();
            return Ok(_medicalProfileSvc.CreateMedicalProfile(Input));
        }

        [HttpPost]
        [ResponseType(typeof(DataModels.Services.MedicalProfile.UpdateMedicalProfileOutput))]
        [Extensions.Auth]
        public IHttpActionResult UpdateMedicalProfile(DataModels.Services.MedicalProfile.UpdateMedicalProfileInput Input)
        {
            Init();
            return Ok(_medicalProfileSvc.UpdateMedicalProfile(Input));
        }

        [HttpPost]
        [ResponseType(typeof(DataModels.Services.MedicalProfile.DeleteMedicalProfileOutput))]
        [Extensions.Auth]
        public IHttpActionResult DeleteMedicalProfile(DataModels.Services.MedicalProfile.DeleteMedicalProfileInput Input)
        {
            Init();
            return Ok(_medicalProfileSvc.DeleteMedicalProfile(Input));
        }
    }
}
