using GamingZoneApp.Data.Models;
using GamingZoneApp.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GamingZoneApp.Data.Configuration
{
    public class GameEntityTypeConfiguration : IEntityTypeConfiguration<Game>
    {
        

        //Seeded games data in the database.
        private readonly IEnumerable<Game> games = new List<Game>()
        {
            new Game
            {
                Id = Guid.Parse("adaaad5d-49ee-4f9d-9de8-3d21f5333e6b"),
                Title = "Bloodlight Sanatorium",
                ReleaseDate = new DateTime(2024, 10, 8),
                Genre = Genre.SurvivalHorror,
                Description = "Venture into the abandoned Bloodlight Sanatorium where shadows breathe and walls remember. As paranormal investigator Elena Voss, unravel decades of buried secrets while evading entities that feed on fear itself.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("f75ee60f-4615-4bca-8a21-be507c6d3a49"),
                PublisherId = Guid.Parse("a0529584-8eb3-4880-8674-c4e5cc67b487"),
                ImageUrl = "/images/game/gameDefault.png",
                UserId = Guid.Parse("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f")
            },
            new Game
            {
                Id = Guid.Parse("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"),
                Title = "Oath of the Vermillion Ronin",
                ReleaseDate = new DateTime(2019, 3, 22),
                Genre = Genre.Soulslike,
                Description = "Disgraced and left for dead, rise as a nameless swordsman in the war-ravaged Kiyohara Province. Master the forbidden Crimson Style, hunt those who betrayed you, and carve a path through oni and warlord alike.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                PublisherId = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                ImageUrl = "/images/game/gameDefault.png",
                UserId = Guid.Parse("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f")
            },
            new Game
            {
                Id = Guid.Parse("bbbc7561-9fac-4795-9606-3f94c28ea92f"),
                Title = "The Mystery of Ravencrest",
                ReleaseDate = new DateTime(2021, 5, 9),
                Genre = Genre.SurvivalHorror,
                Description = "Something ancient stirs beneath Ravencrest Estate. As Dominic Ashford, search for your missing daughter through decaying halls, confront manifestations of familial guilt, and survive a night where the house itself hunts you.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                PublisherId = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                ImageUrl = "/images/game/gameDefault.png",
                UserId = Guid.Parse("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f")
            },
            new Game
            {
                Id = Guid.Parse("96c36ea6-284b-464b-8ece-fae17e8b8cb7"),
                Title = "Gearwright Caverns",
                ReleaseDate = new DateTime(2017, 2, 24),
                Genre = Genre.Metroidvania,
                Description = "Deep within the Brass Hollows, a clockwork civilization crumbles. Guide the last Tinker through labyrinthine foundries, rewire ancient mechanisms, and discover why the Great Engine fell silent centuries ago.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("c0d88616-83ce-416a-a034-11264b776cc1"),
                PublisherId = Guid.Parse("b037a40e-b701-4bdc-a5c0-96b0fdd92619"),
                ImageUrl = "/images/game/gameDefault.png",
                UserId = Guid.Parse("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f")
            },
            new Game
            {
                Id = Guid.Parse("38c117e0-2416-4d69-a39a-2b4141c218b2"),
                Title = "Wanderers of the Shattered Isles",
                ReleaseDate = new DateTime(2025, 4, 8),
                Genre = Genre.ActionRPG,
                Description = "Sail between floating archipelagos in a world torn asunder by magical cataclysm. Recruit a crew of outcasts, uncover the secrets of the Sundering, and restore balance before the remaining islands fall into the void.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                PublisherId = Guid.Parse("556474fc-388b-48a7-9bd2-c45528b09bb1"),
                ImageUrl = "/images/game/gameDefault.png",
                UserId = Guid.Parse("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f")
            },
            new Game
            {
                Id = Guid.Parse("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                Title = "Galactic Crusaders II",
                ReleaseDate = new DateTime(2024, 9, 9),
                Genre = Genre.Shooter,
                Description = "The Hegemony's elite answer humanity's darkest hour. Lead Sergeant Kira Vance through xeno-infested worlds, command squad tactics in brutal firefights, and uncover a conspiracy that threatens the entire frontier.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                PublisherId = Guid.Parse("ed820b54-96c2-4fbf-a533-09b193c08028"),
                ImageUrl = "/images/game/gameDefault.png",
                UserId = Guid.Parse("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f")
            }
        };

        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.HasData(games);
        }
    }
}
