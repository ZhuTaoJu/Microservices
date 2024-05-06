
using System.Data.Common;
using Email.API.Infrastructure;
using Email.API.IntegrationEvents;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF;
using Microsoft.eShopOnContainers.BuildingBlocks.IntegrationEventLogEF.Services;
using MySql.EntityFrameworkCore.Infrastructure;

namespace Email.API.Extensions
{
    public static class Extensions
    {

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var hcBuilder = services.AddHealthChecks();

            hcBuilder
                .AddMySql(_ => configuration.GetRequiredConnectionString("EmailDB"),
                    name: "EmailDB-check",
                    tags: new string[] { "ready" });

            var accountName = configuration["AzureStorageAccountName"];
            var accountKey = configuration["AzureStorageAccountKey"];

            if (!string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountKey))
            {
                hcBuilder
                    .AddAzureBlobStorage(
                        $"DefaultEndpointsProtocol=https;AccountName={accountName};AccountKey={accountKey};EndpointSuffix=core.windows.net",
                        name: "catalog-storage-check",
                        tags: new string[] { "ready" });
            }

            return services;
        }

        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            static void ConfigureMySqlOptions(MySQLDbContextOptionsBuilder sqlOptions)
            {
                sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);

                sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            };

            services.AddDbContext<EmailDBContext>(options =>
            {
                var connectionString = configuration.GetRequiredConnectionString("EmailDB");
                options.UseMySQL(connectionString, ConfigureMySqlOptions);
            });

            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                var connectionString = configuration.GetRequiredConnectionString("EmailDB");

                options.UseMySQL(connectionString, ConfigureMySqlOptions);
            });

            //services.AddDbContext<IntegrationEventLogContext>(options =>
            //{
            //    var connectionString = configuration.GetRequiredConnectionString("EmailDB");

            //    options.UseSqlServer(connectionString, ConfigureSqlOptions);
            //});

            //static void ConfigureSqlOptions(SqlServerDbContextOptionsBuilder sqlOptions)
            //{
            //    sqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName);

            //    // Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 

            //    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //};

            //services.AddDbContext<EmailContext>(options =>
            //{
            //    var connectionString = configuration.GetRequiredConnectionString("CatalogDB");

            //    options.UseSqlServer(connectionString, ConfigureSqlOptions);
            //});

            return services;
        }

        public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration);

            // TODO: Move to the new problem details middleware
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            return services;
        }


        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<IEmailIntegrationEventService, EmailIntegrationEventService>();

            return services;
        }

    }
}
