namespace GamingZoneApp.Services.Models.User
{
    public class DeleteUserDto
    {
            public Guid Id { get; set; }
    
            public string Username { get; set; } = null!;
    
            public string Email { get; set; } = null!;
    }
}
