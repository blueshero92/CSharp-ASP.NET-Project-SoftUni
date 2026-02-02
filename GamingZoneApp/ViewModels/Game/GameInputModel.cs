using System.ComponentModel.DataAnnotations;

using static GamingZoneApp.Data.Common.Constants.ValidationConstants.GameConstants;
using static GamingZoneApp.Data.Common.Constants.AppConstants;


namespace GamingZoneApp.ViewModels.Game
{
    public class GameInputModel
    {
        [Required]
        [MinLength(TitleMinLength, ErrorMessage = "Title must be at least {1} characters long.")]
        [MaxLength(TitleMaxLength, ErrorMessage = "Title cannot exceed {1} characters.")]
        public string Title { get; set; } = null!;

        [Required]
        [DisplayFormat(DataFormatString = "{0" + DateFormat + "}", ApplyFormatInEditMode = true)]
        [RegularExpression(ReleaseDateValidationRegex, ErrorMessage = "Release date must be in the format yyyy-MM-dd.")]
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
            = new List<AddGameDeveloperViewModel>();

        [Required(ErrorMessage = "Please select a publisher.")]
        public Guid PublisherId { get; set; }

        [Required]
        public ICollection<AddGamePublisherViewModel> Publishers { get; set; } 
            = new List<AddGamePublisherViewModel>();
    }
}
