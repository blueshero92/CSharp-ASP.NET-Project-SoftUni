using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static GamingZoneApp.Data.Common.Constants.EntityValidationConstants.PublisherConstants;

namespace GamingZoneApp.Data.Models
{
    [Comment("Publishers in the system.")]
    public class Publisher
    {
        [Key]
        [Comment("Identifier for the publisher.")]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(PublisherNameMaxLength)]
        [Comment("Name of the publisher.")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(PublisherDescriptionMaxLength)]
        [Comment("Brief information about the publisher.")]
        public string Description { get; set; } = null!;

        //HashSet<T> is used for future extesibility and speed for working with larger amounts of data and to avoid duplications.
        public ICollection<Game> GamesPublished { get; set; }
            = new HashSet<Game>();
    }
}