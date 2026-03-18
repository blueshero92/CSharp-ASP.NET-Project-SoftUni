namespace GamingZoneApp.Services.Models.Developer
{
    public class DeveloperAllDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int GamesDeveloped { get; set; }
        public string? ImageUrl { get; set; }
    }
}
