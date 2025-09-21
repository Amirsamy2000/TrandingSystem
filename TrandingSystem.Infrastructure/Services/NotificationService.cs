using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Helper;
using TrandingSystem.Domain.Interfaces;
using TrandingSystem.Infrastructure.Constants;

namespace TrandingSystem.Infrastructure.Services
{
    public class EmailBody
    {
        public string dir { set; get; }
        public string Subject { set; get; }
        public string StieName { set; get; }
        public string Hi { set; get; }
        public string UserName { set; get; }
        public string info1 { set; get; }
        public string info2 { set; get; }
        public string info3 { set; get; }

        public string contact { set; get; }
        public string ActionUrl { set; get; }
        public string namebtn { set; get; }

    }
    public class NotificationService : INotificationService
    {
        private readonly IConfiguration _config;

        public NotificationService(IConfiguration config)
        {
            _config = config;
        }

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

            using var smtp = new SmtpClient();
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



        public Task SendInAppNotificationAsync(string userId, string message)
        {
            throw new NotImplementedException();
        }

        //public Task SendSmsAsync(string phoneNumber, string message)
        //{
        //    throw new NotImplementedException();
        //}
 

     

        public void SendMailForGroupUserAfterCreateBodey(List<User> users, string sub, Domain.Helper.EmailBody emailTemp)
        {
            foreach (var user in users)
            {
                // change User Name For Receviver
                emailTemp.UserName = user.FullName;
                // Create Htm Body
                string body = EmailTemplate.CreateBodyMail(emailTemp);
                SendEmailAsync(user.Email, sub, body);
            }
        }

        public void SendMailForUserAfterCreateBodey(string Email, string sub, Domain.Helper.EmailBody emailTemp)
        {
                // Create Htm Body
                string body = EmailTemplate.CreateBodyMail(emailTemp);
                SendEmailAsync(Email, sub, body);
            
        }

    }
}
