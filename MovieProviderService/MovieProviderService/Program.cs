using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace MovieProviderService
{
    class Program
    {
        public static IConfigurationRoot configuration;

        static async Task Main(string[] args)
        {

            var builder = new HostBuilder()
              .ConfigureAppConfiguration((hostContext, configApp) =>
              {
                        configApp.SetBasePath(Directory.GetCurrentDirectory());
                        configApp.AddJsonFile("appsettings.json", optional: true);
              })
            .ConfigureServices((hostContext, services) =>
            {
                services.Configure<IConfigurationRoot>(hostContext.Configuration);
                services.AddHostedService<MovieFetcherService>();
                services.AddCap(x =>
                {
                    x.UseInMemoryStorage();
                    x.UseRabbitMQ(option =>
                    {
                        option.HostName = hostContext.Configuration.GetSection("RabbitMq:Host").Value;
                        option.UserName = hostContext.Configuration.GetSection("RabbitMq:Username").Value;
                        option.Password = hostContext.Configuration.GetSection("RabbitMq:Password").Value;

                        option.VirtualHost = hostContext.Configuration.GetSection("RabbitMq:VirtualHost").Value;
                    });

                    x.FailedRetryCount = int.Parse(hostContext.Configuration.GetSection("RabbitMq:RetryCount").Value);
                });
            });

            await builder.RunConsoleAsync();

            Console.ReadLine();
        }

      
    }
}
