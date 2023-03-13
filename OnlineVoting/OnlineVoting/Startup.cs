using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OnlineVoting.Models;
using OnlineVoting.Services;
using OnlineVoting.Services.Abstracts;

namespace OnlineVoting;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddRazorPages();
        services.AddDbContext<ApplicationContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        // Add services to the container.
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });
        services.AddControllers(options => options.EnableEndpointRouting = false);
        services.AddScoped<ICandidateService, CandidateService>();
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
        
        app.UseAuthorization();
        app.UseAuthentication();
        
        app.UseMvc();
    }
}