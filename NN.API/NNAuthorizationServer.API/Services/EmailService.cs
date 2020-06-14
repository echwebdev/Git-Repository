using Microsoft.AspNet.Identity;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace NNAuthorizationServer.API.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await configSendGridasync(message);
        }


        private async Task configSendGridasync(IdentityMessage message)
        {
            var apiKey = ConfigurationManager.AppSettings["SENDGRID_KEY"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("ech.inplan@gmail.com", "Nurse Nova");
            var subject = message.Subject;
            var to = new EmailAddress("ech.webdev@gmail.com", "Example User");
            var plainTextContent = message.Body;
            var htmlContent = message.Body;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            // Send the email.
            if (client != null)
            {
                await client.SendEmailAsync(msg);
            }
            else
            {
                Trace.TraceError("Failed to create Web transport.");
                await Task.FromResult(0);
            }
        }

        
    }
}