
using Microsoft.AspNetCore.Mvc;
using Application.Common.Interfaces;
using Infrastructure.Services;

namespace Ui.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();
        services.AddControllersWithViews();
        services.AddAntiforgery();
        services.AddRazorPages();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        

        return services;
    }
}
