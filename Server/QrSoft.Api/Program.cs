using Application.Common.Interfaces;
using AspNetCoreRateLimit;
using Autofac.Core;
using Infrastructure.DependencyInjection;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using Ui.DependencyInjection;

namespace QrSoft.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddWebUIServices();



            builder.Services.AddMemoryCache();

            builder.Services.Configure<IpRateLimitOptions>(options =>
            {
                options.EnableEndpointRateLimiting = true;

                options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "POST:/user/login",
                Limit = 10, // Number of requests allowed
                Period = "1m" // Time period (e.g., 1m = 1 minute)
            }
        };
            });
            builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();


            //builder.
            //Services.AddAuthentication(AuthenticationSchemes.AuthenticationScheme)
            //        .AddScheme<AuthenticationSchemeOptions, ReferenceTokenAuthenticationHandler>(AuthenticationSchemes.AuthenticationScheme,
            //      null).AddCookie();

            //          builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            //{
            //    options.Cookie.Name = "UserLoginCookie";
            //    options.SlidingExpiration = true;
            //    options.ExpireTimeSpan = new TimeSpan(1, 0, 0); // Expires in 1 hour
            //    options.Events.OnRedirectToLogin = (context) =>
            //    {
            //        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            //        return Task.CompletedTask;
            //    };
            //    options.Cookie.HttpOnly = false;
            //    // Only use this when the sites are on different domains
            //    options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
            //});

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
     .AddCookie(options =>
     {
         options.EventsType = typeof(CustomCookieAuthenticationEvents);
     
         options.Cookie.Name = "Qr_XSR";
         options.SlidingExpiration = true;
         options.ExpireTimeSpan = new TimeSpan(1, 0, 0);
         options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
         options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.Always;
         // Additional configuration options
     });




            builder.Services.AddScoped<CustomCookieAuthenticationEvents>();



            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials() // Allow cookies to be sent
                        .WithOrigins("http://localhost:5173"); // Replace with the actual origin of your React.js frontend
                });
            });


            //builder.Services.AddCors(options =>
            //{



            //    options.AddPolicy("CorsPolicy",
            //        builder => builder.AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader());
            //});


            var app = builder.Build();
            app.UseAntiXssMiddleware();

            //app.UseCors("CorsPolicy");
            app.UseCors();
            app.UseAuthorization();
            app.UseAuthentication();
            app.MapControllers();
            app.UseIpRateLimiting();

            app.Run();
        }
    }
}