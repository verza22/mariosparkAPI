using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Google.Apis.Auth.OAuth2;
using Google.Apis.FirebaseCloudMessaging.v1;
using Google.Apis.Services;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();

// Add configuration to access appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Register the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(connectionString);

// Configure Firebase Cloud Messaging (FCM) client
ConfigureFirebaseMessaging(builder.Services, builder.Configuration);

// Scan and register DataAccess and BusinessLogic classes
var assembliesToScan = new[] { Assembly.GetExecutingAssembly() }; // Add more assemblies if necessary
RegisterClasses(builder.Services, "api.DataAccess", assembliesToScan);
RegisterClasses(builder.Services, "api.BusinessLogic", assembliesToScan);

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(jwtOption =>
{
    var key = builder.Configuration.GetValue<string>("JwtConfig:Key");
    var keyBytes = Encoding.ASCII.GetBytes(key);
    jwtOption.SaveToken = true;
    jwtOption.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // Ensure this middleware is before UseAuthorization()
app.UseAuthorization();  // Ensure this middleware is before UseEndpoints()

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

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

// Configure Firebase Cloud Messaging (FCM) client
void ConfigureFirebaseMessaging(IServiceCollection services, IConfiguration configuration)
{
    // Load Firebase admin credentials from a JSON file
    GoogleCredential credential;
    using (var stream = new FileStream("Credentials/mariospark-182a8-firebase-adminsdk-3lib8-afd21d764c.json", FileMode.Open, FileAccess.Read))
    {
        credential = GoogleCredential.FromStream(stream)
            .CreateScoped(FirebaseCloudMessagingService.Scope.FirebaseMessaging);
    }

    string token = credential.UnderlyingCredential.GetAccessTokenForRequestAsync().GetAwaiter().GetResult();

    // Initialize Firebase Cloud Messaging service
    var fcmService = new FirebaseCloudMessagingService(new BaseClientService.Initializer
    {
        HttpClientInitializer = credential,
        ApplicationName = "MariosPark",
        ApiKey = token
    });

    // Register Firebase Cloud Messaging service as singleton
    services.AddSingleton(fcmService);
}