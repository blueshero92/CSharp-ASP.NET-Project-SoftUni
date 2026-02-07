using System.ComponentModel.DataAnnotations;

using static GamingZoneApp.GCommon.Constants.ValidationConstants.GameConstants;
using static GamingZoneApp.GCommon.Constants.AppConstants;
using GamingZoneApp.GCommon.CustomValidationAttributes;


namespace GamingZoneApp.ViewModels.Game
{
    // Input model for adding/editing a game.
    public class GameInputModel
    {
        [Required]
        [MinLength(TitleMinLength, ErrorMessage = "Title must be at least {1} characters long.")]
        [MaxLength(TitleMaxLength, ErrorMessage = "Title cannot exceed {1} characters.")]
        public string Title { get; set; } = null!;

        [Required]
        [DisplayFormat(DataFormatString = "{0" + DateFormat + "}", ApplyFormatInEditMode = true)]
        [ValidReleaseDate] //Custom validation attribute to validate release date.
        public DateTime ReleaseDate { get; set; }

        [Required]        
        public string Genre { get; set; } = null!;

        [Required]
        [MinLength(GameDescriptionMinLength, ErrorMessage = "Description must be at least {1} characters long.")]
        [MaxLength(GameDescriptionMaxLength, ErrorMessage = "Description cannot exceed {1} characters.")]
        public string Description { get; set; } = null!;

        [Required]
        [MinLength(ImageUrlMinLength, ErrorMessage = "Image URL must be at least {1} characters long.")]
        [MaxLength(ImageUrlMaxLength, ErrorMessage = "Image URL cannot exceed {1} characters.")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "Please select a developer.")]
        public Guid DeveloperId { get; set; }

        [Required]
        public ICollection<AddGameDeveloperViewModel> Developers { get; set; } 
            = new List<AddGameDeveloperViewModel>(); //Collection of nested view model for Developers dropdown selection.

        [Required(ErrorMessage = "Please select a publisher.")]
        public Guid PublisherId { get; set; }

        [Required]
        public ICollection<AddGamePublisherViewModel> Publishers { get; set; } 
            = new List<AddGamePublisherViewModel>(); //Collection of nested view model for Publishers dropdown selection.
    }
}
