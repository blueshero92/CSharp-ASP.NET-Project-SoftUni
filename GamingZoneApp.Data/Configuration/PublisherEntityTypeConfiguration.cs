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
                Name = "Konami",
                Description = "Japanese video game publisher recognized for creating atmospheric and psychologically intense horror games that push the boundaries of interactive storytelling.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Konami"
            },
            new Publisher
            {
                Id = Guid.Parse("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"),
                Name = "CD Projekt",
                Description = "Polish video game publisher and distributor known for supporting independent studios and publishing award-winning narrative-driven games.",
                ImageUrl = "https://via.placeholder.com/300x300?text=CD+Projekt"
            },
            new Publisher
            {
                Id = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                Name = "Bandai Namco Entertainment",
                Description = "Japanese video game publisher specializing in action-packed games and collaborations with renowned developers to create premium gaming experiences.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Bandai+Namco"
            },
            new Publisher
            {
                Id = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                Name = "Capcom",
                Description = "Japanese video game publisher known for creating legendary survival horror franchises and action games with exceptional design and storytelling.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Capcom"
            },
            new Publisher
            {
                Id = Guid.Parse("3c77575e-bad2-4408-901b-9ec193a7cfba"),
                Name = "Sony Interactive Entertainment",
                Description = "American video game publisher and division of Sony, famous for publishing exclusive PlayStation titles and supporting innovative game development.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Sony+Interactive"
            },
            new Publisher
            {
                Id = Guid.Parse("b037a40e-b701-4bdc-a5c0-96b0fdd92619"),
                Name = "Team Cherry",
                Description = "Independent publisher known for releasing meticulously crafted indie games with exceptional artistic vision and challenging gameplay.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Team+Cherry"
            },
            new Publisher
            {
                Id = Guid.Parse("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"),
                Name = "Supergiant Games",
                Description = "Independent video game publisher renowned for creating visually stunning games with exceptional soundtracks and innovative gameplay mechanics.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Supergiant+Games"
            },
            new Publisher
            {
                Id = Guid.Parse("1a94a706-493a-4ec2-b3e1-3cd802128f59"),
                Name = "Square Enix",
                Description = "Japanese video game publisher known for publishing epic JRPGs and iconic franchises with memorable characters and engaging storytelling.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Square+Enix"
            },
            new Publisher
            {
                Id = Guid.Parse("bad774ec-fc82-479d-800c-97c5bd602884"),
                Name = "Bethesda Softworks",
                Description = "American video game publisher owned by Microsoft, famous for publishing massive open-world RPGs and supporting innovative game development.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Bethesda"
            },
            new Publisher
            {
                Id = Guid.Parse("e89f7368-94f7-4f55-8e0d-a03333659abb"),
                Name = "Valve",
                Description = "American video game publisher and digital distribution platform known for innovative, genre-defining games and digital distribution excellence.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Valve"
            },
            new Publisher
            {
                Id = Guid.Parse("e2b91da3-3f00-4eb4-9785-643acbcf4c55"),
                Name = "Larian Studios",
                Description = "Belgian video game publisher known for publishing deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Larian+Studios"
            },
            new Publisher
            {
                Id = Guid.Parse("556474fc-388b-48a7-9bd2-c45528b09bb1"),
                Name = "Kepler Interactive",
                Description = "Independent video game publisher known for supporting innovative action RPGs and artistic games from talented developers with unique creative vision.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Kepler+Interactive"
            },
            new Publisher
            {
                Id = Guid.Parse("ed820b54-96c2-4fbf-a533-09b193c08028"),
                Name = "Focus Entertainment",
                Description = "European video game publisher known for publishing action-packed games and supporting diverse game genres from independent to AAA studios.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Focus+Entertainment"
            }
        };

        public void Configure(EntityTypeBuilder<Publisher> entity)
        {
            entity.HasData(publishers);
        }
    }
}
