using Microsoft.Owin.Security.Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace NNAuthorizationServer.API.Providers
{
    public class FacebookAuthProvider : FacebookAuthenticationProvider
    {
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            context.Identity.AddClaim(new Claim("ExternalAccessToken", context.AccessToken));
            //context.Identity.AddClaim(new System.Security.Claims.Claim("urn:facebook:email", context.Email, ClaimValueTypes.Email, "Facebook"));

            return Task.FromResult<object>(null);
        }
    }
}