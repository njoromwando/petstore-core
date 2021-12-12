using Microsoft.Extensions.Configuration;
using PetStore.Services;
using System;
using System.Text;
using Xunit;

namespace Petstore.Test
{
    public class MailServiceTest
    {
        [Fact]
        public async void ShouldSendEmail()
        {
            //arrange
            var config = GetConfiguration();
            var recipient = "jamesnjoroge87@gmail.com";
            var msg = new StringBuilder();
            msg.Append($"<strong> Order Confirmed </strong> <p/>");
            msg.Append("<br/>");
            var message = "test plain text";
            var subject = "New order confirmation";
            // act
            var newMail = new MailService(config);
            var expected = true;
            var response =  await newMail.GMailSMTP(recipient, Convert.ToString(msg), subject);
            Assert.Equal(response, expected);
        }

        public static IConfiguration GetConfiguration()
        {
            try
            {
                return new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}