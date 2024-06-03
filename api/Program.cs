using Microsoft.Extensions.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add configuration to access appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Register the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(connectionString);

// Scan and register DataAccess and BusinessLogic classes
var assembliesToScan = new[] { Assembly.GetExecutingAssembly() }; // Add more assemblies if necessary
RegisterClasses(builder.Services, "api.DataAccess", assembliesToScan);
RegisterClasses(builder.Services, "api.BusinessLogic", assembliesToScan);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();


// Method to register classes based on a namespace and assemblies to scan
void RegisterClasses(IServiceCollection services, string @namespace, Assembly[] assemblies)
{
    var classes = assemblies
        .SelectMany(a => a.GetTypes())
        .Where(t => t.IsClass && t.Namespace == @namespace && !t.IsAbstract);

    foreach (var classType in classes)
    {
        var interfaces = classType.GetInterfaces();

        foreach (var interfaceType in interfaces)
        {
            // Register classes with singleton lifetime if they implement IConfiguration
            if (interfaceType == typeof(IConfiguration))
            {
                services.AddSingleton(interfaceType, classType);
            }
            else
            {
                services.AddTransient(interfaceType, classType);
            }
        }
    }
}