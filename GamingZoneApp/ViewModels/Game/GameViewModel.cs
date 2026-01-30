using GamingZoneApp.Data.Models;

namespace GamingZoneApp.ViewModels.Game
{
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
