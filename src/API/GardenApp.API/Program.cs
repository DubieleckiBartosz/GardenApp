using Payments.Application.Configurations;
using Payments.Infrastructure.Configurations;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var env = builder.Environment;

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var config = builder.Configuration;
var services = builder.Services;

// Add services to the container.

builder
    .RegisterBBApplicationConfigurations()
    .RegisterBBInfrastructureConfigurations()
    .GetUsersApplicationConfigurations()
    .GetUsersInfrastructureConfigurations()
    .GetInfrastructureConfigurations()
    .RegisterPanelsInfrastructure()
    .RegisterWorksInfrastructure()
    .RegisterWorksApplication()
    .GetPaymentsInfrastructureConfigurations()
    .GetPaymentsApplicationConfigurations();

//Yes, we can write some dynamic method which could read all needed assemblies, but in this case we have control over it
var assemblyTypes = new Type[]
{
    typeof(OffersAssemblyReference),
    typeof(PanelsAppAssemblyReference),
    typeof(UsersAssemblyReference),
    typeof(WorksApplicationAssemblyReference)
};

builder.Services.RegisterMediator(assemblyTypes);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((ctx, lc) => lc.LogConfigurationService(builder.Configuration));

builder.Services.GetSwaggerConfiguration();

var app = builder.Build();

app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.PanelsIntegrationRegistration();

//Modules
app.UsersMigration()
   .UsersInitData(configuration);

app.PaymentsMigration()
   .PaymentsInitData(configuration);

app.Run();

public partial class Program
{ }