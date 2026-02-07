using Microsoft.AspNetCore.Identity;

namespace GamingZoneApp.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        //HashSet<T> is used for future extesibility and speed for working with larger amounts of data and to avoid duplications.
        public ICollection<ApplicationUserGame> UsersGames { get; set; }
            = new HashSet<ApplicationUserGame>();
    }
}
