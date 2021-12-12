using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PetStore.Services
{
    public class MailService
    {
        private readonly IConfiguration _config;

        public MailService(IConfiguration config)
        {
            _config = config;
            
        }

        public async Task GMailSMTP()
        {
            try
            {
                var email = new MimeMessage { Sender = MailboxAddress.Parse(_config["Email:user"]) };
                email.To.Add(MailboxAddress.Parse("jamesnjoroge87@gmail.com"));
                email.Subject = "Test";
                var builder = new BodyBuilder { HtmlBody = $"<strong> Hello James </strong>" };
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(_config["Email:host"], Convert.ToInt32(_config["Email:port"]), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["Email:user"], _config["Email:password"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex); 
            }
          


        }
    }
}
