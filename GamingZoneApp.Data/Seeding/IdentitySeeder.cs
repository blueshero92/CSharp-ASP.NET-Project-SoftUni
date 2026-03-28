using GamingZoneApp.Data.Seeding.Interfaces;

using static GamingZoneApp.GCommon.Constants.ExceptionMessages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using GamingZoneApp.Data.Models;

namespace GamingZoneApp.Data.Seeding
{
    public class IdentitySeeder : IIdentitySeeder
    {
        //Roles for seeding.
        private readonly string[] AppRoles = new string[]
        {
            "Admin",
            "Moderator",
            "User"
        };

        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        //Injecting RoleManager for managing roles in the database.
        public IdentitySeeder(RoleManager<IdentityRole<Guid>> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }


        // This method seeds the roles defined in the AppRoles array into the database if they do not already exist.
        public async Task SeedRolesAsync()
        {
            foreach (string role in AppRoles)
            {
                // Check if the role already exists in the database to avoid duplicates.
                bool roleExists = await this.roleManager.RoleExistsAsync(role);

                // If the role does not exist, create a new IdentityRole and add it to the database.
                if (!roleExists)
                {
                    IdentityRole<Guid> newRole = new IdentityRole<Guid>(role);

                    IdentityResult idnetityRoleResult =
                         await roleManager.CreateAsync(newRole);

                    if (!idnetityRoleResult.Succeeded)
                    {
                        throw new InvalidOperationException(string.Format(RoleSeedingExceptionMessage, role));
                    }
                }
            }
        }

        public async Task SeedAdminUserAsync()
        {
            string adminUsername = configuration["UserSeed:AdminUser:Username"]
                ?? throw new InvalidOperationException(UsernameNotFoundInConfigurationExceptionMessage);

            string adminEmail = configuration["UserSeed:AdminUser:Email"]
                ?? throw new InvalidOperationException(EmailNotFoundInConfigurationExceptionMessage);

            string adminPassword = configuration["UserSeed:AdminUser:Password"]
                ?? throw new InvalidOperationException(PasswordNotFoundInConfigurationExceptionMessage);

            ApplicationUser? adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUsername,
                    Email = adminEmail
                };

                IdentityResult result
                    = await userManager.CreateAsync(adminUser, adminPassword);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(ErrorWhileTryingToSeedAdminUserExceptionMessage);
                }
            }

            bool isInRole = await userManager.IsInRoleAsync(adminUser, AppRoles[0]);

            if (!isInRole)
            {
                IdentityResult addToRoleResult
                    = await userManager.AddToRoleAsync(adminUser, AppRoles[0]);

                if (!addToRoleResult.Succeeded)
                {
                    throw new InvalidOperationException(ErrorWhileTryingToAddAdminUserToAdminRoleExceptionMessage);
                }
            }
        }
    }
}

