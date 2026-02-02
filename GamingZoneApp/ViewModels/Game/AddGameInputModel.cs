using System.ComponentModel.DataAnnotations;
using static GamingZoneApp.Data.Common.Constants.ValidationConstants.GameConstants;


namespace GamingZoneApp.ViewModels.Game
{
    public class AddGameInputModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Genre { get; set; } = null!;

        [Required]
        [MinLength(GameDescriptionMinLength)]
        [MaxLength(GameDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MinLength(ImageUrlMinLength)]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        public Guid DeveloperId { get; set; }

        [Required]
        public ICollection<AddGameDeveloperViewModel> Developers { get; set; } 
            = new List<AddGameDeveloperViewModel>();

        [Required]
        public Guid PublisherId { get; set; }

        [Required]
        public ICollection<AddGamePublisherViewModel> Publishers { get; set; } 
            = new List<AddGamePublisherViewModel>();
    }
}
