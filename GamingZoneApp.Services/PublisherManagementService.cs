using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Publisher;
using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Publisher;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Services.Core
{
    public class PublisherManagementService : IPublisherManagementService
    {
        private readonly GamingZoneDbContext dbContext;

        public PublisherManagementService(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> AddPublisherAsync()
        {
            PublisherInputModel? publisherInputModel = new PublisherInputModel();

            if (publisherInputModel == null)
            {
                return false;
            }

            try
            {
                Publisher publisher = new Publisher
                {
                    Name = publisherInputModel.Name,
                    Description = publisherInputModel.Description,
                    ImageUrl = publisherInputModel.ImageUrl ?? null
                };

                return await Task.FromResult(true);
            }
            catch (Exception)
            {
                return await Task.FromResult(false);
            }
        }
        public async Task<bool> AddPublisherAsync(PublisherInputModel publisherInputModel)
        {
            try
            {
                Publisher publisher = new Publisher
                {
                    Name = publisherInputModel.Name,
                    Description = publisherInputModel.Description,
                    ImageUrl = publisherInputModel.ImageUrl ?? null
                };

                await dbContext.AddAsync(publisher);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Task<bool> EditPublisherAsync(Guid publisherId, PublisherInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public Task<DeletePublisherDto?> GetPublisherForDeleteAsync(Guid publisherId)
        {
            throw new NotImplementedException();
        }

        public Task<PublisherInputModel?> GetPublisherForEditAsync(Guid publisherId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HardDeletePublisherAsync(Guid publisherId)
        {
            throw new NotImplementedException();
        }
    }
}
