using Microsoft.AspNetCore.Mvc;

using GamingZoneApp.Data;

using Microsoft.EntityFrameworkCore;

using GamingZoneApp.ViewModels.Game;

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
                                     .Select( g => new AllGamesViewModel
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
                                   Developer = g.Developer,
                                   Publisher = g.Publisher
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
    }
}
