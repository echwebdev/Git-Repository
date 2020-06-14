using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using NNResourceServer.API.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(NNResourceServer.API.Startup))]
namespace NNResourceServer.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            //Token Consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
            });

            var issuer = "https://localhost:44350";
            var audience = "099153c2625149bc8ecb3e85e03f0022";
            var secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[] { new SymmetricKeyIssuerSecurityKeyProvider(issuer, secret) }
                    //IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]{new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)},
                    //Provider = new ApplicationOAuthBearerAuthenticationProvider()
                });
        }
    }
}