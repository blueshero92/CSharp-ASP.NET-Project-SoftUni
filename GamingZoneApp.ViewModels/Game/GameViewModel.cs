namespace GamingZoneApp.ViewModels.Game
{

    // View model for displaying detailed information about an individual game.
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Rating { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Developer { get; set; } = null!;
        public string Publisher { get; set; } = null!;

        public string DeveloperLogoUrl { get; set; } = null!;

        public string PublisherLogoUrl { get; set; } = null!;
    }
}
