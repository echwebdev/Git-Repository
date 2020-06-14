using DataModels.Core;
using DataModels.Services.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Utils;

namespace NNResourceServer.API.Controllers
{
    public class BaseApiController : ApiController
    {
        //Members
        public string PortalAPI { get; set; }
        public string PortalAPISecurityKey { get; set; }
        public GetUserSessionByAccessTokenOutput ApiSession;

        //Methods
        public void BaseInit()
        {
            try
            {
                if (this.Request == null || this.Request.Properties["apiSession"] == null) return;
                //ApiSession = JsonConvert.DeserializeObject<AccessTokenModel>(this.Request.Properties["apiSession"]);
                ApiSession = (GetUserSessionByAccessTokenOutput)this.Request.Properties["apiSession"];
                PortalAPI = WebConfigurationManager.AppSettings["PORTAL_API"];
                PortalAPISecurityKey = WebConfigurationManager.AppSettings["PORTAL_API_Security_Key"];
            }
            catch (Exception ex)
            {
                Logs.Append(ex.Message);
                //throw new Exception(ex.Message);
            }
        }
    }
}
