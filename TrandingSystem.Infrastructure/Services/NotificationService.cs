using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TrandingSystem.Domain.Interfaces;
 
namespace TrandingSystem.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _config;

        public NotificationService(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {  // قراءة الإعدادات من appsettings.json
            var smtpHost = _config["SettingsNotifies:Email:SmtpHost"];
            var smtpPort = int.Parse(_config["SettingsNotifies:Email:SmtpPort"]);
            var smtpUser = _config["SettingsNotifies:Email:SmtpUser"];
            var smtpPass = _config["SettingsNotifies:Email:SmtpPass"];
            var fromName = _config["SettingsNotifies:Email:FromName"];

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromName, smtpUser));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            // ممكن تخليها "plain" لو هتبعت نص بس
            email.Body = new TextPart("html") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(smtpUser, smtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }


        public Task SendInAppNotificationAsync(string userId, string message)
        {
            throw new NotImplementedException();
        }

        public Task SendSmsAsync(string phoneNumber, string message)
        {
            throw new NotImplementedException();
        }
    }
}
