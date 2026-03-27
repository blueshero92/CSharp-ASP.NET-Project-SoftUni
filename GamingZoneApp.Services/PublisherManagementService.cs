using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.Publisher;
using GamingZoneApp.ViewModels.Publisher;

namespace GamingZoneApp.Services.Core
{
    public class PublisherManagementService : IPublisherManagementService
    {

        private readonly IPublisherRepository publisherRepository;

        public PublisherManagementService(IPublisherRepository publisherRepository)
        {
            this.publisherRepository = publisherRepository;
        }

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
                await publisherRepository.CreatePublisherAsync(publisher);

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
            Publisher? publisher = await publisherRepository.GetPublisherByIdAsync(publisherId);

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
            Publisher? publisher = await publisherRepository.GetPublisherByIdAsync(publisherId);

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
                await publisherRepository.UpdatePublisherAsync(publisher);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<DeletePublisherViewModel?> GetPublisherForDeleteAsync(Guid publisherId)
        {
            //Getting the publisher to delete by its Id.
            Publisher? publisherToDelete = await publisherRepository.GetPublisherByIdAsync(publisherId);

            //If the publisher doesn't exist, return null.
            if (publisherToDelete == null)
            {
                return null;
            }

            //If the publisher exists, create a new delete view model and populate its properties with the values from the publisher object, then return the delete view model.
            DeletePublisherViewModel deletePublisherViewModel = new()
            {
                Name = publisherToDelete.Name
            };

            return deletePublisherViewModel;
        }

        public async Task<bool> HardDeletePublisherAsync(Guid publisherId)
        {
            //Getting the publisher to delete by its Id.
            Publisher? publisherToDelete = await publisherRepository.GetPublisherByIdAsync(publisherId);

            //If the publisher doesn't exist, return false.
            if (publisherToDelete == null)
            {
                return false;
            }

            //If the publisher exists, remove it from the database and save the changes.
            try
            {
                await publisherRepository.DeletePublisherAsync(publisherToDelete);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
