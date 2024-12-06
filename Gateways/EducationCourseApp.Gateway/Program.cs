using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile($"configuration.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables();
var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
builder.Services.AddAuthentication("GatewayAuthenticationScheme")
    .AddJwtBearer("GatewayAuthenticationScheme", opt =>
    {
        opt.Authority = builder.Configuration["IdentityServerURL"];
        opt.Audience = "resource_gateway";
        opt.RequireHttpsMetadata = false;
    });
builder.Services.AddOcelot();
var app = builder.Build();
await app.UseOcelot();


app.Run();