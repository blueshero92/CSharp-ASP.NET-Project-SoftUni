using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;

using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Developer;

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
            //An input model is created to hold the data for the new developer.
            DeveloperInputModel? developerInputModel = new DeveloperInputModel();

            //If the input model is null, return false to indicate that the developer cannot be added.
            if (developerInputModel == null)
            {
                return false;
            }

            //If the input model is not null, attempt to create a new Developer entity using the data from the input model.
            try
            {
                //If the developer is successfully created, return true to indicate that the developer can be added; otherwise, return false.
                Developer developer = new Developer
                {
                    Name = developerInputModel.Name,
                    Description = developerInputModel.Description,
                    ImageUrl = developerInputModel.ImageUrl ?? null
                };

                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }

        }

        [HttpPost]
        public async Task<bool> AddDeveloperAsync(DeveloperInputModel developerInputModel)
        {
            //Try to create a new Developer entity using the data from the input model and add it to the database. If successful, return true; otherwise, return false.
            try
            {
                Developer developer = new Developer
                {
                    Name = developerInputModel.Name,
                    Description = developerInputModel.Description,
                    ImageUrl = developerInputModel.ImageUrl
                };

                //Add the new developer to the database context and save changes to persist it in the database.
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
            //Get the developer to edit from the database using the provided developerId.
            Developer ? developer = await dbContext
                                        .Developers
                                        .SingleOrDefaultAsync(d => d.Id == developerId);

            //If the developer is not found, return null to indicate that the developer does not exist.
            if (developer == null)
            {
                return null;
            }

            //If the developer is found, create a DeveloperInputModel to return the necessary information for editing.
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
            //Get the developer to edit from the database using the provided developerId.
            Developer? developer = await dbContext
                                        .Developers
                                        .SingleOrDefaultAsync(d => d.Id == developerId);

            //If the developer is not found, return false to indicate that the edit cannot proceed.
            if (developer == null)
            {
                return false;
            }


            //If the developer is found, attempt to update its properties with the data from the input model and save changes to the database. 
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
            //Get the developer to delete from the database using the provided developerId.
            Developer? devToDelete = await dbContext
                                          .Developers
                                          .SingleOrDefaultAsync(d => d.Id == developerId);

            //If the developer is not found, return false to indicate that the deletion cannot proceed.
            if (devToDelete == null)
            {
                return false;
            }

            //If the developer is found, attempt to remove it from the database and save changes. If successful, return true; otherwise, return false.
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
