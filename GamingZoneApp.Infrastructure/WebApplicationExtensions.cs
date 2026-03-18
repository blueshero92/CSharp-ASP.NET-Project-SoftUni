using GamingZoneApp.Data.Seeding.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GamingZoneApp.Infrastructure
{
    public static class WebApplicationExtensions
    {
        //Use this extension method in Program.cs to seed the roles when the application starts.
        public static IApplicationBuilder UseRolesSeeder(this IApplicationBuilder applicationBuilder)
        {
            using IServiceScope serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            // Resolve the IIdentitySeeder service from the application's service provider.
            IIdentitySeeder identitiySeeder
                = serviceScope.ServiceProvider.GetRequiredService<IIdentitySeeder>();

            // Call the SeedRolesAsync method to seed the roles. Since this is an asynchronous method, we use GetAwaiter().GetResult() to wait for it to complete.
            identitiySeeder.SeedRolesAsync()
                           .GetAwaiter()
                           .GetResult();

            return applicationBuilder;
        }
    }
}
