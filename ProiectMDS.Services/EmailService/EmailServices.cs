using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectMDS.Services.EmailService
{
    public class EmailServices : IEmailServices
    {
        private readonly IConfiguration _configuration;
        public EmailServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailLogin(string toEmail, string subject, string content)
        {
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Get<String>();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("eneandrei2000@gmail.com", "Find a study buddy");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }

        public async Task SendEmailRegister(string toEmail, string name)
        {
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Get<String>();
            var sendGridClient = new SendGridClient(apiKey);
            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom("eneandrei2000@gmail.com", "Find a study buddy");
            sendGridMessage.AddTo(toEmail);
            //var templateKey = Environment.GetEnvironmentVariable("SENDGRID_TEMPLATE_ID");
            var templateKey = _configuration.GetSection("SENDGRID_TEMPLATE_ID").Get<String>();
            sendGridMessage.SetSubject($"Welcome, {name}");
            sendGridMessage.SetTemplateId(templateKey);
            sendGridMessage.SetTemplateData(new
            {
                name = "Find a study buddy",
                url = $"https://mc.sendgrid.com/dynamic-templates/{templateKey}"
            });

            var response = await sendGridClient.SendEmailAsync(sendGridMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                Console.WriteLine("Email sent");
            }
        }

        public async Task SendPassword(string toEmail, string subject, string content)
        {
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Get<String>();
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("eneandrei2000@gmail.com", "Find a study buddy");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
