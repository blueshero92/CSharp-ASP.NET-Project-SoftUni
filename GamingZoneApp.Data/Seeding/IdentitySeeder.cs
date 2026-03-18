using GamingZoneApp.Data.Seeding.Interfaces;

using static GamingZoneApp.GCommon.Constants.ExceptionMessages;
using Microsoft.AspNetCore.Identity;

namespace GamingZoneApp.Data.Seeding
{
    public class IdentitySeeder : IIdentitySeeder
    {
        //Roles for seeding.
        private readonly string[] AppRoles = new string[]
        {
            "Admin",
            "User"
        };

        private readonly RoleManager<IdentityRole<Guid>> roleManager;

        //Injecting RoleManager for managing roles in the database.
        public IdentitySeeder(RoleManager<IdentityRole<Guid>> roleManager)
        {
            this.roleManager = roleManager;
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
    }
}
