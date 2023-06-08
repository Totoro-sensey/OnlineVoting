using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnlineVoting.Models;
using OnlineVoting.Services.Abstracts;
using OnlineVoting.Services.Candidates;
using OnlineVoting.Services.Common;
using OnlineVoting.Services.Users;
using OnlineVoting.Application.Services.Abstracts;
using OnlineVoting.Infrastructure.Services;
using OnlineVoting.Models.Identity.Entities;
using SystemOfWidget.Domain.Identity.Entities;

namespace OnlineVoting.WebUI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        AddDbContext(services, Configuration);
        AddIdentity(services);
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddScoped<IIdentityTokenService, IdentityTokenService>();
        services.AddScoped<ICandidateService, CandidateService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddRazorPages();
        services.AddCors();
        // Add services to the container.
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });
        services.AddControllers(options => options.EnableEndpointRouting = false);
       
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000"));
        app.UseAuthorization();
        app.UseAuthentication();
        
        app.UseMvc();
    }
    
    private static IServiceCollection AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

        return services;
    }
    
    private static IServiceCollection AddIdentity(IServiceCollection services)
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