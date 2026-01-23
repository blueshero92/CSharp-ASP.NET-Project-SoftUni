using GamingZoneApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GamingZoneApp.Data.Configuration
{
    public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
    {
        private readonly IEnumerable<Game> games = new List<Game>()
        {

        };
        
        public void Configure(EntityTypeBuilder<Game> entity)
        {
            throw new NotImplementedException();
        }
    }
}
