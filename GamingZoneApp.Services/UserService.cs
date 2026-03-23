using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Services.Core
{
    public class UserService : IUserService
    {
        private readonly GamingZoneDbContext dbContext;

        public UserService(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            //Retrieve all users from the database.
            IEnumerable<ApplicationUser> allUsers = await dbContext.Users.ToListAsync();

            //Retrieve all roles from the database.
            IEnumerable<IdentityRole<Guid>> roles = await dbContext.Roles.ToListAsync();

            //Retrieve all user-role relationships from the database.
            IEnumerable<IdentityUserRole<Guid>> userRoles = await dbContext.UserRoles.ToListAsync();


            //Map each user to a UserViewModel, including their roles.
            IEnumerable<UserViewModel> usersVm = allUsers.Select(u => new UserViewModel
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Roles = userRoles
                       .Where(ur => ur.UserId == u.Id)
                       .Select(ur => roles
                                    .FirstOrDefault(r => r.Id == ur.RoleId)?.Name ?? string.Empty)
                       .Where(name => !string.IsNullOrEmpty(name))
                       .ToList()
            })
           .ToList();


            return usersVm;

        }
    }
}
