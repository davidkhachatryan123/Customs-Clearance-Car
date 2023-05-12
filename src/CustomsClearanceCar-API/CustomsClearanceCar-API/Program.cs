using CustomsClearanceCar_API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCORS();
builder.Services.ConfigureForwardedHeaders();

builder.Services.AddControllers();

builder.Services.ConfigureDatabase(builder.Configuration);
builder.Services.ConfigureRepositoryManager();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else if (app.Environment.IsProduction())
    app.UseForwardedHeaders();

app.UseCors("AllowOrigin");

app.MapControllers();

app.MigrateDatabase();

app.Run();
