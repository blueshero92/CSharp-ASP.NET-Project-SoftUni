using GamingZoneApp.Data.Models;

namespace GamingZoneApp.Data.Repository.Interfaces
{
    public interface IPublisherRepository
    {
            IQueryable<Publisher> GetAllPublishersNoTracking();
    
            IQueryable<Game> GetAllGamesByPublisherNoTracking(Guid publisherId);
    
            Task<bool> CheckIfPublisherExistsAsync(Guid publisherId);
    }
}
