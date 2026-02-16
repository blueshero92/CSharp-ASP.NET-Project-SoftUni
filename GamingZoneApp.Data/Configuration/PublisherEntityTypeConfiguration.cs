using GamingZoneApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingZoneApp.Data.Configuration
{
    public class PublisherEntityTypeConfiguration : IEntityTypeConfiguration<Publisher>
    {
        //Seeded publishers data in the database.
        //Some developers also publish their own games so Developers and Publishers may share the same name but are different entities.
        private readonly IEnumerable<Publisher> publishers = new List<Publisher>()
        {
            new Publisher
            {
                Id = Guid.Parse("a0529584-8eb3-4880-8674-c4e5cc67b487"),
                Name = "Phantom Gate Publishing",
                Description = "International video game publisher recognized for creating atmospheric and psychologically intense horror games that push the boundaries of interactive storytelling.",
                ImageUrl = "/images/publisher.png"
            },
            new Publisher
            {
                Id = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                Name = "Dragon Crown Entertainment",
                Description = "Major video game publisher specializing in action-packed games and collaborations with renowned developers to create premium gaming experiences.",
                ImageUrl = "/images/publisher.png"
            },
            new Publisher
            {
                Id = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                Name = "Phoenix Rising Publishing",
                Description = "Established video game publisher known for creating legendary survival horror franchises and action games with exceptional design and storytelling.",
                ImageUrl = "/images/publisher.png"
            },
            new Publisher
            {
                Id = Guid.Parse("b037a40e-b701-4bdc-a5c0-96b0fdd92619"),
                Name = "Pixel Hollow Publishing",
                Description = "Independent publisher known for releasing meticulously crafted indie games with exceptional artistic vision and challenging gameplay.",
                ImageUrl = "/images/publisher.png"
            },
            new Publisher
            {
                Id = Guid.Parse("556474fc-388b-48a7-9bd2-c45528b09bb1"),
                Name = "Prism Voyage Publishing",
                Description = "Independent video game publisher known for supporting innovative action RPGs and artistic games from talented developers with unique creative vision.",
                ImageUrl = "/images/publisher.png"
            },
            new Publisher
            {
                Id = Guid.Parse("ed820b54-96c2-4fbf-a533-09b193c08028"),
                Name = "Titan Forge Publishing",
                Description = "International video game publisher known for publishing action-packed games and supporting diverse game genres from independent to AAA studios.",
                ImageUrl = "/images/publisher.png"
            }
        };

        public void Configure(EntityTypeBuilder<Publisher> entity)
        {
            entity.HasData(publishers);
        }
    }
}
