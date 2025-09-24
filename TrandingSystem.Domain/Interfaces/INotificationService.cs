using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrandingSystem.Domain.Entities;
using TrandingSystem.Domain.Helper;

namespace TrandingSystem.Domain.Interfaces
{
    public interface INotificationService
    {
        Task SendEmailAsync(string to, string subject, string body);
        //Task SendSmsAsync(string phoneNumber, string message);
        Task SendInAppNotificationAsync(string userId, string message);
        void SendMailForGroupUserAfterCreateBodey(List<User> users, string sub, EmailBody emailTemp);
        void SendMailForUserAfterCreateBodey(string  Email, string sub, EmailBody emailTemp);

    }
}
