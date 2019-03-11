using Microsoft.Extensions.Configuration;
using System.IO;

namespace RMQ.Infra
{
    public class ConsoleConfiguration
    {
        public IConfigurationRoot Get()
        {
            var builder = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }
    }
}
