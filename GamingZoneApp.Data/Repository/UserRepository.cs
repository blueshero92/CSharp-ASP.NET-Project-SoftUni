using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly GamingZoneDbContext dbContext;

        public UserRepository(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<IEnumerable<UserAllDto>> GetAllUsersWithTheirRolesAsync()
        {
            //Retrieve all users from the database.
            IEnumerable<ApplicationUser> allUsers = await dbContext.Users.ToListAsync();

            //Retrieve all roles from the database.
            IEnumerable<IdentityRole<Guid>> allRoles = await dbContext.Roles.ToListAsync();

            //Retrieve all user-role relationships from the database.
            IEnumerable<IdentityUserRole<Guid>> allUserRoles = await dbContext.UserRoles.ToListAsync();

            //Map each user to a UserViewModel, including their roles.
            IEnumerable<UserAllDto> userAllDto = allUsers.Select(u => new UserAllDto
            {
                Id = u.Id,
                Username = u.UserName,
                Email = u.Email,
                Roles = allUserRoles
                       .Where(ur => ur.UserId == u.Id)
                       .Select(ur => allRoles
                                    .FirstOrDefault(r => r.Id == ur.RoleId)?.Name ?? string.Empty)
                       .Where(name => !string.IsNullOrEmpty(name))
                       .ToList()
            })
           .ToList();


            return userAllDto;
        }

        public async Task<IEnumerable<string?>> GetAllRolesByNameAsync()
        {
            IEnumerable<string?> roles = await dbContext
                                             .Roles
                                             .Select(r => r.Name)
                                             .ToListAsync();

            return roles!;
        }

        public async Task<bool> AssignRoleToUserAsync(Guid userId, string roleName)
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

        public async Task<bool> RemoveRoleFromUserAsync(Guid userId, string roleName)
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

        public async Task<DeleteUserDto> GetDeleteUserAsync(Guid userId)
        {
            //Check if the user exists in the database using the provided userId.
            ApplicationUser? userToDelete = await dbContext
                                                 .Users
                                                 .SingleOrDefaultAsync(u => u.Id == userId);

            //If the user does not exist, return null to indicate that the user deletion cannot proceed.
            if (userToDelete == null)
            {
                return null!;
            }

            //If the user exists, map the user's details to a DeleteUserDto and return it.
            DeleteUserDto userForDeletionDto = new DeleteUserDto
            {
                Id = userToDelete.Id,
                Username = userToDelete.UserName!,
                Email = userToDelete.Email!
            };

            // Return the DeleteUserDto containing the user's details for deletion confirmation.
            return userForDeletionDto;
        }

        public async Task<bool> PostDeleteUserAsync(Guid userId)
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

                //Remove all games created by the user you want to delete.
                //Ignore global query filter to include soft-deleted games in the deletion process.
                IEnumerable<Game> gamesByUser = await dbContext
                                                     .Games
                                                     .IgnoreQueryFilters()
                                                     .Where(g => g.UserId == userId)
                                                     .ToListAsync();

                dbContext.Games.RemoveRange(gamesByUser);


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
