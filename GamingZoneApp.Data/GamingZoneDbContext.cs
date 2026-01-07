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

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        public virtual DbSet<ApplicationUserGame> ApplicationUsersGames { get; set; } = null!;

        public virtual DbSet<Developer> Developers { get; set; } = null!;

        public virtual DbSet<Game> Games { get; set; } = null!;

        public virtual DbSet<Publisher> Publishers { get; set; } = null!;
    }
}
