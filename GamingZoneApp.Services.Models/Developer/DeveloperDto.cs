namespace GamingZoneApp.Services.Models.Developer
{
    public class DeveloperDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
