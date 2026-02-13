using GamingZoneApp.ViewModels.Game;

using GamingZoneApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;



namespace GamingZoneApp.Controllers
{
    public class GamesController : BaseController
    {
        private readonly IGameService gameService;
        private readonly IDeveloperService developerService;
        private readonly IPublisherService publisherService;

        public GamesController(IGameService gameService, IDeveloperService developerService, IPublisherService publisherService)
        {
            this.gameService = gameService;
            this.developerService = developerService;
            this.publisherService = publisherService;
        }

        //Visualize all games using a view model.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            //Using the game service to retrieve all games and map them to the collection of AllGamesViewModel.
            IEnumerable<AllGamesViewModel> allGames
                = await gameService
                       .GetAllGamesAsync();

            //Return the view with the collection of AllGamesViewModel.
            return View(allGames);
        }

        //Visualize game details for an individual game using a view model.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GameDetails(Guid id)
        {
            //Using the game service to retrieve the game details by id.
            GameViewModel? selectedGame = await gameService
                                            .GetGameDetailsByIdAsync(id);


            //If the game is not found, return NotFound.
            //This is an addional validation to ensure the game exists even if we have already validated it in the GameService.
            if (selectedGame == null)
            {
                return NotFound();
            }

            return View(selectedGame);

        }

        //Visualize all games added by the current user using a view model.
        [HttpGet]
        public async Task<IActionResult> MyGames()
        {
            Guid userId = GetUserId();

            IEnumerable<AllGamesViewModel> myGames = await gameService.GetAllGamesByUserIdAsync(userId); 
            
            return View(myGames);
        }

        //Visualize the Add Game form with developers and publishers.
        [HttpGet]
        public async Task<IActionResult> AddGame()
        {
            //The view model to be passed to the view.
            //This model contains the lists of developers and publishers for the dropdowns.
            GameInputModel viewModel = new GameInputModel()
            {
                //Using helper methods from corresponding services to get all developers and publishers.
                Developers = (await developerService.GetAllDevelopersAsync()).ToList(),
                Publishers = (await publisherService.GetAllPublishersAsync()).ToList()
            };

            return View(viewModel);
        }

        //Process the Add Game form submission.
        [HttpPost]
        public async Task<IActionResult> AddGame(GameInputModel inputModel)
        {
            //Using helper method from the BaseController to get the current user's id.
            Guid userId = GetUserId();

            //Validate the model state.
            if (!ModelState.IsValid)
            {
                //Using helper method to get all developers and publishers.
                await PopulateDevelopersAndPublishersAsync(inputModel);

                return View(inputModel);
            }

            //Validate that the selected developer exists using helper method from the developer service.
            if (!await developerService.DeveloperExistsAsync(inputModel.DeveloperId))
            {
                ModelState.AddModelError(nameof(inputModel.DeveloperId), "Selected developer does not exist.");

                // Reload developers and publishers for the dropdowns
                await PopulateDevelopersAndPublishersAsync(inputModel);

                return View(inputModel);
            }

            //Validate that the selected publisher exists using helper method from the publisher service.
            if (!await publisherService.PublisherExistsAsync(inputModel.PublisherId))
            {
                ModelState.AddModelError(nameof(inputModel.PublisherId), "Selected publisher does not exist.");

                // Reload developers and publishers for the dropdowns
                await PopulateDevelopersAndPublishersAsync(inputModel);

                return View(inputModel);
            }

            //Try to create and save the new game using the game service. Catch any exceptions and return the view with an error message.
            bool gameIsAdded = await gameService.AddGameAsync(inputModel, userId);

            //If the game is not added successfully, return the view with an error message.
            if (!gameIsAdded)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while adding the game. Please try again.");
                return View(inputModel);
            }

