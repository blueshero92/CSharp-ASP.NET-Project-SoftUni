using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Services.Core;
using GamingZoneApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string? connectionString 
                = builder.Configuration.GetConnectionString("GamingZoneDbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            
            builder.Services.AddDbContext<GamingZoneDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //Registering custom services for dependency injection.
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IDeveloperService, DeveloperService>();
            builder.Services.AddScoped<IPublisherService, PublisherService>();

            //Configuring Identity services with custom options from appsettings.Development.json using a helper method.
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                ConfigureIdentityOptions(options, builder.Configuration);

            })
                            .AddRoles<IdentityRole<Guid>>()
                            .AddEntityFrameworkStores<GamingZoneDbContext>();
            
            builder.Services.AddControllersWithViews();

            builder.Services.AddRazorPages();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        //Helper method to configure Identity options.
        private static void ConfigureIdentityOptions(IdentityOptions options, ConfigurationManager configuration)
        {
            //appsettings.Development.json configuration for Identity options used for development and testing purposes.
            //For production, we should be using appsettings.json configuration with more secure settings.

            //Sign in settings for Email and Phone confirmation used for development and testing purposes.
            options.SignIn.RequireConfirmedAccount = configuration.GetValue<bool>("IdentityOptions:SignIn:RequireConfirmedAccount");
            options.SignIn.RequireConfirmedEmail = configuration.GetValue<bool>("IdentityOptions:SignIn:RequireConfirmedEmail");
            options.SignIn.RequireConfirmedPhoneNumber = configuration.GetValue<bool>("IdentityOptions:SignIn:RequireConfirmedPhoneNumber");

            //Required unique email for each user.
            options.User.RequireUniqueEmail = configuration.GetValue<bool>("IdentityOptions:User:RequireUniqueEmail");

            //Password settings for registration and login used for development and testing purposes.
            options.Password.RequireDigit = configuration.GetValue<bool>("IdentityOptions:Password:RequireDigit");
            options.Password.RequireLowercase = configuration.GetValue<bool>("IdentityOptions:Password:RequireLowercase");
            options.Password.RequireNonAlphanumeric = configuration.GetValue<bool>("IdentityOptions:Password:RequireNonAlphanumeric");
            options.Password.RequireUppercase = configuration.GetValue<bool>("IdentityOptions:Password:RequireUppercase");
            options.Password.RequiredLength = configuration.GetValue<int>("IdentityOptions:Password:RequiredLength");

            //Lockout settings for user accounts used for development and testing purposes.
            options.Lockout.MaxFailedAccessAttempts = configuration.GetValue<int>("IdentityOptions:Lockout:MaxFailedAccessAttempts");
            options.Lockout.DefaultLockoutTimeSpan = configuration.GetValue<TimeSpan>("IdentityOptions:Lockout:DefaultLockoutTimeSpan");
        }
    }
}
