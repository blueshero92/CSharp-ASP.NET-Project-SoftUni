namespace GamingZoneApp.ViewModels.Developer
{
    //View model to visualize all developers in the "Developers" section.
    public class AllDevelopersViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int GamesDeveloped { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
}
