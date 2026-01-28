using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Controllers
{
    public class DevelopersController : Controller
    {
        private readonly GamingZoneDbContext dbContext;

        public DevelopersController(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Visualize all developers.

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Developer> developers = dbContext
                                             .Developers
                                             .Include(d => d.GamesDeveloped)
                                             .OrderBy(d => d.Name)
                                             .AsNoTracking()
                                             .ToList();

            return View(developers);
        }

        //Visualize all games by a specific developer.
        //Created buttons to be able to access this view from the Developers/Index view.

        [HttpGet]
        public IActionResult DeveloperGames(Guid developerId)
        {
            IEnumerable<Game> gamesByDev = dbContext
                                       .Games
                                       .Include(g => g.Developer)
                                       .Include(g => g.Publisher)
                                       .Where(g => g.DeveloperId == developerId)
                                       .AsNoTracking()
                                       .AsSplitQuery()
                                       .ToList();

            return View(gamesByDev);

        }
    }
}
