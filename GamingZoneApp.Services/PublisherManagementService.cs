using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.Services.Models.Publisher;

using GamingZoneApp.ViewModels.Publisher;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<bool> AddPublisherAsync()
        {
            //An input model is created to hold the data for the new publisher.
            PublisherInputModel? publisherInputModel = new PublisherInputModel();

            //If the input model is null, return false.
            if (publisherInputModel == null)
            {
                return false;
            }

            //If the input model is not null, attempt to create a new publisher object and populate its properties with the values from the input model.
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

        [HttpPost]
        public async Task<bool> AddPublisherAsync(PublisherInputModel publisherInputModel)
        {
            //Try to create a new publisher object and populate its properties with the values from the input model. 
            try
            {
                Publisher publisher = new Publisher
                {
                    Name = publisherInputModel.Name,
                    Description = publisherInputModel.Description,
                    ImageUrl = publisherInputModel.ImageUrl ?? null
                };

                //If the publisher object is successfully created, add it to the database and save the changes.
                await dbContext.AddAsync(publisher);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PublisherInputModel?> GetPublisherForEditAsync(Guid publisherId)
        {
            //Check if the publisher exists.
            Publisher? publisher = await dbContext
                                        .Publishers
                                        .SingleOrDefaultAsync(p => p.Id == publisherId);

            //If the publisher doesn't exist, return null.
            if (publisher == null)
            {
                return null;
            }

            //If the publisher exists, create a new input model and populate its properties with the values from the publisher object, then return the input model.
            PublisherInputModel publisherInputModel = new()
            {
                Name = publisher.Name,
                Description = publisher.Description,
                ImageUrl = publisher.ImageUrl
            };

            return publisherInputModel;
        }
        public async Task<bool> EditPublisherAsync(Guid publisherId, PublisherInputModel inputModel)
        {
            //Check if the publisher exists.
            Publisher? publisher = await dbContext
                                        .Publishers
                                        .SingleOrDefaultAsync(p => p.Id == publisherId);

            //If the publisher doesn't exist, return false.
            if (publisher == null)
            {
                return false;
            }

            //Update the publisher's properties with the new values from the input model.
            try
            {

                publisher.Name = inputModel.Name;
                publisher.Description = inputModel.Description;
                publisher.ImageUrl = inputModel.ImageUrl ?? null;

                //If the publisher exists, update it in the database and save the changes.
                dbContext.Update(publisher);
                dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<DeletePublisherDto?> GetPublisherForDeleteAsync(Guid publisherId)
        {
            //Getting the publisher to delete by its Id.
            Publisher? publisherToDelete = await dbContext
                                                 .Publishers
                                                 .SingleOrDefaultAsync(p => p.Id == publisherId);

            //If the publisher doesn't exist, return null.
            if (publisherToDelete == null)
            {
                return null;
            }

            //If the publisher exists, create a new delete DTO and populate its properties with the values from the publisher object, then return the delete DTO.
            DeletePublisherDto deletePublisherDto = new()
            {
                Name = publisherToDelete.Name
            };

            return deletePublisherDto;
        }

        public async Task<bool> HardDeletePublisherAsync(Guid publisherId)
        {
            //Getting the publisher to delete by its Id.
            Publisher? publisherToDelete = await dbContext
                                                .Publishers
                                                .SingleOrDefaultAsync(p => p.Id == publisherId);

            //If the publisher doesn't exist, return false.
            if (publisherToDelete == null)
            {
                return false;
            }

            //If the publisher exists, remove it from the database and save the changes.
            try
            {
                dbContext.Publishers.Remove(publisherToDelete);
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
