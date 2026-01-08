using Microsoft.AspNetCore.Identity;

namespace GamingZoneApp.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        public virtual ICollection<ApplicationUserGame> UsersGames { get; set; }
            = new HashSet<ApplicationUserGame>();
    }
}
