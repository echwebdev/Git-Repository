using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Utils;

namespace NNResourceServer.API.Extensions
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public bool Required { get; set; }
        private BLL.Services.AuthService  _authSvc;

        public AuthAttribute()
        {
            _authSvc = new BLL.Services.AuthService();
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization != null)
                {
                    var authorizationHeader = actionContext.Request.Headers.Authorization.Parameter;
                    var sessionUserRv = _authSvc.GetUserSessionByAccessToken(authorizationHeader);

                    if (sessionUserRv != null)
                    {
                        //sessionUserRv.AccessToken = authorizationHeader;
                        if (actionContext.Request.Properties.Any(o => o.Key == "apiSession"))
                            actionContext.Request.Properties.Remove("apiSession");
                        actionContext.Request.Properties.Add("apiSession", sessionUserRv);
                        return;
                    }
                }
            }
            catch(Exception ex) {
                Logs.Append(ex.Message);
                Logs.Append(ex.StackTrace);
                //throw new Exception(ex.Message);
            }

            var response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "UNAUTHORIZED");
            actionContext.Response = response;
        }

    }
}