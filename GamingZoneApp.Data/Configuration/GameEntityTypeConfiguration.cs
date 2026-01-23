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
                Title = "Silent Hill 2 Remake",
                ReleaseDate = new DateTime(2024, 10, 8),
                Genre = Genre.SurvivalHorror,
                Description = "A faithful remake of the classic psychological horror masterpiece. Players navigate the fog-shrouded town of Silent Hill as James Sunderland, uncovering dark secrets and confronting manifestations of guilt and despair.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("f75ee60f-4615-4bca-8a21-be507c6d3a49"),
                PublisherId = Guid.Parse("a0529584-8eb3-4880-8674-c4e5cc67b487"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Silent+Hill+2"
            },
            new Game
            {
                Id = Guid.Parse("55562e8e-cdda-4bbb-8603-e9c619b46580"),
                Title = "The Witcher 3: Wild Hunt",
                ReleaseDate = new DateTime(2015, 5, 19),
                Genre = Genre.ActionRPG,
                Description = "An open-world fantasy RPG where you play as Geralt of Rivia, a monster hunter. Complete quests, explore vast landscapes, and make choices that shape the world around you. The game features one of gaming's most compelling stories.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("ba8230ef-c6b3-4be8-9408-43d57b2049c3"),
                PublisherId = Guid.Parse("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Witcher+3"
            },
            new Game
            {
                Id = Guid.Parse("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"),
                Title = "Cyberpunk 2077",
                ReleaseDate = new DateTime(2020, 12, 10),
                Genre = Genre.ActionRPG,
                Description = "An ambitious dystopian RPG set in the megacity of Night City. Play as V, a customizable mercenary navigating a world filled with corporate intrigue, high-tech augmentations, and morally complex choices.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("ba8230ef-c6b3-4be8-9408-43d57b2049c3"),
                PublisherId = Guid.Parse("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Cyberpunk+2077"
            },
            new Game
            {
                Id = Guid.Parse("baaa2834-e824-4afb-8fd7-5911ad70390f"),
                Title = "Elden Ring",
                ReleaseDate = new DateTime(2022, 2, 25),
                Genre = Genre.Soulslike,
                Description = "An open-world masterpiece combining challenging combat with expansive exploration. Traverse the Lands Between to restore the Elden Ring, facing formidable bosses and discovering hidden dungeons.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                PublisherId = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Elden+Ring"
            },
            new Game
            {
                Id = Guid.Parse("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"),
                Title = "Sekiro: Shadows Die Twice",
                ReleaseDate = new DateTime(2019, 3, 22),
                Genre = Genre.Soulslike,
                Description = "A challenging action-adventure game set in Sengoku-era Japan. Play as a shinobi warrior seeking revenge, utilizing parkour, sword combat, and skill-based gameplay. Defeat legendary bosses in this FromSoftware title.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                PublisherId = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Sekiro"
            },
            new Game
            {
                Id = Guid.Parse("b85a91b1-3d37-46fc-ba9a-db2593cd2616"),
                Title = "Dark Souls III",
                ReleaseDate = new DateTime(2016, 4, 12),
                Genre = Genre.Soulslike,
                Description = "The conclusion to the Dark Souls trilogy, featuring challenging combat, intricate level design, and dark fantasy storytelling. Explore interconnected worlds and defeat powerful enemies.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                PublisherId = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Dark+Souls+III"
            },
            new Game
            {
                Id = Guid.Parse("bbbc7561-9fac-4795-9606-3f94c28ea92f"),
                Title = "Resident Evil Village",
                ReleaseDate = new DateTime(2021, 5, 9),
                Genre = Genre.SurvivalHorror,
                Description = "A survival horror game where you play as Ethan Winters searching for his daughter in a mysterious village. Encounter terrifying creatures, solve puzzles, and experience intense action.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                PublisherId = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Resident+Evil"
            },
            new Game
            {
                Id = Guid.Parse("ed8b79ef-f6a2-40bd-87a4-1ff887f72b4d"),
                Title = "God of War Ragnarök",
                ReleaseDate = new DateTime(2022, 11, 9),
                Genre = Genre.Action,
                Description = "The epic conclusion to the Norse saga. Kratos and Atreus face the apocalypse as they prepare for the final battle. Experience stunning visuals, intense combat, and an emotional narrative.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("4af65147-9ea5-4e30-a16d-de4750debe73"),
                PublisherId = Guid.Parse("3c77575e-bad2-4408-901b-9ec193a7cfba"),
                ImageUrl = "https://via.placeholder.com/300x400?text=God+of+War"
            },
            new Game
            {
                Id = Guid.Parse("96c36ea6-284b-464b-8ece-fae17e8b8cb7"),
                Title = "Hollow Knight",
                ReleaseDate = new DateTime(2017, 2, 24),
                Genre = Genre.Metroidvania,
                Description = "A challenging 2D metroidvania game set in a haunted kingdom of insects. Explore interconnected caverns, defeat powerful bosses, and uncover the dark secrets of Hallownest.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("c0d88616-83ce-416a-a034-11264b776cc1"),
                PublisherId = Guid.Parse("b037a40e-b701-4bdc-a5c0-96b0fdd92619"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Hollow+Knight"
            },
            new Game
            {
                Id = Guid.Parse("66bc4087-06f8-418e-9c4a-8908bc6b7771"),
                Title = "Hades",
                ReleaseDate = new DateTime(2020, 9, 17),
                Genre = Genre.Roguelike,
                Description = "A roguelike action game where you escape from Hades, the Greek underworld. Repeatedly attempt runs through procedurally generated dungeons, upgrading your abilities and uncovering rich mythology.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("7596ad8e-5eaa-44da-a716-6ab508873b52"),
                PublisherId = Guid.Parse("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Hades"
            },
            new Game
            {
                Id = Guid.Parse("ecbda2ec-594d-419d-8b55-9ecb4358ef41"),
                Title = "Final Fantasy VII Remake",
                ReleaseDate = new DateTime(2020, 4, 10),
                Genre = Genre.JRPG,
                Description = "A modern reimagining of the classic JRPG. Play as Cloud and his allies in the sprawling metropolis of Midgar. Experience real-time combat and an expanded story.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"),
                PublisherId = Guid.Parse("1a94a706-493a-4ec2-b3e1-3cd802128f59"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Final+Fantasy+VII"
            },
            new Game
            {
                Id = Guid.Parse("ab827d8a-311d-46cd-ad74-3742e07bfc5e"),
                Title = "Starfield",
                ReleaseDate = new DateTime(2023, 9, 6),
                Genre = Genre.RPG,
                Description = "Bethesda's ambitious space RPG featuring procedurally generated planets and a massive universe to explore. Create your character, build your spaceship, and embark on quests across hundreds of worlds.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"),
                PublisherId = Guid.Parse("bad774ec-fc82-479d-800c-97c5bd602884"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Starfield"
            },
            new Game
            {
                Id = Guid.Parse("ed221215-c642-4077-b111-b770619b49c2"),
                Title = "Portal 2",
                ReleaseDate = new DateTime(2011, 4, 19),
                Genre = Genre.Puzzle,
                Description = "A puzzle-platformer featuring innovative portal mechanics. Use your portal gun to manipulate space, solve complex puzzles, and navigate through hilarious tests.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("07dbe539-557e-4692-85e6-c875b1262a71"),
                PublisherId = Guid.Parse("e89f7368-94f7-4f55-8e0d-a03333659abb"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Portal+2"
            },
            new Game
            {
                Id = Guid.Parse("31af3372-60e4-4fa2-a2a6-35032f396601"),
                Title = "Ghost of Tsushima",
                ReleaseDate = new DateTime(2020, 7, 17),
                Genre = Genre.Action,
                Description = "An open-world action-adventure set in 13th-century Japan. Play as Jin Sakai, a samurai warrior defending his homeland from invading Mongol forces. Experience stunning visuals, intense sword combat, and a compelling tale of honor and sacrifice.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("ae06f7d4-7f50-411d-86af-c97ac324972e"),
                PublisherId = Guid.Parse("3c77575e-bad2-4408-901b-9ec193a7cfba"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Ghost+of+Tsushima"
            },
            new Game
            {
                Id = Guid.Parse("faaa0756-7312-400c-9ba5-a581e4c90faf"),
                Title = "Monster Hunter: World",
                ReleaseDate = new DateTime(2018, 1, 26),
                Genre = Genre.ActionRPG,
                Description = "Hunt massive monsters in a sprawling open world. Master various weapons, craft armor, and work with other hunters to take down legendary creatures.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                PublisherId = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Monster+Hunter"
            },
            new Game
            {
                Id = Guid.Parse("bb09f7dd-442f-4e5e-b12c-e13a68fffa2f"),
                Title = "Baldur's Gate 3",
                ReleaseDate = new DateTime(2023, 8, 3),
                Genre = Genre.TacticalRPG,
                Description = "A massive CRPG set in the Dungeons & Dragons universe. Create your character, make impactful choices, and enjoy an incredibly deep story with multiple endings.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"),
                PublisherId = Guid.Parse("e2b91da3-3f00-4eb4-9785-643acbcf4c55"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Baldurs+Gate+3"
            },
            new Game
            {
                Id = Guid.Parse("2ab1107e-a9a0-4bfc-b366-034ebb4a73a1"),
                Title = "Street Fighter 6",
                ReleaseDate = new DateTime(2023, 6, 2),
                Genre = Genre.Fighting,
                Description = "The latest entry in the legendary fighting game franchise. Master diverse characters, execute precise combos, and compete online or offline.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                PublisherId = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Street+Fighter+6"
            },
            new Game
            {
                Id = Guid.Parse("38c117e0-2416-4d69-a39a-2b4141c218b2"),
                Title = "Clair Obscur: Expedition 33",
                ReleaseDate = new DateTime(2025, 4, 8),
                Genre = Genre.ActionRPG,
                Description = "A vibrant action RPG set in a beautiful watercolor-inspired world. Embark on an expedition to defeat cosmic corruption and discover the secrets of the land. Features real-time tactical combat and rich exploration.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                PublisherId = Guid.Parse("556474fc-388b-48a7-9bd2-c45528b09bb1"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Clair+Obscur"
            },
            new Game
            {
                Id = Guid.Parse("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                Title = "Warhammer 40,000: Space Marine 2",
                ReleaseDate = new DateTime(2024, 9, 9),
                Genre = Genre.Shooter,
                Description = "An intense third-person shooter set in the grim Warhammer 40K universe. Play as a Space Marine and battle hordes of enemies using devastating weapons and melee combat. Experience fast-paced action and epic battles.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                PublisherId = Guid.Parse("ed820b54-96c2-4fbf-a533-09b193c08028"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Space+Marine+2"
            }
        };

        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.HasData(games);
        }
    }
}
