using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using OnlineVoting.Application.Services.Abstracts;
using OnlineVoting.Models;
using OnlineVoting.Models.Identity.Entities;
using SystemOfWidget.Domain.Identity.Entities;

namespace OnlineVoting.Application.Extensions;

public static class DbExtension
{
    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);

        return services;
    }
    
    private static IServiceCollection AdddIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationUser>
            (opt =>
            {
                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
            })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "CookieName";
            options.LoginPath = "/Account/Login";
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration = true;
        });

        return services;
    }
}