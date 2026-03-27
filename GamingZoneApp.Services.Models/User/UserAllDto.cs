namespace GamingZoneApp.Services.Models.User
{
    public class UserAllDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<string> Roles { get; set; }
            = new List<string>();
    }
}
