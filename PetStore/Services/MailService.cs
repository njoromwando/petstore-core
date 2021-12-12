using MailKit.Security;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;

using SendGrid.Helpers.Mail;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using PetStore.Interface;

namespace PetStore.Services
{
   

    public class MailService : IMailService
    {
        private readonly IConfiguration _config;

        public MailService(IConfiguration config)
        {
            _config = config;

        }

        public async Task<bool> GMailSMTP(string reciever, string message,string subject)
        {
            try
            {
                var email = new MimeMessage { Sender = MailboxAddress.Parse(_config["Email:user"]) };
                email.To.Add(MailboxAddress.Parse(reciever));
                email.Subject = subject;
                var builder = new BodyBuilder { HtmlBody = message };
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(_config["Email:host"], Convert.ToInt32(_config["Email:port"]), SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["Email:user"], _config["Email:password"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                return false;
            }

        }
    }
}
