using System.ComponentModel.DataAnnotations;

using static GamingZoneApp.GCommon.Constants.AppConstants;
using static GamingZoneApp.GCommon.Constants.ValidationConstants.GameConstants;
using static GamingZoneApp.GCommon.Constants.ErrorMessages.GameInputModelErrors;
using GamingZoneApp.GCommon.CustomValidationAttributes;


namespace GamingZoneApp.ViewModels.Game
{
    // Input model for adding/editing a game.
    public class GameInputModel
    {
        [Required]
        [MinLength(TitleMinLength, ErrorMessage = TitleMinLengthError)]
        [MaxLength(TitleMaxLength, ErrorMessage = TitleMaxLengthError)]
        public string Title { get; set; } = null!;

        [Required]
        [DisplayFormat(DataFormatString = "{0" + AppDateFormat + "}", ApplyFormatInEditMode = true)]
        [ValidReleaseDate] //Custom validation attribute to validate release date.
        public DateTime ReleaseDate { get; set; }

        [Required]        
        public string Genre { get; set; } = null!;

        [Required]
        [MinLength(GameDescriptionMinLength, ErrorMessage = DescriptionMinLengthError)]
        [MaxLength(GameDescriptionMaxLength, ErrorMessage = DescriptionMaxLengthError)]
        public string Description { get; set; } = null!;

        
        [MinLength(ImageUrlMinLength, ErrorMessage = ImageUrlMinLengthError)]
        [MaxLength(ImageUrlMaxLength, ErrorMessage = ImageUrlMaxLengthError)]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = DeveloperRequiredError)]
        public Guid DeveloperId { get; set; }

        [Required]
        public ICollection<AddGameDeveloperViewModel> Developers { get; set; } 
            = new List<AddGameDeveloperViewModel>(); //Collection of nested view model for Developers dropdown selection.

        [Required(ErrorMessage = PublisherRequiredError)]
        public Guid PublisherId { get; set; }

        [Required]
        public ICollection<AddGamePublisherViewModel> Publishers { get; set; } 
            = new List<AddGamePublisherViewModel>(); //Collection of nested view model for Publishers dropdown selection.
        
        public Guid? UserId { get; set; }
    }
}
