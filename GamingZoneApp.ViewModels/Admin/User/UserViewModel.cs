namespace GamingZoneApp.ViewModels.Admin.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<string> Roles { get; set; }
            = new List<string>();
    }
}
