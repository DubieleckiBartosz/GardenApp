using BuildingBlocks.Infrastructure.Config;

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
    .RegisterBBInfrastructureConfigurations();

//Yes, we can write some dynamic method which could read all needed assemblies, but in this case we have control over it
var assemblyTypes = new Type[]
{
    typeof(PanelsAssemblyReference)
};

builder.Services.RegisterMediator(assemblyTypes);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Host.UseSerilog((ctx, lc) => lc.LogConfigurationService(builder.Configuration));

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{ }