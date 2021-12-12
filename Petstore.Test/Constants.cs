using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Petstore.Test
{
    public class Constants
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        }
    }
}
