using GamingZoneApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Data
{
    public class GamingZoneDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        //Empty constructor for debugging purposes.
        public GamingZoneDbContext()
        {

        }

        public GamingZoneDbContext(DbContextOptions<GamingZoneDbContext> options)
            : base(options)
        {
        }
    }
}
