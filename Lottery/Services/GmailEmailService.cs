using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;

namespace Lottery.Services
{
    public class GmailEmailService : SmtpClient,IEmailSender
    {
        public GmailEmailService() : base("smtp.gmail.com", 587)
        {
            //Get values from web.config file:
            this.UserName = "bityukofff@gmail.com";
            this.EnableSsl = true;
            this.UseDefaultCredentials = false;
            this.Credentials = new System.Net.NetworkCredential(this.UserName, "3934168z");
        }

        public string UserName { get; }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage(new MailAddress("bityukofff@gmail.com", "Lottery Account Confirmation (do not reply)"), new MailAddress(email));

            message.Subject = subject;
            message.Body = htmlMessage;

            message.IsBodyHtml = true;

            await this.SendMailAsync(message);
        }
    }
}
