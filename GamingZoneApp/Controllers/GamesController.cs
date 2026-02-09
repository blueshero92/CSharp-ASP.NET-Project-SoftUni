using GamingZoneApp.Data;
using GamingZoneApp.ViewModels.Game;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;

using static GamingZoneApp.GCommon.Constants.AppConstants;
using System.Globalization;
using GamingZoneApp.Services.Core.Interfaces;
using System.Threading.Tasks;


namespace GamingZoneApp.Controllers
{
    public class GamesController : Controller
    {

        private readonly GamingZoneDbContext dbContext;
        private readonly IGameService gameService;

        public GamesController(GamingZoneDbContext dbContext, IGameService gameService)
        {
            this.dbContext = dbContext;
            this.gameService = gameService;
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
        public IActionResult AddGame()
        {
            //The view model to be passed to the view.
            //This model contains the lists of developers and publishers for the dropdowns.
            GameInputModel viewModel = new GameInputModel()
            {
                //Using helper methods to get all developers and publishers.
                Developers = GetAllDevelopers().ToList(),
                Publishers = GetAllPublishers().ToList() 
            };

            return View(viewModel);
        }

        //Process the Add Game form submission.
        [HttpPost]
        public IActionResult AddGame(GameInputModel inputModel)
        {

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

            //Try to create and save the new game. Catch any exceptions and return the view with an error message.
            try
            {
                Game newGame = new Game
                {
                    Title = inputModel.Title,
                    ReleaseDate = inputModel.ReleaseDate,
                    Genre = Enum.Parse<Genre>(inputModel.Genre),
                    Description = inputModel.Description,
                    ImageUrl = inputModel.ImageUrl,
                    DeveloperId = inputModel.DeveloperId,
                    PublisherId = inputModel.PublisherId
                };

                dbContext.Games.Add(newGame);
                dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception.Message);

                ModelState.AddModelError(string.Empty, "An error occurred while adding the game. Please try again.");
                
                return View(inputModel);
            }
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

