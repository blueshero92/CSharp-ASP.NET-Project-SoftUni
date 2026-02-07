namespace GamingZoneApp.ViewModels.Game
{
    // View model to display all games in the section "Games".
    public class AllGamesViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public string Developer { get; set; } = null!;

    }
}
