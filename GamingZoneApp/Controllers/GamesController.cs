using GamingZoneApp.ViewModels.Game;

using GamingZoneApp.Services.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using static GamingZoneApp.GCommon.Constants.OutputMessages.TempDataSuccessMessages;
using static GamingZoneApp.GCommon.Constants.OutputMessages.GameControllerErrors;
using static GamingZoneApp.GCommon.Constants.AppConstants;


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
        public async Task<IActionResult> Index(string? searchQuery)
        {
            //Using the game service to retrieve all games and map them to the collection of AllGamesViewModel.
            IEnumerable<AllGamesViewModel> allGames;

            //If there is no search query, retrieve all games as usual.
            //If there is a search query, use the game service to search for games by the search query.
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                allGames = await gameService.GetAllGamesAsync();
            }
            else
            {
                allGames = await gameService.SearchGamesAsync(searchQuery);
            }

            //Passing the search query to the view through ViewData to be able to display it in the search box after the search is performed.
            ViewData["SearchQuery"] = searchQuery?.Trim();

            //Return the view with the collection of AllGamesViewModel.
            return View(allGames);
        }

        //Visualize game details for an individual game using a view model.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GameDetails(Guid id)
        {

            //Using the game service to retrieve the game details by the game's Id and map it to the GameViewModel.
            GameViewModel? gameViewModel = await gameService.GetGameDetailsByIdAsync(id);


            //If the game is not found, return NotFound.
            //This is an addional validation to ensure the game exists even if we have already validated it in the GameService.
            if (gameViewModel == null)
            {
                return NotFound();
            }

            return View(gameViewModel);

        }

        //Visualize all games added by the current user using a view model.
        [HttpGet]
        public async Task<IActionResult> MyGames()
        {
            //Using helper method from the BaseController to get the current user's id.
            Guid userId = GetUserId();

            //Using the game service to retrieve all games added by the current user and map them to the collection of AllGamesViewModel.
            IEnumerable<AllGamesViewModel> myGames = await gameService.GetAllGamesByUserIdAsync(userId);

            //Return the view with the collection of AllGamesViewModel.
            return View(myGames);
        }

        //Visualize all games added to favorites by the current user using a view model.
        [HttpGet]
        public async Task<IActionResult> MyFavoriteGames()
        {
            //Using helper method from the BaseController to get the current user's id.
            Guid userId = GetUserId();

            //Using the game service to retrieve all games added to favorites by the current user and map them to the collection of AllGamesViewModel.
            IEnumerable<AllGamesViewModel> myFavoriteGames = await gameService.GetFavoriteGamesByUserIdAsync(userId);

            //Return the view with the collection of AllGamesViewModel.
            return View(myFavoriteGames);
        }

        //Add a game to the favorites of the current user by the game's Id.
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(Guid gameId)
        {
            //Using helper method from the BaseController to get the current user's id.
            Guid userId = GetUserId();

            //Validate that the game exists using helper method from the game service.
            if (!await gameService.GameExistsAsync(gameId))
            {
                return BadRequest();
            }

            //Validate that the game is not already in the favorites of the user using helper method from the game service.
            if (await gameService.IsGameInFavoritesAsync(gameId, userId))
            {
                //If the game is already in favorites, return the view with an error notification.
                TempData[ErrorTempDataKey] = GameAlreadyInFavoritesError;
                return RedirectToAction(nameof(MyFavoriteGames));
            }

            //Validate that the user is not the creator of the game using helper method from the game service. A user cannot add their own game to favorites.
            if (await gameService.IsUserCreatorAsync(gameId, userId))
            {
                //If the user is the creator of the game, return the view with an error notification.
                TempData[ErrorTempDataKey] = OwnGameCannotBeAddedToFavoritesError;
                return RedirectToAction(nameof(MyFavoriteGames));
            }

            //Try to add the game to favorites using the game service.
            bool isAddedToFavorites = await gameService.AddGameToFavoritesAsync(gameId, userId);

            //If the game is not added to favorites successfully, return the view with an error message.
            if (!isAddedToFavorites)
            {
                //If the game is not added to favorites successfully, return the view with an error notification.
                TempData[ErrorTempDataKey] = ErrorAddingGameToFavorites;
                return RedirectToAction(nameof(GameDetails), new { id = gameId });
            }

            //If the game is added to favorites successfully, redirect to the MyFavoriteGames action with a success notification.
            TempData[SuccessTempDataKey] = GameAddedToFavoritesSuccessfullyMessage;
            return RedirectToAction(nameof(MyFavoriteGames));
        }

        //Remove game from favorites of the current user by the game's Id and the user's Id.
        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(Guid gameId)
        {
            //Using helper method from the BaseController to get the current user's id.
            Guid userId = GetUserId();

            //Validate that the game exists using helper method from the game service.
            if (!await gameService.GameExistsAsync(gameId))
            {
                return BadRequest();
            }

            //Validate that the game is not in the favorites of the user using helper method from the game service.
            if (!await gameService.IsGameInFavoritesAsync(gameId, userId))
            {
                //Check if user is the creator. A user cannot remove their own game from favorites because they cannot add it in the first place.
                if (await gameService.IsUserCreatorAsync(gameId, userId))
                {
                    //If the user is the creator of the game, return the view with an error notification.
                    TempData[ErrorTempDataKey] = OwnGameCannotbeRemovedFromFavoritesError;
                    return RedirectToAction(nameof(MyFavoriteGames));
                }

                //If the game is not in favorites, return the view with an error notification.
                TempData[ErrorTempDataKey] = GameNotInFavoritesError;
                return RedirectToAction(nameof(MyFavoriteGames));
            }

            //Try to remove the game from favorites using the game service.
            bool isRemovedFromFavorites = await gameService.RemoveGameFromFavoritesAsync(gameId, userId);

            //If the game is not removed from favorites successfully, return the view with an error message.
            if (!isRemovedFromFavorites)
            {
                //If the game is not removed from favorites successfully, return the view with an error notification.
                TempData[ErrorTempDataKey] = ErrorRemovingGameFromFavorites;
                return RedirectToAction(nameof(GameDetails), new { id = gameId });
            }

            //If the game is removed from favorites successfully, redirect to the MyFavoriteGames action with a success notification.
            TempData[SuccessTempDataKey] = GameRemovedFromFavoritesSuccessfullyMessage;
            return RedirectToAction(nameof(MyFavoriteGames));
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
                ModelState.AddModelError(nameof(inputModel.DeveloperId), DeveloperDoesNotExistError);

                // Reload developers and publishers for the dropdowns
                await PopulateDevelopersAndPublishersAsync(inputModel);

                return View(inputModel);
            }

            //Validate that the selected publisher exists using helper method from the publisher service.
            if (!await publisherService.PublisherExistsAsync(inputModel.PublisherId))
            {
                ModelState.AddModelError(nameof(inputModel.PublisherId), PublisherDoesNotExistError);

                // Reload developers and publishers for the dropdowns
                await PopulateDevelopersAndPublishersAsync(inputModel);

                return View(inputModel);
            }

            //Try to create and save the new game using the game service. Catch any exceptions and return the view with an error message.
            bool gameIsAdded = await gameService.AddGameAsync(inputModel, userId);

            //If the game is not added successfully, return the view with an error message.
            if (!gameIsAdded)
            {
                ModelState.AddModelError(string.Empty, ErrorAddingGame);
                return View(inputModel);
            }

            //If the game is added successfully, redirect to the MyGames action with a success notification.
            TempData[SuccessTempDataKey] = GameAddedSuccessfullyMessage;
            return RedirectToAction(nameof(MyGames));
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
                ModelState.AddModelError(string.Empty, NotAuthorizedToEditGameError);
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            GameInputModel? gameInputModel = await gameService.GetGameForEditAsync(id, userId);            

            
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
                ModelState.AddModelError(string.Empty, NotAuthorizedToEditGameError);
                return StatusCode(StatusCodes.Status403Forbidden);
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
                ModelState.AddModelError(nameof(inputModel.DeveloperId), DeveloperDoesNotExistError);

                // Reload developers and publishers for the dropdowns
                await PopulateDevelopersAndPublishersAsync(inputModel);

                return View(inputModel);
            }

            //Validate that the selected publisher exists using helper method from the publisher service.
            if (!await publisherService.PublisherExistsAsync(inputModel.PublisherId))
            {
                ModelState.AddModelError(nameof(inputModel.PublisherId), PublisherDoesNotExistError);

                // Reload developers and publishers for the dropdowns
                await PopulateDevelopersAndPublishersAsync(inputModel);

                return View(inputModel);
            }

            //Try to edit and save the game using the game service.
            bool gameIsEdited = await gameService.EditGameAsync(id, inputModel, userId);

            //If the game is not edited successfully, return the view with an error message.
            if (!gameIsEdited)
            {
                ModelState.AddModelError(string.Empty, ErrorEditingGame);
                return View(inputModel);
            }

            //If the game is edited successfully, redirect to the Index action with a success notification.
            TempData[SuccessTempDataKey] = GameEditedSuccessfullyMessage;
            return RedirectToAction(nameof(Index));
        }

        //Visualize the Delete Game confirmation page.
        [HttpGet]
        public async Task<IActionResult> DeleteGame(Guid id)
        {
            Guid userId = GetUserId();

            //Check if the game exists in the database for it to be deleted.
            if (!await gameService.GameExistsAsync(id))
            {
                return BadRequest();
            }

            //After authentication, check if the user is the creator of the game using helper task from the game service.
            if (!await gameService.IsUserCreatorAsync(id, userId))
            {
                ModelState.AddModelError(string.Empty, NotAuthorizedToDeleteGameError);
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            //Retrieve the game to be deleted and map it to the DeleteGameViewModel to be passed to the view.
            DeleteGameViewModel? viewModel = await gameService.GetGameForDeleteAsync(id, userId);

            //If the game is not found, return NotFound.
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        //Process the Delete Game confirmation.
        [HttpPost]
        public async Task<IActionResult> DeleteGame(Guid id, DeleteGameViewModel? viewModel)
        {
            Guid userId = GetUserId();

            //Check if the game exists in the database for it to be deleted.
            if (!await gameService.GameExistsAsync(id))
            {
                return BadRequest();
            }

            //After authentication, check if the user is the creator of the game using helper task from the game service.
            if (!await gameService.IsUserCreatorAsync(id, userId))
            {
                ModelState.AddModelError(string.Empty, NotAuthorizedToDeleteGameError);
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            //Retrieve the game to be deleted.
            //Note: Soft and hard delete can be interchaned here by simply switching the task from the game service. The view and the rest of the code will work with both approaches without any changes.
            bool gameIsDeleted = await gameService.SoftDeleteGameAsync(id, userId);

            //Checks if the game is deleted successfully. If not, return the view with an error message.
            if (!gameIsDeleted)
            {
                ModelState.AddModelError(string.Empty, ErrorDeletingGame);
                return View(viewModel);
            }

            //If the game is deleted successfully, redirect to the Index action with a success notification.
            TempData[SuccessTempDataKey] = GameDeletedSuccessfullyMessage;
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


