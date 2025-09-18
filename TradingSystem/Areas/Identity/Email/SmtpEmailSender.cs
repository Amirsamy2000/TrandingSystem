using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;
 using Microsoft.Extensions.Configuration;
 
namespace TrandingSystem.Areas.Identity.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public SmtpEmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //public async Task SendEmailAsyncV2(string email, string subject, string htmlMessage)
        //{
        //    var smtpClient = new SmtpClient(_configuration["SettingsNotifies:Email:SmtpHost"])
        //    {
        //        Port = int.Parse(_configuration["SettingsNotifies:Email:SmtpPort"]) , // جرّب 587 بدل 465
        //        Credentials = new NetworkCredential(
        //            _configuration["SettingsNotifies:Email:SmtpUser"],
        //            _configuration["SettingsNotifies:Email:SmtpPass"]),
        //        EnableSsl = true,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        UseDefaultCredentials = false
        //    };

        //    var mailMessage = new MailMessage
        //    {
        //        From = new MailAddress(_configuration["SettingsNotifies:Email:SmtpUser"], _configuration["SettingsNotifies:Email:FromName"]),
        //        Subject = subject,
        //        Body = htmlMessage,
        //        IsBodyHtml = true,
        //    };
        //    mailMessage.To.Add(email);

        //    await smtpClient.SendMailAsync(mailMessage);
        //}

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // قراءة الإعدادات من appsettings.json
            var smtpHost = "mail5011.site4now.net";          // من بيانات SmarterASP
            var smtpPort = 465;                           // بورت TLS
            var smtpUser = "support@saifalqadi.com";       // الإيميل اللي هتبعت منه
            var smtpPass = "123456789@Support";                  // باسورد الإيميل
            var fromName = "Saif Al Qadi Support";         // الاسم اللي يظهر للمرسل


            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(fromName, smtpUser));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;

            // ممكن تخليها "plain" لو هتبعت نص بس
            email.Body = new TextPart("html") { Text = body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.SslOnConnect);
                await smtp.AuthenticateAsync(smtpUser, smtpPass);
                await smtp.SendAsync(email);
            }
            catch (Exception ex)
            {
                // سجل الخطأ أو ارميه حسب السيناريو
                Console.WriteLine($"Email send failed: {ex.Message}");

            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }
        }
    }
}
