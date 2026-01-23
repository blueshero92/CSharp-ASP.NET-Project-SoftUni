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
                Name = "Bloober Team",
                Description = "Polish video game developer known for creating atmospheric psychological horror experiences with cutting-edge graphics and immersive storytelling.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Bloober+Team"
            },
            new Developer
            {
                Id = Guid.Parse("ba8230ef-c6b3-4be8-9408-43d57b2049c3"),
                Name = "CD Projekt Red",
                Description = "Polish video game studio renowned for creating story-driven, visually stunning games with deep narratives and meaningful player choice.",
                ImageUrl = "https://via.placeholder.com/300x300?text=CD+Projekt+Red"
            },            
            new Developer
            {
                Id = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                Name = "FromSoftware",
                Description = "Renowned Japanese video game developer celebrated for crafting challenging and immersive gameplay experiences with intricate level design and compelling boss battles.",
                ImageUrl = "https://via.placeholder.com/300x300?text=FromSoftware"
            },
            new Developer
            {
                Id = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                Name = "Capcom",
                Description = "Japanese video game developer and publisher known for creating legendary franchises and survival horror masterpieces with intense action gameplay.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Capcom"
            },
            new Developer
            {
                Id = Guid.Parse("4af65147-9ea5-4e30-a16d-de4750debe73"),
                Name = "Santa Monica Studio",
                Description = "American video game developer owned by Sony Interactive Entertainment, famous for creating epic narrative-driven action games with stunning visuals.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Santa+Monica+Studio"
            },
            new Developer
            {
                Id = Guid.Parse("c0d88616-83ce-416a-a034-11264b776cc1"),
                Name = "Team Cherry",
                Description = "Independent Australian video game developer known for creating meticulously crafted indie games with exceptional art direction and challenging gameplay.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Team+Cherry"
            },
            new Developer
            {
                Id = Guid.Parse("7596ad8e-5eaa-44da-a716-6ab508873b52"),
                Name = "Supergiant Games",
                Description = "Independent video game studio renowned for creating visually stunning games with exceptional soundtracks and innovative gameplay mechanics.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Supergiant+Games"
            },
            new Developer
            {
                Id = Guid.Parse("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"),
                Name = "Square Enix",
                Description = "Japanese video game developer and publisher known for creating epic JRPGs and iconic franchises with memorable characters and engaging storylines.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Square+Enix"
            },
            new Developer
            {
                Id = Guid.Parse("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"),
                Name = "Bethesda Game Studios",
                Description = "American video game developer owned by Microsoft, famous for creating massive open-world RPGs with deep worlds and endless exploration opportunities.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Bethesda"
            },
            new Developer
            {
                Id = Guid.Parse("07dbe539-557e-4692-85e6-c875b1262a71"),
                Name = "Valve",
                Description = "American video game developer and digital distribution company known for creating innovative, genre-defining games with exceptional design and storytelling.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Valve"
            },
            new Developer
            {
                Id = Guid.Parse("ae06f7d4-7f50-411d-86af-c97ac324972e"),
                Name = "Sucker Punch Productions",
                Description = "American video game developer known for creating open-world action games with compelling narratives, stunning visual artistry, and engaging gameplay mechanics.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Sucker+Punch"
            },
            new Developer
            {
                Id = Guid.Parse("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"),
                Name = "Larian Studios",
                Description = "Belgian video game developer known for creating deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Larian+Studios"
            },
            new Developer
            {
                Id = Guid.Parse("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                Name = "Sandfall Interactive",
                Description = "French video game developer founded by veteran developers, known for creating innovative action RPGs with unique artistic vision and creative gameplay.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Sandfall+Interactive"
            },
            new Developer
            {
                Id = Guid.Parse("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                Name = "Saber Interactive",
                Description = "American video game developer known for creating fast-paced action games with intense gameplay and faithful adaptations of popular franchises.",
                ImageUrl = "https://via.placeholder.com/300x300?text=Saber+Interactive"
            }
        };

        public void Configure(EntityTypeBuilder<Developer> entity)
        {
            entity.HasData(developers);
        }
    }
}
