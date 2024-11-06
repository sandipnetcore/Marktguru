using Marktguru.BusinessLogic.Configurations;
using MarktguruAssignment.API.Logging;
using MarktguruAssignment.API.Middlleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Load AllConfigurations that is required for Connection String
builder.Services.Configure<ConnectionStringConfiguration>(builder.Configuration.GetSection("Configurations"));

//Load All Configuration that is required for JWT creation
builder.Services.Configure<JWTConfigurationSettings>(builder.Configuration.GetSection("JWTConfiguration"));

//Add custom logging. This logging is used to save in to file system.
//Else we can use the default logging provided by the .NET Core. 
builder.Logging.AddFileLogger(config =>
{
    builder.Configuration.GetSection("FileLogger").GetSection("FileLoggingOptions").Bind(config);
});

//Define the Authentication Scheme.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
        options => builder.Configuration.Bind("JwtSettings", options));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(swagger =>
{
    //This is to generate the Default UI of Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Marktguru Assignement API",
        Description = @"API to perform CRUD operations for Product. " + 
                        "For performing the Create, Update and Delete operations - <br/>" +
                        "1. First create the JWT using the login operation <br/>" +
                        "2. Copy the token value. <br/>" +
                        "3. Add the token into the headers of the Request <br/>" +
                        "4. Request Key name is &quot;token&quot;<br/><br/>" +
                        "<b>Note<b/> <br/>- Product name is unique"+
                        "<br/>- Assuming - Product can have only one description" +
                        "<br/>- If product has multiple description then we need to change few things."
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("token", new OpenApiSecurityScheme()
    {
        Name = "token",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = @"JWT Authorization using the Bearer scheme. <br/> " +
                        "Just enter token value in the text box.<br/>" +
                        "Example: &quot;eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VyTmFtZSI6IlVzZXIxIiwiZW1haWwiOiJzb21lRW1haWxAd2Vic2l0ZS5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOlsiQWRtaW4iLCJVc2VyIl0sImV4cCI6MTczMDg0MTEzMCwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1Mjg3IiwiYXVkIjoiaHR0cDovL2xvY2FsaG9zdDo1Mjg3In0.mg9KbJIn9zdIHcZCHbJM4husW3t8w74U_sZ8VIKRF2E&quot;",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "token"
                        }
                    },
                    new string[] {}

            }
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//UseAuthenticationMiddleware - Is a custom middleware, so that we can do authentication 
//Basically the validation of the signed in user and assign the user to the Context.
app.UseAuthenticationMiddleware();

//Log all my errors with request and response
app.UseLoggingMiddleware();

app.MapControllers();

app.Run();
