namespace GamingZoneApp.Services.Models.Game
{
    public class GameDetailsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Rating { get; set; }
        public string? ImageUrl { get; set; }
        public string Developer { get; set; } = null!;
        public string Publisher { get; set; } = null!;

        public string DeveloperLogoUrl { get; set; } = null!;

        public string PublisherLogoUrl { get; set; } = null!;
    }
}
