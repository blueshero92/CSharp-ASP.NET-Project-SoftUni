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
                ?? throw new InvalidOperationException(AdminUsernameNotFoundInConfigurationExceptionMessage);

            string adminEmail = configuration["UserSeed:AdminUser:Email"]
                ?? throw new InvalidOperationException(AdminEmailNotFoundInConfigurationExceptionMessage);

            string adminPassword = configuration["UserSeed:AdminUser:Password"]
                ?? throw new InvalidOperationException(AdminPasswordNotFoundInConfigurationExceptionMessage);

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

        public async Task SeedModeratorUserAsync()
        {
            string moderatorUsername = configuration["UserSeed:ModeratorUser:Username"]
                ?? throw new InvalidOperationException(ModeratorUsernameNotFoundInConfigurationExceptionMessage);

            string moderatorEmail = configuration["UserSeed:ModeratorUser:Email"]
                ?? throw new InvalidOperationException(ModeratorEmailNotFoundInConfigurationExceptionMessage);

            string moderatorPassword = configuration["UserSeed:ModeratorUser:Password"]
                ?? throw new InvalidOperationException(ModeratorPasswordNotFoundInConfigurationExceptionMessage);

            ApplicationUser? moderatorUser = await userManager.FindByEmailAsync(moderatorEmail);
            if (moderatorUser == null)
            {
                moderatorUser = new ApplicationUser
                {
                    UserName = moderatorUsername,
                    Email = moderatorEmail
                };

                IdentityResult result
                    = await userManager.CreateAsync(moderatorUser, moderatorPassword);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(string.Format(ErrorWhileTryingToSeedModeratorUserExceptionMessage, moderatorEmail));
                }
            }
            bool isInRole = await userManager.IsInRoleAsync(moderatorUser, AppRoles[1]);

            if (!isInRole)
            {
                IdentityResult addToRoleResult
                    = await userManager.AddToRoleAsync(moderatorUser, AppRoles[1]);

                if (!addToRoleResult.Succeeded)
                {
                    throw new InvalidOperationException(string.Format(ErrorWhileTryingToAddModeratorUserToModeratorRoleExceptionMessage, moderatorEmail));
                }
            }
        }
    }
}

