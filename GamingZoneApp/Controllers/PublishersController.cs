using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.ViewModels.Game;
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

        //Visualize all games by a specific publisher using a view model.
        //Created buttons to be able to access this view from the Publishers/Index view.

        [HttpGet]
        public IActionResult PublisherGames(Guid publisherId)
        {
            IEnumerable<AllGamesViewModel> gamesByPublisher = dbContext
                                       .Games
                                       .Include(g => g.Developer)
                                       .Include(g => g.Publisher)
                                       .Where(g => g.PublisherId == publisherId)
                                       .Select(g => new AllGamesViewModel
                                        {
                                             Id = g.Id,
                                             Title = g.Title,
                                             ImageUrl = g.ImageUrl,
                                             Genre = g.Genre.ToString(),
                                             Developer = g.Developer.Name
                                        })
                                       .AsNoTracking()
                                       .AsSplitQuery()
                                       .ToList();
            
            return View(gamesByPublisher);
        }
    }
}
