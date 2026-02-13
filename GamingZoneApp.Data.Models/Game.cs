using GamingZoneApp.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static GamingZoneApp.GCommon.Constants.ValidationConstants.GameConstants;

namespace GamingZoneApp.Data.Models
{
    [Comment("Games in the system.")]
    public class Game
    {
        [Key]
        [Comment("Identifier of the videogame.")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("Title of the videogame.")]
        public string Title { get; set; } = null!;

        [Required]
        [Column(TypeName = ReleaseDateType)]
        [Comment("Videogame Release Date.")]
        public DateTime ReleaseDate { get; set; } 

        [Required]
        [Comment("Genre of the videogame.")]
        public Genre Genre { get; set; }

        [Required]
        [MaxLength(GameDescriptionMaxLength)]
        [Comment("Description/trivia of the game.")]
        public string Description { get; set; } = null!;

        [Required]
        [Column(TypeName = GameRatingType)]
        [Comment("Videogame's average rating. Default value used, because users will rate the game")]
        public decimal Rating { get; set; } = 0.0m;

        [Required]
        [Comment("Shows if the videogame is deleted.")]
        public bool IsDeleted { get; set; }

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        [Comment("URL of the videogame image.")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Developer))]
        [Comment("Foreign Key referencing the developer of the game.")]
        public Guid DeveloperId { get; set; }

        public Developer Developer { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Publisher))]
        [Comment("Foreign Key referencing the publisher of the game.")]
        public Guid PublisherId { get; set; }

        public  Publisher Publisher { get; set; } = null!;

        //HashSet<T> is used for future extesibility and speed for working with larger amounts of data and to avoid duplications.
        public ICollection<ApplicationUserGame> GamesUsers { get; set; }
            = new HashSet<ApplicationUserGame>();

        [Required]
        [ForeignKey(nameof(User))]
        [Comment("Foreign Key referencing the user who added the game.")]
        public Guid UserId { get; set; }

        [Required]
        public ApplicationUser? User { get; set; }
    }
}
