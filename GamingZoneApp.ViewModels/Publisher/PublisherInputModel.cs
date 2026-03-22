using System.ComponentModel.DataAnnotations;
using static GamingZoneApp.GCommon.Constants.ValidationConstants.PublisherConstants;

namespace GamingZoneApp.ViewModels.Publisher
{
    public class PublisherInputModel
    {

        [Required]
        [MinLength(PublisherNameMinLength)]
        [MaxLength(PublisherNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(PublisherDescriptionMinLength)]
        [MaxLength(PublisherDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [MinLength(ImageUrlMinLength)]
        [MaxLength(ImageUrlMaxLength)]
        public string? ImageUrl { get; set; }
    }
}
