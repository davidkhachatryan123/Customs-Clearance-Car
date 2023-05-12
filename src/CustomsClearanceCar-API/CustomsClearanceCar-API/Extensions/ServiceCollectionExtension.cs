using CustomsClearanceCar_API.Database;
using CustomsClearanceCar_API.Interfaces;
using CustomsClearanceCar_API.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

namespace CustomsClearanceCar_API.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationConfiguration(this ConfigurationManager configuration)
        {
            configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }

        public static void ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: "AllowOrigin",
                    builder =>
                    {
                        builder
                        .WithOrigins("http://localhost")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(_ => true);
                    });
            });
        }

        public static void ConfigureForwardedHeaders(this IServiceCollection services)
        {
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.All;
            });
        }

        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            string mssql_password = configuration["SA_PASSWORD"]!;
            string connection_string = configuration.GetConnectionString("Default")!
                .Replace("{password}", mssql_password);

            services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(connection_string,
                x => x.MigrationsAssembly("CustomsClearanceCar-API")));
        }

        public static void MigrateDatabase(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                db.Database.Migrate();
            }
        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        => services.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}
