using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Services.Core.Interfaces;
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
    }
}
