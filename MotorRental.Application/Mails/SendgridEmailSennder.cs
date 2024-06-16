using SendGrid.Helpers.Mail;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MotorRental.UseCase.Mails
{
    public class SendgridEmailSennder : IEmailSender
    {
        public string SendGridSecret { get; set; }

        public SendgridEmailSennder(IConfiguration config)
        {
            SendGridSecret = config["SendGrid:SecretKey"];
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(SendGridSecret);

            var from = new EmailAddress("huynhvangiang0504@gmail.com", "MotorRental");
            var to = new EmailAddress(email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
