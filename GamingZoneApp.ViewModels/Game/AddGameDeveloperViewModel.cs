namespace GamingZoneApp.ViewModels.Game
{
    //View model for assigning a developer to a game in the Add/Edit form.
    public class AddGameDeveloperViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
