using GamingZoneApp.Data;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;

using Microsoft.EntityFrameworkCore;


namespace GamingZoneApp.Services.Core
{
    public class DeveloperService : IDeveloperService
    {
        private readonly GamingZoneDbContext dbContext;

        public DeveloperService(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<AddGameDeveloperViewModel>> GetAllDevelopersAsync()
        {
            return await dbContext
                        .Developers
                        .AsNoTracking()
                        .Select(d => new AddGameDeveloperViewModel
                        {
                            Id = d.Id,
                            Name = d.Name
                        })
                        .OrderBy(d => d.Name)
                        .ToListAsync();
        }

        public async Task<bool> DeveloperExistsAsync(Guid developerId)
        {
            return await dbContext
                        .Developers.AnyAsync(d => d.Id == developerId);
        }

    }
}
