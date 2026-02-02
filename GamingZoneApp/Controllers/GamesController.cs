using GamingZoneApp.Data;
using GamingZoneApp.ViewModels.Game;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;


namespace GamingZoneApp.Controllers
{
    public class GamesController : Controller
    {
        private readonly GamingZoneDbContext dbContext;

        public GamesController(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Visualize all games using a view model.

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<AllGamesViewModel> allGames = dbContext
                                     .Games
                                     .Include(g => g.Developer)
                                     .Select(g => new AllGamesViewModel
                                     {
                                         Id = g.Id,
                                         Title = g.Title,
                                         ImageUrl = g.ImageUrl,
                                         Genre = g.Genre.ToString(),
                                         Developer = g.Developer.Name,

                                     })
                                     .OrderBy(g => g.Title)
                                     .AsNoTracking()
                                     .ToList();

            return View(allGames);
        }

        //Visualize game details for an individual game using a view model.
        [HttpGet]
        public IActionResult GameDetails(Guid id)
        {
            GameViewModel? selectedGame = dbContext
                               .Games
                               .Include(g => g.Developer)
                               .Include(g => g.Publisher)
                               .Select(g => new GameViewModel
                               {
                                   Id = g.Id,
                                   Title = g.Title,
                                   ReleaseDate = g.ReleaseDate.ToString("dd/MM/yyyy"),
                                   Genre = g.Genre.ToString(),
                                   Description = g.Description,
                                   Rating = g.Rating,
                                   ImageUrl = g.ImageUrl,
                                   Developer = g.Developer.Name,
                                   Publisher = g.Publisher.Name,
                                   DeveloperLogoUrl = g.Developer.ImageUrl,
                                   PublisherLogoUrl = g.Publisher.ImageUrl

                               })
                               .AsNoTracking()
                               .AsSplitQuery()
                               .SingleOrDefault(g => g.Id == id);


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
            GameInputModel viewModel = new GameInputModel()
            {
                Developers = GetAllDevelopers().ToList(),

                Publishers = GetAllPublishers().ToList()
            };

            return View(viewModel);
        }

        //Process the Add Game form submission.
        [HttpPost]
        public IActionResult AddGame(GameInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                inputModel.Developers = GetAllDevelopers().ToList();

                inputModel.Publishers = GetAllPublishers().ToList();

                return View(inputModel);
            }

            if (!DeveloperExists(inputModel.DeveloperId))
            {
                ModelState.AddModelError(nameof(inputModel.DeveloperId), "Selected developer does not exist.");

                return View(inputModel);
            }

            if (!PublisherExists(inputModel.PublisherId))
            {
                ModelState.AddModelError(nameof(inputModel.PublisherId), "Selected publisher does not exist.");

                return View(inputModel);
            }

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

