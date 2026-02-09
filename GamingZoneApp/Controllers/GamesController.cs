using GamingZoneApp.Data;
using GamingZoneApp.ViewModels.Game;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;

using GamingZoneApp.Services.Core.Interfaces;


namespace GamingZoneApp.Controllers
{
    public class GamesController : Controller
    {

        private readonly GamingZoneDbContext dbContext;
        private readonly IGameService gameService;
        private readonly IDeveloperService developerService;
        private readonly IPublisherService publisherService;

        public GamesController(GamingZoneDbContext dbContext, IGameService gameService, 
                               IDeveloperService developerService, IPublisherService publisherService)
        {
            this.dbContext = dbContext;
            this.gameService = gameService;
            this.developerService = developerService;
            this.publisherService = publisherService;
        }

        //Visualize all games using a view model.
        [HttpGet]
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

            //Validate the model state.
            if (!ModelState.IsValid)
            {
                //Using helper methods from corresponding services to get all developers and publishers and return the view with the input model.
                inputModel.Developers = (await developerService.GetAllDevelopersAsync()).ToList();

                inputModel.Publishers = (await publisherService.GetAllPublishersAsync()).ToList();

                return View(inputModel);
            }

            //Validate that the selected developer exists using helper method from the developer service.
            if (!await developerService.DeveloperExistsAsync(inputModel.DeveloperId))
            {
                ModelState.AddModelError(nameof(inputModel.DeveloperId), "Selected developer does not exist.");

                return View(inputModel);
            }

            //Validate that the selected publisher exists using helper method from the publisher service.
            if (!await publisherService.PublisherExistsAsync(inputModel.PublisherId))
            {
                ModelState.AddModelError(nameof(inputModel.PublisherId), "Selected publisher does not exist.");

                return View(inputModel);
            }

            //Try to create and save the new game using the game service. Catch any exceptions and return the view with an error message.
            bool gameIsAdded = await gameService.AddGameAsync(inputModel);

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
        public IActionResult EditGame(Guid id)
        {
            if(!GameExists(id)) //Using helper method to check if the game exists.
            {
                return BadRequest();
            }

            //Retrieve the game to be edited.
            Game? selectedGame = dbContext
                                .Games
                                .Include(g => g.Developer)
                                .Include(g => g.Publisher)
                                .AsNoTracking()
                                .SingleOrDefault(g => g.Id == id);

            //If the game is not found, return NotFound.
            if (selectedGame == null)
            {
                return NotFound();
            }

            //The game input model to be passed to the view. This is the model we will edit.
            GameInputModel gameViewModel = new()
            {
                Title = selectedGame.Title,
                ReleaseDate = selectedGame.ReleaseDate,
                Genre = selectedGame.Genre.ToString(),
                Description = selectedGame.Description,
                ImageUrl = selectedGame.ImageUrl,
                DeveloperId = selectedGame.DeveloperId,
                Developers = GetAllDevelopers().ToList(),
                PublisherId = selectedGame.PublisherId,
                Publishers = GetAllPublishers().ToList()
            };


            return View(gameViewModel);
        }

