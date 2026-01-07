using GamingZoneApp.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GamingZoneApp.Data.Common.Constants.EntityValidationConstants.GameConstants;

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
        [Comment("Videogame's average rating.")]
        public decimal Rating { get; set; }

        [Required]
        [Comment("Shows if the videogame is deleted.")]
        public bool IsDeleted { get; set; }

        [Required]
        [ForeignKey(nameof(Developer))]
        [Comment("Foreign Key referencing the developer of the game.")]
        public Guid DeveloperId { get; set; }

        public virtual Developer Developer { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Publisher))]
        [Comment("Foreign Key referencing the publisher of the game.")]
        public Guid PublisherId { get; set; }

        public virtual Publisher Publisher { get; set; } = null!;


    }
}
