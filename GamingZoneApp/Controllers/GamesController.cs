using Microsoft.AspNetCore.Mvc;

using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using NuGet.Protocol;

namespace GamingZoneApp.Controllers
{
    public class GamesController : Controller
    {
        private readonly GamingZoneDbContext dbContext;

        public GamesController(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Game> games = dbContext
                                     .Games
                                     .Include(g => g.Developer)
                                     .OrderBy(g => g.Title)
                                     .AsNoTracking()
                                     .ToList();

            return View(games);
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
