using GamingZoneApp.Services.Core.Interfaces;
using GamingZoneApp.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;
using static GamingZoneApp.GCommon.Constants.AppConstants;
using static GamingZoneApp.GCommon.Constants.OutputMessages.TempDataSuccessMessages;
using static GamingZoneApp.GCommon.Constants.OutputMessages.GameControllerErrors;


namespace GamingZoneApp.Areas.Admin.Controllers
{
    public class GameManagementController : BaseAdminController
    {
        private readonly IGameService gameService;
        private readonly IGameManagementService gameManagementService;
        private readonly IDeveloperService developerService;
        private readonly IPublisherService publisherService;

        //gameService, developerService and publisherService are reused for getting the necessary data to display in the views.
        public GameManagementController(IGameService gameService, IGameManagementService gameManagementService,
                                       IDeveloperService developerService, IPublisherService publisherService)
        {
            this.gameService = gameService;
            this.gameManagementService = gameManagementService;
            this.developerService = developerService;
            this.publisherService = publisherService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //Using the service method to get all games and map them to a collection of AllGamesViewModel for display in the index view.
            IEnumerable<AllGamesViewModel> allGames =  await gameService.GetAllGamesAsync();

            //Passing the collection of AllGamesViewModel to the index view for display.
            return View(allGames);
        }

        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute(Name = "id")] Guid gameId)
        {
            //Using helper method from the game service(for reusability) to check if the game exists.
            if (!await gameService.GameExistsAsync(gameId))
            {
                return BadRequest();
            }

            //Using the service method to get the game details for editing.
            GameInputModel? gameInputModel = await gameManagementService.GetEditAsync(gameId);

            //If the game details are not found, redirect to the edit page.
            if (gameInputModel == null)
            {
                return RedirectToAction(nameof(Edit));
            }

            //Populating the developers and publishers collections for the dropdown lists in the edit view.
            gameInputModel.Developers = (await developerService.GetAllDevelopersAsync()).ToList();
            gameInputModel.Publishers = (await publisherService.GetAllPublishersAsync()).ToList();

            return View(gameInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute(Name = "id")] Guid gameId, GameInputModel inputModel)
        {
            if (!await gameService.GameExistsAsync(gameId))
            {
                return BadRequest();
            }

            //Validate the model state.
            if (!ModelState.IsValid)
            {
                //Populate developers and publishers again for the dropdown lists in case of validation errors.
                inputModel.Developers = (await developerService.GetAllDevelopersAsync()).ToList();
                inputModel.Publishers = (await publisherService.GetAllPublishersAsync()).ToList();

                return View(inputModel);
            }

            //Validate that the selected developer exists using helper method from the developer service.
            if (!await developerService.DeveloperExistsAsync(inputModel.DeveloperId))
            {
                ModelState.AddModelError(nameof(inputModel.DeveloperId), DeveloperDoesNotExistError);

                // Reload developers and publishers for the dropdowns
                inputModel.Developers = (await developerService.GetAllDevelopersAsync()).ToList();
                inputModel.Publishers = (await publisherService.GetAllPublishersAsync()).ToList();

                return View(inputModel);
            }

            //Validate that the selected publisher exists using helper method from the publisher service.
            if (!await publisherService.PublisherExistsAsync(inputModel.PublisherId))
            {
                ModelState.AddModelError(nameof(inputModel.PublisherId), PublisherDoesNotExistError);

                // Reload developers and publishers for the dropdowns
                inputModel.Developers = (await developerService.GetAllDevelopersAsync()).ToList();
                inputModel.Publishers = (await publisherService.GetAllPublishersAsync()).ToList();

                return View(inputModel);
            }

            //Using the service method to execute the editing of the game in the database.
            bool editSuccessful = await gameManagementService.PostEditAsync(gameId, inputModel);

            //If the editing was not successful, add a model error and return the view with the input model to display the error message.
            if (!editSuccessful)
            {
                ModelState.AddModelError(string.Empty, ErrorEditingGame);
                return View(inputModel);
            }

            //If the editing was successful, set a success message in TempData and redirect to the index page.
            TempData[SuccessTempDataKey] = GameEditedSuccessfullyMessage;
            return RedirectToAction(nameof(Index));

        }

        [HttpGet]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] Guid gameId)
        {
            //Using helper method from the game service(for reusability) to check if the game exists.   
            if (!await gameService.GameExistsAsync(gameId))
            {
                return BadRequest();
            }

            //Using DeleteGameViewModel to display the game details in the delete confirmation view.
            DeleteGameViewModel? deleteGameViewModel = await gameManagementService.GetDeleteAsync(gameId);

            //If the game details are not found, return a NotFound result.
            if (deleteGameViewModel == null)
            {
                return NotFound();
            }

            return View(deleteGameViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] Guid gameId, DeleteGameViewModel deleteGameViewModel)
        {
            //Using helper method from the game service(for reusability) to check if the game exists.
            if (!await gameService.GameExistsAsync(gameId))
            {
                return BadRequest();
            }

            //Using the service method to execute the deletion of the game in the database.
            bool deleteSuccessful = await gameManagementService.PostDeleteAsync(gameId, deleteGameViewModel);

            //If the deletion was not successful, add a model error and return the view with the deleteGameViewModel to display the error message.
            if (!deleteSuccessful)
            {
                ModelState.AddModelError(string.Empty, ErrorDeletingGame);
                return View(deleteGameViewModel);
            }

            //If the deletion was successful, set a success message in TempData and redirect to the index page.
            TempData[SuccessTempDataKey] = GameDeletedSuccessfullyMessage;
            return RedirectToAction(nameof(Index));
        }
    }
}

