using GamingZoneApp.Data;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;

using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Services.Core
{
    public class PublisherService : IPublisherService
    {
        private readonly GamingZoneDbContext dbContext;

        public PublisherService(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<AddGamePublisherViewModel>> GetAllPublishersAsync()
        {
            return await dbContext
                        .Publishers
                        .Select(p => new AddGamePublisherViewModel
                        {
                            Id = p.Id,
                            Name = p.Name
                        })
                        .ToListAsync();
        }

        public async Task<bool> PublisherExistsAsync(Guid publisherId)
        {
            return await dbContext
                        .Publishers.AnyAsync(p => p.Id == publisherId);
        }
    }
}
