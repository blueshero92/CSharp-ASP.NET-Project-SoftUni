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
                Title = "Echoes of the Abyss",
                ReleaseDate = new DateTime(2024, 10, 8),
                Genre = Genre.SurvivalHorror,
                Description = "A psychological horror experience where players explore a fog-covered abandoned asylum. Uncover the dark history of Dr. Mallory's experiments while evading twisted manifestations of past patients.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("f75ee60f-4615-4bca-8a21-be507c6d3a49"),
                PublisherId = Guid.Parse("a0529584-8eb3-4880-8674-c4e5cc67b487"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Echoes+of+the+Abyss"
            },
            new Game
            {
                Id = Guid.Parse("55562e8e-cdda-4bbb-8603-e9c619b46580"),
                Title = "Chronicles of Valdoria",
                ReleaseDate = new DateTime(2015, 5, 19),
                Genre = Genre.ActionRPG,
                Description = "An epic open-world fantasy RPG where you play as Kael the Wanderer, a legendary bounty hunter. Traverse mystical kingdoms, battle mythical beasts, and shape the fate of Valdoria through your choices.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("ba8230ef-c6b3-4be8-9408-43d57b2049c3"),
                PublisherId = Guid.Parse("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Chronicles+of+Valdoria"
            },
            new Game
            {
                Id = Guid.Parse("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"),
                Title = "Neon Horizon 2088",
                ReleaseDate = new DateTime(2020, 12, 10),
                Genre = Genre.ActionRPG,
                Description = "A futuristic RPG set in the sprawling megacity of Nova Prime. Play as Zero, a cybernetically enhanced operative navigating corporate warfare, digital conspiracies, and the blurred line between human and machine.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("ba8230ef-c6b3-4be8-9408-43d57b2049c3"),
                PublisherId = Guid.Parse("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Neon+Horizon+2088"
            },
            new Game
            {
                Id = Guid.Parse("baaa2834-e824-4afb-8fd7-5911ad70390f"),
                Title = "Shattered Realms",
                ReleaseDate = new DateTime(2022, 2, 25),
                Genre = Genre.Soulslike,
                Description = "An expansive dark fantasy adventure combining punishing combat with vast exploration. Journey through the Fractured Lands to restore the Eternal Crown, facing legendary guardians and uncovering ancient secrets.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                PublisherId = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Shattered+Realms"
            },
            new Game
            {
                Id = Guid.Parse("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"),
                Title = "Blade of the Crimson Moon",
                ReleaseDate = new DateTime(2019, 3, 22),
                Genre = Genre.Soulslike,
                Description = "A challenging action game set in feudal-era Ashina. Play as a disgraced ronin seeking vengeance, mastering the way of the sword, and facing impossible odds against demonic warlords.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                PublisherId = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Blade+of+Crimson+Moon"
            },
            new Game
            {
                Id = Guid.Parse("b85a91b1-3d37-46fc-ba9a-db2593cd2616"),
                Title = "Ashen Legacy",
                ReleaseDate = new DateTime(2016, 4, 12),
                Genre = Genre.Soulslike,
                Description = "The final chapter in the Ashen saga, featuring brutal combat, interconnected gothic environments, and haunting lore. Venture through cursed kingdoms to end the cycle of undeath.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                PublisherId = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Ashen+Legacy"
            },
            new Game
            {
                Id = Guid.Parse("bbbc7561-9fac-4795-9606-3f94c28ea92f"),
                Title = "Nightmare Manor",
                ReleaseDate = new DateTime(2021, 5, 9),
                Genre = Genre.SurvivalHorror,
                Description = "A survival horror experience where you play as Marcus Vale searching for his missing sister in a cursed European estate. Face grotesque horrors, solve cryptic puzzles, and survive the night.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                PublisherId = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Nightmare+Manor"
            },
            new Game
            {
                Id = Guid.Parse("ed8b79ef-f6a2-40bd-87a4-1ff887f72b4d"),
                Title = "Titans of Olympus: Ascension",
                ReleaseDate = new DateTime(2022, 11, 9),
                Genre = Genre.Action,
                Description = "The epic finale of the Greek mythology saga. Ares and his son Atlas face the wrath of the gods as prophecy unfolds. Experience breathtaking combat, divine powers, and an emotionally charged story.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("4af65147-9ea5-4e30-a16d-de4750debe73"),
                PublisherId = Guid.Parse("3c77575e-bad2-4408-901b-9ec193a7cfba"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Titans+of+Olympus"
            },
            new Game
            {
                Id = Guid.Parse("96c36ea6-284b-464b-8ece-fae17e8b8cb7"),
                Title = "Voidborne",
                ReleaseDate = new DateTime(2017, 2, 24),
                Genre = Genre.Metroidvania,
                Description = "A challenging 2D metroidvania set in a dying mechanical world. Explore interconnected clockwork ruins, battle corrupted automatons, and uncover the secrets of the last organic beings.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("c0d88616-83ce-416a-a034-11264b776cc1"),
                PublisherId = Guid.Parse("b037a40e-b701-4bdc-a5c0-96b0fdd92619"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Voidborne"
            },
            new Game
            {
                Id = Guid.Parse("66bc4087-06f8-418e-9c4a-8908bc6b7771"),
                Title = "Infernal Descent",
                ReleaseDate = new DateTime(2020, 9, 17),
                Genre = Genre.Roguelike,
                Description = "A roguelike action game where you escape the depths of Tartarus. Battle through ever-changing demonic chambers, unlock divine boons, and unravel the mysteries of the underworld.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("7596ad8e-5eaa-44da-a716-6ab508873b52"),
                PublisherId = Guid.Parse("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Infernal+Descent"
            },
            new Game
            {
                Id = Guid.Parse("ecbda2ec-594d-419d-8b55-9ecb4358ef41"),
                Title = "Crystal Saga: Rebirth",
                ReleaseDate = new DateTime(2020, 4, 10),
                Genre = Genre.JRPG,
                Description = "A reimagining of the beloved JRPG classic. Join Strife and the Avalanche rebels in the industrial city of Midheim. Experience dynamic real-time combat and an expanded narrative.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"),
                PublisherId = Guid.Parse("1a94a706-493a-4ec2-b3e1-3cd802128f59"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Crystal+Saga+Rebirth"
            },
            new Game
            {
                Id = Guid.Parse("ab827d8a-311d-46cd-ad74-3742e07bfc5e"),
                Title = "Cosmic Frontier",
                ReleaseDate = new DateTime(2023, 9, 6),
                Genre = Genre.RPG,
                Description = "An ambitious space exploration RPG featuring procedurally generated star systems and countless planets to discover. Build your fleet, recruit crew members, and chart your destiny among the stars.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"),
                PublisherId = Guid.Parse("bad774ec-fc82-479d-800c-97c5bd602884"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Cosmic+Frontier"
            },
            new Game
            {
                Id = Guid.Parse("ed221215-c642-4077-b111-b770619b49c2"),
                Title = "Dimensional Rift",
                ReleaseDate = new DateTime(2011, 4, 19),
                Genre = Genre.Puzzle,
                Description = "A mind-bending puzzle-platformer featuring reality-warping mechanics. Manipulate space and time with your Rift Device, solve intricate challenges, and escape the mysterious testing facility.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("07dbe539-557e-4692-85e6-c875b1262a71"),
                PublisherId = Guid.Parse("e89f7368-94f7-4f55-8e0d-a03333659abb"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Dimensional+Rift"
            },
            new Game
            {
                Id = Guid.Parse("31af3372-60e4-4fa2-a2a6-35032f396601"),
                Title = "Shadow of the Shogun",
                ReleaseDate = new DateTime(2020, 7, 17),
                Genre = Genre.Action,
                Description = "An open-world samurai epic set in war-torn ancient Japan. Play as Takeshi Yamamoto, a masterless warrior defending his village from invading forces. Master the blade, embrace stealth, and forge your legend.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("ae06f7d4-7f50-411d-86af-c97ac324972e"),
                PublisherId = Guid.Parse("3c77575e-bad2-4408-901b-9ec193a7cfba"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Shadow+of+Shogun"
            },
            new Game
            {
                Id = Guid.Parse("faaa0756-7312-400c-9ba5-a581e4c90faf"),
                Title = "Beast Slayer: Wilds",
                ReleaseDate = new DateTime(2018, 1, 26),
                Genre = Genre.ActionRPG,
                Description = "Hunt colossal creatures across diverse ecosystems. Master unique weapon styles, craft legendary gear, and team up with fellow hunters to take down apex predators.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                PublisherId = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Beast+Slayer+Wilds"
            },
            new Game
            {
                Id = Guid.Parse("bb09f7dd-442f-4e5e-b12c-e13a68fffa2f"),
                Title = "Realm of Shadows",
                ReleaseDate = new DateTime(2023, 8, 3),
                Genre = Genre.TacticalRPG,
                Description = "An epic tactical RPG set in a world of magic and intrigue. Create your hero, forge alliances, and experience a branching narrative with countless endings based on your choices.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"),
                PublisherId = Guid.Parse("e2b91da3-3f00-4eb4-9785-643acbcf4c55"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Realm+of+Shadows"
            },
            new Game
            {
                Id = Guid.Parse("2ab1107e-a9a0-4bfc-b366-034ebb4a73a1"),
                Title = "Fist of Fury 6",
                ReleaseDate = new DateTime(2023, 6, 2),
                Genre = Genre.Fighting,
                Description = "The newest entry in the legendary martial arts franchise. Master a diverse roster of fighters, execute devastating combos, and prove your skills in online tournaments.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                PublisherId = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Fist+of+Fury+6"
            },
            new Game
            {
                Id = Guid.Parse("38c117e0-2416-4d69-a39a-2b4141c218b2"),
                Title = "Prismatic Dawn: Quest 33",
                ReleaseDate = new DateTime(2025, 4, 8),
                Genre = Genre.ActionRPG,
                Description = "A stunning action RPG set in a watercolor-painted fantasy realm. Lead an expedition against the Void Corruption and unlock the mysteries of the Prismatic Stones. Features tactical real-time combat.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                PublisherId = Guid.Parse("556474fc-388b-48a7-9bd2-c45528b09bb1"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Prismatic+Dawn"
            },
            new Game
            {
                Id = Guid.Parse("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                Title = "Galactic Crusaders II",
                ReleaseDate = new DateTime(2024, 9, 9),
                Genre = Genre.Shooter,
                Description = "An intense third-person shooter set in a dark sci-fi universe. Play as an elite soldier battling alien swarms with devastating weaponry and brutal melee attacks. Experience epic large-scale warfare.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                PublisherId = Guid.Parse("ed820b54-96c2-4fbf-a533-09b193c08028"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Galactic+Crusaders+II"
            }
        };

        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.HasData(games);
        }
    }
}
