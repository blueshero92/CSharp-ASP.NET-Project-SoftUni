using Microsoft.AspNetCore.Mvc;

using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Protocol;
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


        [HttpGet]
        public IActionResult GameDetails(Guid id)
        {
            Game? selectedGame = dbContext
                               .Games
                               .Include(g => g.Developer)
                               .Include(g => g.Publisher)
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
