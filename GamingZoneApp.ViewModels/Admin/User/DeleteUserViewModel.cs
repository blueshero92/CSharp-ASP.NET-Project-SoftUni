namespace GamingZoneApp.ViewModels.Admin.User
{
    public class DeleteUserViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

    }
}
