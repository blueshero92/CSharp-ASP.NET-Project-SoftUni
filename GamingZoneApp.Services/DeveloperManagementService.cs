using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Developer;
using GamingZoneApp.Services.Models.Game;
using GamingZoneApp.ViewModels.Developer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace GamingZoneApp.Services.Core
{
    public class DeveloperManagementService : IDeveloperManagementService
    {
        private readonly GamingZoneDbContext dbContext;

        public DeveloperManagementService(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<bool> AddDeveloperAsync()
        {
            DeveloperInputModel? developerInputModel = new DeveloperInputModel();


            if (developerInputModel == null)
            {
                return false;
            }

            try
            {
                Developer developer = new Developer
                {
                    Name = developerInputModel.Name,
                    Description = developerInputModel.Description,
                    ImageUrl = developerInputModel.ImageUrl
                };

                await dbContext.AddAsync(developer);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        [HttpPost]
        public async Task<bool> AddDeveloperAsync(DeveloperInputModel developerInputModel)
        {
            try
            {
                Developer developer = new Developer
                {
                    Name = developerInputModel.Name,
                    Description = developerInputModel.Description,
                    ImageUrl = developerInputModel.ImageUrl
                };

                await dbContext.AddAsync(developer);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<DeveloperInputModel?> GetDeveloperForEditAsync(Guid developerId)
        {
            Developer? developer = await dbContext
                                        .Developers
                                        .SingleOrDefaultAsync(d => d.Id == developerId);

            if(developer == null)
            {
                return null;
            }

            DeveloperInputModel developerInputModel = new DeveloperInputModel
            {
                Name = developer.Name,
                Description = developer.Description,
                ImageUrl = developer.ImageUrl ?? null
            };

            return developerInputModel;
        }

        public async Task<bool> EditDeveloperAsync(Guid developerId, DeveloperInputModel inputModel)
        {
            Developer? developer = await dbContext
                                        .Developers
                                        .SingleOrDefaultAsync(d => d.Id == developerId);

            if (developer == null)
            {
                return false;
            }

            try
            {
                developer.Name = inputModel.Name;
                developer.Description = inputModel.Description;
                developer.ImageUrl = inputModel.ImageUrl;

                dbContext.Update(developer);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<DeleteDeveloperDto?> GetDeveloperForDeleteAsync(Guid developerId)
        {
            //Get the developer to delete from the database using the provided developerId.
            Developer? devToDelete = await dbContext
                                        .Developers
                                        .SingleOrDefaultAsync(d => d.Id == developerId);

            //If the developer is not found, return null to indicate that the developer does not exist.
            if (devToDelete == null)
            {
                return null;
            }

            //If the developer is found, create a DeleteDeveloperDto to return the necessary information for deletion.
            DeleteDeveloperDto deleteDeveloperDto = new DeleteDeveloperDto()
            {
                Name = devToDelete.Name
            };

            return deleteDeveloperDto;
        }


        public async Task<bool> HardDeleteDeveloperAsync(Guid developerId)
        {
            Developer? devToDelete = await dbContext
                                          .Developers
                                          .SingleOrDefaultAsync(d => d.Id == developerId);

            if (devToDelete == null)
            {
                return false;
            }

            try
            {
                dbContext.Remove(devToDelete);
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
