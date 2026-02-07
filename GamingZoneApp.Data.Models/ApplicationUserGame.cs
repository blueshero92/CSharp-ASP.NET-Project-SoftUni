using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamingZoneApp.Data.Models
{
    [PrimaryKey(nameof(UserId), nameof(GameId))]
    [Comment("Mapping table for users' games")]
    public class ApplicationUserGame
    {
        [Required]
        [ForeignKey(nameof(User))]
        [Comment("Foreign Key referencing the Id of the user.")]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Game))]
        [Comment("Foreign Key referencing the Id of the game.")]
        public Guid GameId { get; set; }
     
        public Game Game { get; set; } = null!;
    }
}
