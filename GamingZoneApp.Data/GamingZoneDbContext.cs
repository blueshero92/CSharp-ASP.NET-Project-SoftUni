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

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        public DbSet<ApplicationUserGame> ApplicationUsersGames { get; set; } = null!;

        public DbSet<Developer> Developers { get; set; } = null!;

        public DbSet<Game> Games { get; set; } = null!;

        public DbSet<Publisher> Publishers { get; set; } = null!;
    }
} 
