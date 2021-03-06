using FluentValidation;
using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Bson.Serialization;
using MovieService.Api.Filters;
using MovieService.Api.Services;
using MovieService.Application.Dtos.Requests;
using MovieService.Application.IntegrationEvents.Handlers;
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
                "amqp://guest:guest@"+Configuration.GetSection("RabbitMq:Host").Value,
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
            BsonClassMap.RegisterClassMap<Movie>(cm => {
                cm.AutoMap();
                cm.MapField("_reviews").SetElementName("Reviews");
            });

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = Configuration.GetSection("Redis").Value;
            });

            services.AddMediatR(typeof(GetMovieRequestHandler));

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
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
           
                    option.VirtualHost = Configuration.GetSection("RabbitMq:VirtualHost").Value;
                });

                x.FailedRetryCount = int.Parse(Configuration.GetSection("RabbitMq:RetryCount").Value);
            });

            services.AddTransient<MovieFetchedIntegrationEventHandler>();

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

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Atros Movie Service",
                    Version = "v1",
                    Description = "Movie Service HTTP API. This is a Data-Driven/CRUD microservice "
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(p =>
            {
                p.SwaggerEndpoint("v1/swagger.json", "Swagger Test");
            });
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
