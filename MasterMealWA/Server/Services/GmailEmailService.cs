using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterMealWA.Server.Services.Interfaces
{
    public class GmailEmailService : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public GmailEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        /// <summary>
        /// sends an email using gmail configured in appsettings.json
        /// </summary> 
        /// <param name ="email">Recipient email</param>
        /// <param name="subject">Subject of email</param>
        /// <param name="htmlMessage">Body of email</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailTo = new MimeMessage();
            emailTo.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
            emailTo.To.Add(MailboxAddress.Parse(email));
            emailTo.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = htmlMessage;
            emailTo.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();
            var host = _configuration["MailSettings:Host"];
            var port = Convert.ToInt32(_configuration["MailSettings:Port"]);
            smtp.Connect(host, port, SecureSocketOptions.StartTls);
            var username = _configuration["MailSettings:Mail"];
            var pass = _configuration["MailSettings:Password"];
            smtp.Authenticate(username, pass);

            await smtp.SendAsync(emailTo);
            smtp.Disconnect(true);
        }
    }
}

