using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.Services.EmailService
{
    public interface IEmailServices
    {
        Task SendEmailLogin(string model, string subject, string content);
        Task SendEmailRegister(string model, string name);
        Task SendPassword(string model, string subject, string content);
        //Task SendEmailToAFriend(string toEmail, string subject, string content);
    }
}
