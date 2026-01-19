using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static GamingZoneApp.Data.Common.Constants.EntityValidationConstants.DeveloperConstants;

namespace GamingZoneApp.Data.Models
{
    [Comment("Developers in the system.")]
    public class Developer
    {
        [Key]
        [Comment("Identifier for the developer.")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(DevNameMaxLength)]
        [Comment("Name of the developer.")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(DevDescriptionMaxLength)]
        [Comment("Brief information about the developer.")]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        [Comment("URL of the developer logo image.")]
        public string ImageUrl { get; set; } = null!;

        //HashSet<T> is used for future extesibility and speed for working with larger amounts of data and to avoid duplications.
        public ICollection<Game> GamesDeveloped { get; set; }
            = new HashSet<Game>();
    }
}