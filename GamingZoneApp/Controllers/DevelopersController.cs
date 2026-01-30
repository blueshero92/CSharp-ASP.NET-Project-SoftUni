using GamingZoneApp.Data;
using GamingZoneApp.ViewModels.Developer;
using GamingZoneApp.ViewModels.Game;
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

        //Visualize all developers using view model.

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<AllDevelopersViewModel> developers = dbContext
                                             .Developers
                                             .Include(d => d.GamesDeveloped)
                                             .Select(d => new AllDevelopersViewModel
                                                {
                                                    Id = d.Id,
                                                    Name = d.Name,
                                                    GamesDeveloped = d.GamesDeveloped.Count,
                                                    Description = d.Description,
                                                    ImageUrl = d.ImageUrl,
                                                })
                                             .OrderBy(d => d.Name)
                                             .AsNoTracking()
                                             .ToList();

            return View(developers);
        }

        //Visualize all games by a specific developer using a view model.
        //Created buttons to be able to access this view from the Developers/Index view.

        [HttpGet]
        public IActionResult DeveloperGames(Guid developerId)
        {
            IEnumerable<AllGamesViewModel> gamesByDev = dbContext
                                       .Games
                                       .Include(g => g.Developer)
                                       .Include(g => g.Publisher)
                                       .Where(g => g.DeveloperId == developerId)
                                       .Select(g => new AllGamesViewModel
                                       {   
                                           Id = g.Id,
                                           Title = g.Title,
                                           ImageUrl = g.ImageUrl,
                                           Genre = g.Genre.ToString(),
                                           Developer = g.Developer.Name,

                                       })
                                       .AsNoTracking()
                                       .AsSplitQuery()
                                       .ToList();

            return View(gamesByDev);

        }
    }
}
