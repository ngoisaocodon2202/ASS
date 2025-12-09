using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace ASS.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var settings = _config.GetSection("EmailSettings");

            var client = new SmtpClient(settings["Host"], int.Parse(settings["Port"]!))
            {
                Credentials = new NetworkCredential(settings["Username"], settings["Password"]),
                EnableSsl = true
            };

            var mail = new MailMessage(settings["Username"]!, to, subject, body)
            {
                IsBodyHtml = true
            };

            client.Send(mail);
        }
    }
}
