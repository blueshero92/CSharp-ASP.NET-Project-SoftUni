using GamingZoneApp.Data;
using GamingZoneApp.Data.Models;
using GamingZoneApp.ViewModels.Game;
using GamingZoneApp.ViewModels.Publisher;
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

        //Visualize all publishers using a view model.

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<AllPublishersViewModel> publishers = dbContext
                                             .Publishers
                                             .Include(p => p.GamesPublished)
                                             .OrderBy(p => p.Name)
                                             .Select(p => new AllPublishersViewModel
                                                {
                                                    Id = p.Id,
                                                    Name = p.Name,
                                                    Description = p.Description,
                                                    GamesPublished = p.GamesPublished.Count,
                                                    ImageUrl = p.ImageUrl,
                                                })
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
