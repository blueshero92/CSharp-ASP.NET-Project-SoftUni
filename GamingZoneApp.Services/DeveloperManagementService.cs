using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Repository.Interfaces;
using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Admin.Developer;
using GamingZoneApp.ViewModels.Developer;


namespace GamingZoneApp.Services.Core
{
    public class DeveloperManagementService : IDeveloperManagementService
    {
        private readonly IDeveloperRepository developerRepository;

        public DeveloperManagementService(IDeveloperRepository developerRepository)
        {
            this.developerRepository = developerRepository;
        }
     
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
                await developerRepository.CreateDeveloperAsync(developer);

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
            Developer ? developer = await developerRepository.GetDeveloperByIdAsync(developerId);

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
            Developer? developer = await developerRepository.GetDeveloperByIdAsync(developerId);

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

                await developerRepository.UpdateDeveloperAsync(developer);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<DeleteDeveloperViewModel?> GetDeveloperForDeleteAsync(Guid developerId)
        {
            //Get the developer to delete from the database using the provided developerId.
            Developer? devToDelete = await developerRepository.GetDeveloperByIdAsync(developerId);

            //If the developer is not found, return null to indicate that the developer does not exist.
            if (devToDelete == null)
            {
                return null;
            }

            //If the developer is found, create a DeleteDeveloperViewModel to return the necessary information for deletion.
            DeleteDeveloperViewModel deleteDeveloperViewModel = new DeleteDeveloperViewModel()
            {
                Name = devToDelete.Name
            };

            return deleteDeveloperViewModel;
        }


        public async Task<bool> HardDeleteDeveloperAsync(Guid developerId)
        {
            //Get the developer to delete from the database using the provided developerId.
            Developer? devToDelete = await developerRepository.GetDeveloperByIdAsync(developerId);

            //If the developer is not found, return false to indicate that the deletion cannot proceed.
            if (devToDelete == null)
            {
                return false;
            }

            //If the developer is found, attempt to remove it from the database and save changes. If successful, return true; otherwise, return false.
            try
            {
                await developerRepository.DeleteDeveloperAsync(devToDelete);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
