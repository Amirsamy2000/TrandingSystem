using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TrandingSystem.Areas.Identity.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient(_configuration["SettingsNotifies:Email:SmtpHost"])
            {
                Port = int.Parse(_configuration["SettingsNotifies:Email:SmtpPort"]) , // جرّب 587 بدل 465
                Credentials = new NetworkCredential(
                    _configuration["SettingsNotifies:Email:SmtpUser"],
                    _configuration["SettingsNotifies:Email:SmtpPass"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["SettingsNotifies:Email:SmtpUser"], _configuration["SettingsNotifies:Email:FromName"]),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