            return RedirectToAction(nameof(Index));
        }

        //Visualize the Edit Game form with developers and publishers.
        [HttpGet]
        public async Task<IActionResult> EditGame(Guid id)
        {            
            Guid userId = GetUserId();

            if (!await gameService.GameExistsAsync(id)) //Using helper method to check if the game exists.
            {
                return BadRequest();
            }

            //After authentication, check if the user is the creator of the game using helper task from the game service.
            if (!await gameService.IsUserCreatorAsync(id, userId))
            {
                return BadRequest();
            }

            GameInputModel? gameInputModel = await gameService.GetGameForEditAsync(id, userId);            

            //If the game is not found, return NotFound.
            if (gameInputModel == null)
            {
                return RedirectToAction(nameof(EditGame));
            }

            gameInputModel.Developers = (await developerService.GetAllDevelopersAsync()).ToList();
            gameInputModel.Publishers = (await publisherService.GetAllPublishersAsync()).ToList();


            return View(gameInputModel);
        }

        //Process the Edit Game form submission.
        [HttpPost]
        public async Task<IActionResult> EditGame(Guid id, GameInputModel inputModel)
        {
            Guid userId = GetUserId();

            //Check if the game exists in the database for it to be edited using helper method from the game service.
            if (!await gameService.GameExistsAsync(id))
            {
                return BadRequest();
            }

            //After authentication, check if the user is the creator of the game using helper task from the game service same as in the GET EditGame action.
            if (!await gameService.IsUserCreatorAsync(id, userId))
            {
                return BadRequest();
            }

            //Validate the model state.
            if (!ModelState.IsValid)
            {
                //Using helper method to get all developers and publishers.
                await PopulateDevelopersAndPublishersAsync(inputModel);

                return View(inputModel);
            }

            //Validate that the selected developer exists using helper method from the developer service.
            if (!await developerService.DeveloperExistsAsync(inputModel.DeveloperId))
            {
                ModelState.AddModelError(nameof(inputModel.DeveloperId), "Selected developer does not exist.");

                // Reload developers and publishers for the dropdowns
                await PopulateDevelopersAndPublishersAsync(inputModel);

                return View(inputModel);
            }

            //Validate that the selected publisher exists using helper method from the publisher service.
            if (!await publisherService.PublisherExistsAsync(inputModel.PublisherId))
            {
                ModelState.AddModelError(nameof(inputModel.PublisherId), "Selected publisher does not exist.");

                // Reload developers and publishers for the dropdowns
                await PopulateDevelopersAndPublishersAsync(inputModel);

                return View(inputModel);
            }

            //Try to edit and save the game using the game service.
            bool gameIsEdited = await gameService.EditGameAsync(id, inputModel, userId);

            //If the game is not edited successfully, return the view with an error message.
            if (!gameIsEdited)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while editing the game. Please try again.");
                return View(inputModel);
            }

            return RedirectToAction(nameof(Index));
        }

        //Visualize the Delete Game confirmation page.
        [HttpGet]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            //Check if the game exists in the database for it to be deleted.
            if (!await gameService.GameExistsAsync(id))
            {
                return BadRequest();
            }

            //Retrieve the game to be deleted using the game service and map it to the DeleteGameViewModel.         
            DeleteGameViewModel? viewModel = await gameService.GetGameForDeleteAsync(id);

            //If the game is not found, return NotFound.
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteGame(Guid id, DeleteGameViewModel? viewModel)
        {
            //Check if the game exists in the database for it to be deleted.
            if (!await gameService.GameExistsAsync(id))
            {
                return BadRequest();
            }

            //Retrieve the game to be deleted.
            bool gameIsDeleted = await gameService.DeleteGameAsync(id);

            //Checks if the game is deleted successfully. If not, return the view with an error message.
            if (!gameIsDeleted)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the game. Please try again.");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        //A helper method to populate the developers and publishers for the dropdowns in the Add and Edit views.
        private async Task PopulateDevelopersAndPublishersAsync(GameInputModel inputModel)
        {
            inputModel.Developers = (await developerService.GetAllDevelopersAsync()).ToList();
            inputModel.Publishers = (await publisherService.GetAllPublishersAsync()).ToList();
        }
    }

        
}


