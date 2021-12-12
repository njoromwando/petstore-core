using Microsoft.Extensions.Configuration;
using PetStore.Services;
using System;
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
            // act
            var newMail = new MailService(config);
            var expected = true;
            var response = await newMail.GMailSMTP();
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