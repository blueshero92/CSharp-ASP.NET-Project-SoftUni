using System.ComponentModel.DataAnnotations;
using static GamingZoneApp.GCommon.Constants.ValidationConstants.DeveloperConstants;

namespace GamingZoneApp.ViewModels.Developer
{
    public class DeveloperInputModel
    {
        [Required]
        [MinLength(DevNameMinLength)]
        [MaxLength(DevNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(DevDescriptionMinLength)]
        [MaxLength(DevDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [MinLength(ImageUrlMinLength)]
        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }
    }
}