        [HttpPost]
        public IActionResult EditGame(Guid id, GameInputModel inputModel)
        {
            //Check if the game exists in the database for it to be edited.
            if (!GameExists(id))
            {
                return BadRequest();
            }

            //Validate the model state.
            if (!ModelState.IsValid)
            {
                //Using helper methods to get all developers and publishers and return the view with the input model.

                inputModel.Developers = GetAllDevelopers().ToList();

                inputModel.Publishers = GetAllPublishers().ToList();

                return View(inputModel);
            }

            //Validate that the selected developer exists.
            if (!DeveloperExists(inputModel.DeveloperId))
            {
                ModelState.AddModelError(nameof(inputModel.DeveloperId), "Selected developer does not exist.");

                return View(inputModel);
            }

            //Validate that the selected publisher exists.
            if (!PublisherExists(inputModel.PublisherId))
            {
                ModelState.AddModelError(nameof(inputModel.PublisherId), "Selected publisher does not exist.");

                return View(inputModel);
            }

            Game? gameToEdit = dbContext
                                .Games
                                .Include(g => g.Developer)
                                .Include(g => g.Publisher)
                                .SingleOrDefault(g => g.Id == id);

            if(gameToEdit == null)
            {
                return NotFound();
            }

            //Try to update and save the edited game. Catch any exceptions and return the view with an error message.
            try
            {                
                gameToEdit.Title = inputModel.Title;
                gameToEdit.ReleaseDate = inputModel.ReleaseDate;
                gameToEdit.Genre = Enum.Parse<Genre>(inputModel.Genre);
                gameToEdit.Description = inputModel.Description;
                gameToEdit.ImageUrl = inputModel.ImageUrl;
                gameToEdit.DeveloperId = inputModel.DeveloperId;
                gameToEdit.PublisherId = inputModel.PublisherId;

                dbContext.Games.Update(gameToEdit);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                ModelState.AddModelError(string.Empty, "An error occurred while editing the game. Please try again.");

                return View(inputModel);
            }
        }

        //Visualize the Delete Game confirmation page.
        [HttpGet]
        public IActionResult DeleteGame(Guid id)
        {
            //Check if the game exists in the database for it to be deleted.
            if(!GameExists(id))
            {
                return BadRequest();
            }

            //Retrieve the game to be deleted.
            Game? gameToDelete = dbContext
                                .Games
                                .Include(g => g.Developer)
                                .Include(g => g.Publisher)
                                .AsNoTracking()
                                .SingleOrDefault(g => g.Id == id);

            //If the game is not found, return NotFound.
            if (gameToDelete == null)
            {
                return NotFound();
            }

            DeleteGameViewModel viewModel = new DeleteGameViewModel
            {
                Title = gameToDelete.Title,           
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteGame(Guid id, DeleteGameViewModel? viewModel)
        {
            //Check if the game exists in the database for it to be deleted.
            if(!GameExists(id))
            {
                return BadRequest();
            }

            //Retrieve the game to be deleted.
            Game? gameToDelete = dbContext
                                .Games
                                .Include(g => g.Developer)
                                .Include(g => g.Publisher)
                                .SingleOrDefault(g => g.Id == id);

            //If the game is not found, return NotFound.
            if (gameToDelete == null)
            {
                return NotFound();
            }

            //Try to delete the game. Catch any exceptions and return the view with an error message.
            try
            {
                dbContext.Games.Remove(gameToDelete);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                ModelState.AddModelError(string.Empty, "An error occurred while deleting the game. Please try again.");

                return View(viewModel);
            }
        }


        //Helper method to check if a game exists in the database.
        private bool GameExists(Guid gameId)
        {
            return dbContext.Games.Any(g => g.Id == gameId);
        }

        //Helper method to check if a developer exists in the database.
        private bool DeveloperExists(Guid developerId)
        {
            return dbContext.Developers.Any(d => d.Id == developerId);
        }

        //Helper method to check if a publisher exists in the database.
        private bool PublisherExists(Guid publisherId)
        {
            return dbContext.Publishers.Any(p => p.Id == publisherId);
        }

        //Helper method to get all developers from the database.
        private IEnumerable<AddGameDeveloperViewModel> GetAllDevelopers()
        {
            return dbContext
                  .Developers
                  .AsNoTracking()
                  .Select(d => new AddGameDeveloperViewModel
                  {
                      Id = d.Id,
                      Name = d.Name
                  })
                  .OrderBy(d => d.Name)
                  .ToList();
        }

        //Helper method to get all publishers from the database.
        private IEnumerable<AddGamePublisherViewModel> GetAllPublishers()
        {
           return dbContext
                 .Publishers
                 .AsNoTracking()
                 .Select(p => new AddGamePublisherViewModel
                 {
                     Id = p.Id,
                     Name = p.Name
                 })
                 .OrderBy(p => p.Name)
                 .ToList();
        }
    }
}

