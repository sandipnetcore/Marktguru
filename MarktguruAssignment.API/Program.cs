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
        Title = "JWT Token Authentication API",
        Description = ".NET 8 Web API"
    });
    // To Enable authorization using Swagger (JWT)
    swagger.AddSecurityDefinition("token", new OpenApiSecurityScheme()
    {
        Name = "token",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
