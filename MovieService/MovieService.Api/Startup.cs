using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using MovieService.Api.Filters;
using MovieService.Application.Dtos.Requests;
using MovieService.Application.UseCases;
using MovieService.Application.Validations;
using MovieService.Domain.AggregatesModels.Movie;
using MovieService.Domain.SeedWork;
using MovieService.Infrastructure.Repositories;
using MovieService.Infrastructure.Settings;

namespace MovieService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"{Configuration["Auth0:Domain"]}";
                options.Audience = Configuration["Auth0:Audience"];
            });

            var hcBuilder = services.AddHealthChecks();

            hcBuilder
                .AddCheck("MongoDB-self", () => HealthCheckResult.Healthy())
                .AddMongoDb(
                    Configuration.GetSection("MongoConnection:ConnectionString").Value,
                    name: "MongoDB-check",
                    tags: new string[] { "MongoDB" });
            hcBuilder
              .AddCheck("Redis-self", () => HealthCheckResult.Healthy())
              .AddRedis(
                  Configuration.GetSection("Redis").Value,
                  name: "Redis-check",
                  tags: new string[] { "Redis" });

            hcBuilder
              .AddCheck("RabbitMQ-self", () => HealthCheckResult.Healthy())
              .AddRabbitMQ(
                 Configuration.GetSection("RabbitMq:Host").Value,
                  name: "RabbitMQ-check",
                  tags: new string[] { "RabbitMQ" });

            hcBuilder
              .AddCheck("Elasticsearch-self", () => HealthCheckResult.Healthy())
              .AddElasticsearch(
                  Configuration.GetSection("ElasticSearchOptions:ClusterUrl").Value,
                  name: "Elasticsearch-check",
                  tags: new string[] { "Elasticsearch" });


            services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString
                    = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database
                    = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration.GetSection("Redis").Value;
            });

            services.AddMediatR(typeof(GetMovieRequestHandler));


            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddTransient<IGuidGenerator, GuidGenerator>();

            services.AddTransient<IValidator<AddReviewRequest>, AddReviewRequestValidation>();
            services.AddTransient<IValidator<GetMovieRequest>, GetMovieRequestValidation>();
            services.AddTransient<IValidator<RecommendMovieRequest>, RecommendMovieRequestValidation>();


            services.AddCap(x =>
            {
                x.UseMongoDB(option =>
                {
                    option.DatabaseConnection = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                });
                x.UseRabbitMQ(option =>
                {
                    option.HostName = Configuration.GetSection("RabbitMq:Host").Value;
                    option.UserName = Configuration.GetSection("RabbitMq:Username").Value;
                    option.Password = Configuration.GetSection("RabbitMq:Password").Value;
                });

                x.FailedRetryCount = int.Parse(Configuration.GetSection("RabbitMq:RetryCount").Value);
            });


            services.AddMvc(options =>
            {
                options.Filters.Add(new ValidateModelStateFilter());

            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });

            services.Configure<ApiBehaviorOptions>(options => {
                options.SuppressModelStateInvalidFilter = true;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();


            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }
}
