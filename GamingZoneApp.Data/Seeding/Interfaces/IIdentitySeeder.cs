namespace GamingZoneApp.Data.Seeding.Interfaces
{
    public interface IIdentitySeeder
    {
        Task SeedRolesAsync();

        Task SeedAdminUserAsync();

        Task SeedModeratorUserAsync();
    }
}
