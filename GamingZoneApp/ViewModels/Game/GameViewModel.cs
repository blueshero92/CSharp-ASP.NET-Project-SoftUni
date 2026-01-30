namespace GamingZoneApp.ViewModels.Game
{
    using GamingZoneApp.Data.Models;

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
        public Developer Developer { get; set; } = null!;
        public Publisher Publisher { get; set; } = null!;
    }
}
