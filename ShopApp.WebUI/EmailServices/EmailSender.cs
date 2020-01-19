using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private const string SendGridKey = "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";
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(SendGridKey, subject, htmlMessage, email);
        }

        private Task Execute(string sendGridKey, string subject, string htmlMessage, string email)
        {
            var client = new SendGridClient(sendGridKey);

            var message = new SendGridMessage()
            {
                From = new EmailAddress("info@shopapp.com", "Shop App"),
                 Subject = subject,
                  PlainTextContent = htmlMessage,
                   HtmlContent = htmlMessage
            };
            message.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(message);
        }
    }

    //dotnet add package SendGrid
}
