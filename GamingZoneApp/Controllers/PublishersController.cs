using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Controllers
{
    public class PublishersController : Controller
    {
        private readonly GamingZoneDbContext dbContext;

        public PublishersController(GamingZoneDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Visualize all publishers.

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Publisher> publishers = dbContext
                                             .Publishers
                                             .Include(p => p.GamesPublished)
                                             .OrderBy(p => p.Name)
                                             .AsNoTracking()
                                             .ToList();

            return View(publishers);
        }

        //Visualize all games by a specific publisherr.
        //Created buttons to be able to access this view from the Publishers/Index view.

        [HttpGet]
        public IActionResult PublisherGames(Guid publisherId)
        {
            IEnumerable<Game> gamesByPublisher = dbContext
                                       .Games
                                       .Include(g => g.Developer)
                                       .Include(g => g.Publisher)
                                       .Where(g => g.PublisherId == publisherId)
                                       .AsNoTracking()
                                       .AsSplitQuery()
                                       .ToList();
            
            return View(gamesByPublisher);
        }
    }
}
