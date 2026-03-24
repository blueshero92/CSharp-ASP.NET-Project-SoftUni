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


        public async Task<IEnumerable<string>> GetAllRolesAsync()
        {
            IEnumerable<string?> roles = await dbContext
                                             .Roles
                                             .Select(r => r.Name)
                                             .ToListAsync();

            return roles!;
        }

        public async Task<bool> AssignRoleAsync(Guid userId, string roleName)
        {
            // Check if the user exists in the database using the provided userId.
            ApplicationUser? user = await dbContext
                                         .Users
                                         .SingleOrDefaultAsync(u => u.Id == userId);

            // If the user does not exist, return false to indicate that the role assignment cannot proceed.
            if (user == null)
            {
                return false;
            }

            // Check if the role exists in the database using the provided roleName.
            IdentityRole<Guid>? role = await dbContext
                                            .Roles
                                            .SingleOrDefaultAsync(r => r.Name == roleName);

            // If the role does not exist, return false to indicate that the role assignment cannot proceed.
            if (role == null)
            {
                return false;
            }

            // Check if the user is already assigned to the specified role by querying the UserRoles mapping table.
            bool isAlreadyInRole = await dbContext
                                        .UserRoles
                                        .AnyAsync(ur => ur.UserId == userId && ur.RoleId == role.Id);

            // If the user is already in the role, return false to indicate that the role assignment cannot proceed.
            if (isAlreadyInRole)
            {
                return false;
            }

           
            try
            {
                //If everything else passes, assign the role to the user by assigning RoleId and UserId in the mapping table.
                IdentityUserRole<Guid> userRole = new IdentityUserRole<Guid>
                {
                    UserId = userId,
                    RoleId = role.Id
                };

                // Save the changes to the database to persist the new user-role assignment.
                dbContext.UserRoles.Add(userRole);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                // If any exceptions occur, return false to indicate that the role assignment failed.
                return false;
            }
        }

        public async Task<bool> RemoveRoleAsync(Guid userId, string roleName)
        {
            //Check if the role exists in the database using the provided roleName.
            IdentityRole<Guid>? role = await dbContext
                                            .Roles
                                            .SingleOrDefaultAsync(r => r.Name == roleName);

            //If the role does not exist, return false to indicate that the role removal cannot proceed.
            if (role == null)
            {
                return false;
            }

            //Check if the user is assigned to the specified role by querying the UserRoles mapping table.
            IdentityUserRole<Guid>? userRole = await dbContext
                                                    .UserRoles
                                                    .SingleOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == role.Id);

            //If the user is not in the role, return false to indicate that the role removal cannot proceed.
            if (userRole == null)
            {
                return false;
            }


            try
            {
                //If everything else passes, remove the role from the user by removing the UserRole mapping.
                dbContext.UserRoles.Remove(userRole);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<DeleteUserViewModel?> GetUserForDeletionAsync(Guid userId)
        {
            ApplicationUser? userToDelete = await dbContext
                                                 .Users
                                                 .SingleOrDefaultAsync(u => u.Id == userId);

            if (userToDelete == null)
            {
                return null;
            }

            DeleteUserViewModel userForDeletionVm = new DeleteUserViewModel
            {
                Id = userToDelete.Id,
                Username = userToDelete.UserName!,
                Email = userToDelete.Email!
            };

            return userForDeletionVm;
        }


        public async Task<bool> DeleteUserAsync(Guid userId)
        {
            //Check if the user exists in the database using the provided userId.
            ApplicationUser? user = await dbContext
                                         .Users
                                         .SingleOrDefaultAsync(u => u.Id == userId);

            //If the user does not exist, return false to indicate that the user deletion cannot proceed.
            if (user == null)
            {
                return false;
            }

            try
            {
                //If everything else passes, delete the user from the database.
                dbContext.Users.Remove(user);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
