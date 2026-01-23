using GamingZoneApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingZoneApp.Data.Configuration
{
    public class DeveloperEntityTypeConfiguraton : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> builder)
        {
            throw new NotImplementedException();
        }
    }
}
