namespace GamingZoneApp.ViewModels.Publisher
{
    // View model for displaying all publishers.
    public class AllPublishersViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int GamesPublished { get; set; }
        public string ImageUrl { get; set; } = null!;


    }
}
