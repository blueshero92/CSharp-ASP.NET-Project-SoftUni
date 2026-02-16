using GamingZoneApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GamingZoneApp.Data.Configuration
{
    public class DeveloperEntityTypeConfiguraton : IEntityTypeConfiguration<Developer>
    {
        //Seeded developers data in the database.
        //Some developers also publish their own games so Developers and Publishers may share the same name but are different entities.
        private readonly IEnumerable<Developer> developers = new List<Developer>()
        {
            new Developer
            {
                Id = Guid.Parse("f75ee60f-4615-4bca-8a21-be507c6d3a49"),
                Name = "Nightshade Studios",
                Description = "European video game developer known for creating atmospheric psychological horror experiences with cutting-edge graphics and immersive storytelling.",
                ImageUrl = "/images/developer.png"
            },        
            new Developer
            {
                Id = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                Name = "Iron Tempest Games",
                Description = "Renowned video game developer celebrated for crafting challenging and immersive gameplay experiences with intricate level design and compelling boss battles.",
                ImageUrl = "/images/developer.png"
            },
            new Developer
            {
                Id = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                Name = "Phoenix Rising Interactive",
                Description = "Veteran video game developer and publisher known for creating legendary franchises and survival horror masterpieces with intense action gameplay.",
                ImageUrl = "/images/developer.png"
            },
            new Developer
            {
                Id = Guid.Parse("c0d88616-83ce-416a-a034-11264b776cc1"),
                Name = "Pixel Hollow Studios",
                Description = "Independent video game developer known for creating meticulously crafted indie games with exceptional art direction and challenging gameplay.",
                ImageUrl = "/images/developer.png"
            },
            new Developer
            {
                Id = Guid.Parse("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                Name = "Prism Voyage Studios",
                Description = "Boutique video game developer founded by industry veterans, known for creating innovative action RPGs with unique artistic vision and creative gameplay.",
                ImageUrl = "/images/developer.png"
            },
            new Developer
            {
                Id = Guid.Parse("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                Name = "Titan Forge Games",
                Description = "Dynamic video game developer known for creating fast-paced action games with intense gameplay and large-scale multiplayer experiences.",
                ImageUrl = "/images/developer.png"
            }
        };

        public void Configure(EntityTypeBuilder<Developer> entity)
        {
            entity.HasData(developers);
        }
    }
}
