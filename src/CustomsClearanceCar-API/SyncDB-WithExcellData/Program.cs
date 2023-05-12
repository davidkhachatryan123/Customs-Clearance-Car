using CustomsClearanceCar_API.Database;
using Microsoft.EntityFrameworkCore;
using SyncDB_WithExcellData;
using SyncDB_WithExcellData.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        string mssql_password = configuration["SA_PASSWORD"]!;
        string connection_string = configuration.GetConnectionString("Default")!
            .Replace("{password}", mssql_password);

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        optionsBuilder.UseSqlServer(connection_string);
        services.AddScoped(db => new ApplicationContext(optionsBuilder.Options));

        services.AddScoped<SyncDB>();

        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
