using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;
using WebApp_EMail.Services;

var builder = WebApplication.CreateBuilder(args);

// Register the TokenService for JWT validation
builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<IEmailService, EmailService>();

// Add services to the container.
builder.Services.AddControllers();

// Add authentication and authorization using Azure AD settings
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();

builder.Services.AddAuthorization();

var app = builder.Build();

// Use authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

app.Run();