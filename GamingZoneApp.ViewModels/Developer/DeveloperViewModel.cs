namespace GamingZoneApp.ViewModels.Developer
{
    public class DeveloperViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
