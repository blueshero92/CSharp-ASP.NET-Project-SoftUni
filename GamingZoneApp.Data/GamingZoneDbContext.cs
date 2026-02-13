using Azure.Identity;
using GamingZoneApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GamingZoneApp.Data
{
    public class GamingZoneDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public GamingZoneDbContext(DbContextOptions<GamingZoneDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

        public DbSet<ApplicationUserGame> ApplicationUsersGames { get; set; } = null!;

        public DbSet<Developer> Developers { get; set; } = null!;

        public DbSet<Game> Games { get; set; } = null!;

        public DbSet<Publisher> Publishers { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all configurations from the GamingZoneApp.Data to seed the database.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GamingZoneDbContext).Assembly);


            //Created an admin user with a known password for testing purposes.
            Guid adminUserId = Guid.Parse("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f");

            ApplicationUser adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                ConcurrencyStamp = Guid.NewGuid().ToString("D")

            };

            // Hash the password for the admin user.
            adminUser.PasswordHash = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, "Admin123!");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);
        }
    }

} 
