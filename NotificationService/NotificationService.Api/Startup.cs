using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationService.Api.IntegrationEvents.Handlers;

namespace NotificationService.Api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

           var configuration = new ConfigurationBuilder()
             .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
             .AddJsonFile("appsettings.json", false)
             .Build();
            services.AddScoped<SmtpClient>((serviceProvider) =>
            {
                return new SmtpClient()
                {
                    Host = configuration.GetSection("Email:Smtp:Host").Value,
                    Port = int.Parse(configuration.GetSection("Email:Smtp:Port").Value),
                    Credentials = new NetworkCredential(
                            configuration.GetSection("Email:Smtp:Username").Value,
                            configuration.GetSection("Email:Smtp:Password").Value
                        )
                };
            });
            var a = configuration.GetSection("RabbitMq:Host").Value;
            services.AddCap(x =>
            {
                x.UseInMemoryStorage();
                x.UseRabbitMQ(option =>
                {
                    option.HostName = configuration.GetSection("RabbitMq:Host").Value;
                    option.UserName = configuration.GetSection("RabbitMq:Username").Value;
                    option.Password = configuration.GetSection("RabbitMq:Password").Value;

                    option.VirtualHost = configuration.GetSection("RabbitMq:VirtualHost").Value;
                });

                x.FailedRetryCount = int.Parse(configuration.GetSection("RabbitMq:RetryCount").Value);
            });
            services.AddTransient<MovieRecommendedIntegrationEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
