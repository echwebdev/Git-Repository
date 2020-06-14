using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace NNResourceServer.API.Controllers
{
    
    [RoutePrefix("Contact")]
    //[EnableCors("*", "*", "*")]
    public class ContactController : BaseApiController
    {
        //private FieldUpdateHistoryService _theSvc;
        //private ContactService _contactSvc;

        //private void Init()
        //{
        //    BaseInit();
        //    LogFile.Append("Init between");
        //    _contactSvc = new ContactService(ApiSession);
        //    _theSvc = new FieldUpdateHistoryService(ApiSession);
        //}

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

        //[HttpPost]
        //[ResponseType(typeof(DataModels.Services.Contact.DTO.InsertContactOutput))]
        //[Extensions.Auth]
        //public IHttpActionResult InsertContact(DataModels.Services.Contact.DTO.InsertContactInput Input)
        //{
        //    Init();
        //    return Ok(_contactSvc.InsertContact(Input));
        //}

        //[HttpPost]
        //[ResponseType(typeof(DataModels.Services.Contact.DTO.UpdateContactOutput))]
        //[Extensions.Auth]
        //public IHttpActionResult UpdateContact(DataModels.Services.Contact.DTO.UpdateContactInput Input)
        //{
        //    Init();
        //    return Ok(_contactSvc.UpdateContact(Input));
        //}

        //[HttpPost]
        //[ResponseType(typeof(DataModels.Services.Contact.DTO.UpdateBulkContactsOutput))]
        //[Extensions.Auth]
        //public IHttpActionResult UpdateBulkContacts(DataModels.Services.Contact.DTO.UpdateBulkContactsInput Input)
        //{
        //    Init();
        //    return Ok(_contactSvc.UpdateBulkContacts(Input));
        //}

        //[HttpPost]
        //[ResponseType(typeof(DataModels.Services.Contact.DTO.DeleteContactOutput))]
        //[Extensions.Auth]
        //public IHttpActionResult DeleteContact(DataModels.Services.Contact.DTO.DeleteContactInput Input)
        //{
        //    Init();
        //    return Ok(_contactSvc.DeleteContact(Input));
        //}

        //[HttpPost]
        //[ResponseType(typeof(DataModels.Services.Contact.DTO.DeleteBulkContactsOutput))]
        //[Extensions.Auth]
        //public IHttpActionResult DeleteBulkContacts(DataModels.Services.Contact.DTO.DeleteBulkContactsInput Input)
        //{
        //    Init();
        //    return Ok(_contactSvc.DeleteBulkContacts(Input));
        //}

        ////[Route("GetContactsByRecordOwnerID")]
        //[HttpPost]
        //[ResponseType(typeof(List<DataModels.Services.Contact.DTO.GetReferredByContactsByRecordOwnerIDOutput>))]
        //[Extensions.Auth]
        //public IHttpActionResult GetReferredByContactsByRecordOwnerID(DataModels.Services.Contact.DTO.GetReferredByContactsByRecordOwnerIDInput Input)
        //{
        //    Init();
        //    return Ok(_contactSvc.GetReferredByContactsByRecordOwnerID(Input));
        //}

        //[HttpPost]
        //[ResponseType(typeof(List<DataModels.Services.Contact.DTO.GetDuplicateContactsByRecordOwnerIDOutput>))]
        //[Extensions.Auth]
        //public IHttpActionResult GetDuplicateContactsByRecordOwnerID(DataModels.Services.Contact.DTO.GetDuplicateContactsByRecordOwnerIDInput Input)
        //{
        //    Init();
        //    return Ok(_contactSvc.GetDuplicateContactsByRecordOwnerID(Input));
        //}



        ////TODOSD: This is temporary here, should be in FieldUpdateHistory
        //[HttpPost]
        //[ResponseType(typeof(List<GetFieldUpdateHistoryOutput>))]
        //[Extensions.Auth]
        //public IHttpActionResult GetItems(GetFieldUpdateHistoryInput input)
        //{
        //    Init();
        //    return Ok(_theSvc.GetFieldChanges(input));
        //}

    }
}
