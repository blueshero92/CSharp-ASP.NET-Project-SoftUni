using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Developer;
using Microsoft.AspNetCore.Mvc;


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
    }
}
