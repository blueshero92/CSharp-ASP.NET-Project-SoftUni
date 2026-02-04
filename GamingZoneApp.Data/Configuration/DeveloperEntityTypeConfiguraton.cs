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
                ImageUrl = "https://via.placeholder.com/300x300?text=Nightshade+Studios"
            },
            new Developer
            {
                Id = Guid.Parse("ba8230ef-c6b3-4be8-9408-43d57b2049c3"),
                Name = "Crimson Forge Entertainment",
                Description = "Award-winning studio renowned for creating story-driven, visually stunning games with deep narratives and meaningful player choice.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Crimson+Forge"
            },            
            new Developer
            {
                Id = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                Name = "Iron Tempest Games",
                Description = "Renowned video game developer celebrated for crafting challenging and immersive gameplay experiences with intricate level design and compelling boss battles.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Iron+Tempest"
            },
            new Developer
            {
                Id = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                Name = "Phoenix Rising Interactive",
                Description = "Veteran video game developer and publisher known for creating legendary franchises and survival horror masterpieces with intense action gameplay.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Phoenix+Rising"
            },
            new Developer
            {
                Id = Guid.Parse("4af65147-9ea5-4e30-a16d-de4750debe73"),
                Name = "Olympus Digital",
                Description = "Premier video game developer famous for creating epic narrative-driven action games with stunning visuals and cinematic presentation.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Olympus+Digital"
            },
            new Developer
            {
                Id = Guid.Parse("c0d88616-83ce-416a-a034-11264b776cc1"),
                Name = "Pixel Hollow Studios",
                Description = "Independent video game developer known for creating meticulously crafted indie games with exceptional art direction and challenging gameplay.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Pixel+Hollow"
            },
            new Developer
            {
                Id = Guid.Parse("7596ad8e-5eaa-44da-a716-6ab508873b52"),
                Name = "Ember Flame Games",
                Description = "Independent video game studio renowned for creating visually stunning games with exceptional soundtracks and innovative gameplay mechanics.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Ember+Flame"
            },
            new Developer
            {
                Id = Guid.Parse("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"),
                Name = "Crystal Dawn Entertainment",
                Description = "Major video game developer and publisher known for creating epic JRPGs and iconic franchises with memorable characters and engaging storylines.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Crystal+Dawn"
            },
            new Developer
            {
                Id = Guid.Parse("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"),
                Name = "Starbound Studios",
                Description = "Leading video game developer famous for creating massive open-world RPGs with deep worlds and endless exploration opportunities.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Starbound+Studios"
            },
            new Developer
            {
                Id = Guid.Parse("07dbe539-557e-4692-85e6-c875b1262a71"),
                Name = "Quantum Rift Software",
                Description = "Innovative video game developer known for creating genre-defining games with exceptional design, storytelling, and groundbreaking mechanics.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Quantum+Rift"
            },
            new Developer
            {
                Id = Guid.Parse("ae06f7d4-7f50-411d-86af-c97ac324972e"),
                Name = "Shadow Blade Productions",
                Description = "Acclaimed video game developer known for creating open-world action games with compelling narratives, stunning visual artistry, and engaging gameplay.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Shadow+Blade"
            },
            new Developer
            {
                Id = Guid.Parse("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"),
                Name = "Arcane Depths Interactive",
                Description = "European video game developer known for creating deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Arcane+Depths"
            },
            new Developer
            {
                Id = Guid.Parse("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                Name = "Prism Voyage Studios",
                Description = "Boutique video game developer founded by industry veterans, known for creating innovative action RPGs with unique artistic vision and creative gameplay.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Prism+Voyage"
            },
            new Developer
            {
                Id = Guid.Parse("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                Name = "Titan Forge Games",
                Description = "Dynamic video game developer known for creating fast-paced action games with intense gameplay and large-scale multiplayer experiences.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Titan+Forge"
            }
        };

        public void Configure(EntityTypeBuilder<Developer> entity)
        {
            entity.HasData(developers);
        }
    }
}
