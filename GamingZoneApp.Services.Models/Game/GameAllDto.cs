namespace GamingZoneApp.Services.Models.Game
{
    public class GameAllDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public string Genre { get; set; } = null!;

        public string Developer { get; set; } = null!;

        public string Publisher { get; set; } = null!;
    }
}
