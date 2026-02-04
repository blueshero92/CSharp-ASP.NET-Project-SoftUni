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
                ImageUrl = "https://via.placeholder.com/300x400?text=Whispers+of+Hollowmere"
            },
            new Game
            {
                Id = Guid.Parse("55562e8e-cdda-4bbb-8603-e9c619b46580"),
                Title = "Lorekeepers of Aethermoor",
                ReleaseDate = new DateTime(2015, 5, 19),
                Genre = Genre.ActionRPG,
                Description = "Become Varen Duskwood, a wandering archivist thrust into a continental war over forbidden knowledge. Traverse five distinct kingdoms, forge pacts with ancient spirits, and decide which truths deserve to survive.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("ba8230ef-c6b3-4be8-9408-43d57b2049c3"),
                PublisherId = Guid.Parse("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Lorekeepers+of+Aethermoor"
            },
            new Game
            {
                Id = Guid.Parse("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"),
                Title = "Synthetica: Fractured Grid",
                ReleaseDate = new DateTime(2020, 12, 10),
                Genre = Genre.ActionRPG,
                Description = "In the towering megastructure of Arcturus Prime, play as Cipher-7, a rogue synthetic seeking answers about their origins. Infiltrate corporate enclaves, hack neural networks, and question what it means to be alive.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("ba8230ef-c6b3-4be8-9408-43d57b2049c3"),
                PublisherId = Guid.Parse("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Synthetica+Fractured+Grid"
            },
            new Game
            {
                Id = Guid.Parse("baaa2834-e824-4afb-8fd7-5911ad70390f"),
                Title = "Thornveil Dominion",
                ReleaseDate = new DateTime(2022, 2, 25),
                Genre = Genre.Soulslike,
                Description = "The Blighted Throne awaits a new sovereign. Cross treacherous mirelands, challenge colossal guardians born of dying gods, and piece together a shattered realm where every victory carves your legend into stone.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                PublisherId = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Thornveil+Dominion"
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
                ImageUrl = "https://via.placeholder.com/300x400?text=Oath+of+Vermillion+Ronin"
            },
            new Game
            {
                Id = Guid.Parse("b85a91b1-3d37-46fc-ba9a-db2593cd2616"),
                Title = "Embervault Requiem",
                ReleaseDate = new DateTime(2016, 4, 12),
                Genre = Genre.Soulslike,
                Description = "Descend into the smoldering ruins of the Embervault, where the First Flame's remnants twist reality. Face relentless horrors, uncover the Pyrekeepers' tragic fate, and choose whether to rekindle hope or embrace oblivion.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                PublisherId = Guid.Parse("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Embervault+Requiem"
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
                ImageUrl = "https://via.placeholder.com/300x400?text=Ravencrest+Estate"
            },
            new Game
            {
                Id = Guid.Parse("ed8b79ef-f6a2-40bd-87a4-1ff887f72b4d"),
                Title = "Fury of the Stormborn",
                ReleaseDate = new DateTime(2022, 11, 9),
                Genre = Genre.Action,
                Description = "Ragnarok approaches as Halvard and his son Eirik defy fate itself. Wield thunder-forged weapons, traverse the Nine Branches, and challenge beings of primordial power in this climactic tale of sacrifice and legacy.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("4af65147-9ea5-4e30-a16d-de4750debe73"),
                PublisherId = Guid.Parse("3c77575e-bad2-4408-901b-9ec193a7cfba"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Fury+of+Stormborn"
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
                ImageUrl = "https://via.placeholder.com/300x400?text=Gearwright+Caverns"
            },
            new Game
            {
                Id = Guid.Parse("66bc4087-06f8-418e-9c4a-8908bc6b7771"),
                Title = "Pyreclimb Ascendancy",
                ReleaseDate = new DateTime(2020, 9, 17),
                Genre = Genre.Roguelike,
                Description = "Condemned to the Scorching Pits, your only escape lies upward. Battle through procedurally shifting infernal layers, bargain with imprisoned demigods for boons, and unravel why you were cast into eternal fire.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("7596ad8e-5eaa-44da-a716-6ab508873b52"),
                PublisherId = Guid.Parse("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Pyreclimb+Ascendancy"
            },
            new Game
            {
                Id = Guid.Parse("ecbda2ec-594d-419d-8b55-9ecb4358ef41"),
                Title = "Aethershard Chronicles",
                ReleaseDate = new DateTime(2020, 4, 10),
                Genre = Genre.JRPG,
                Description = "Join Zephyr and the Tempest Vanguard in overthrowing the Ironhaven Directorate. Wield crystallized aether in spectacular combat, forge bonds with unlikely allies, and witness a revolution that will reshape the world.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"),
                PublisherId = Guid.Parse("1a94a706-493a-4ec2-b3e1-3cd802128f59"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Aethershard+Chronicles"
            },
            new Game
            {
                Id = Guid.Parse("ab827d8a-311d-46cd-ad74-3742e07bfc5e"),
                Title = "Voidstrider Odyssey",
                ReleaseDate = new DateTime(2023, 9, 6),
                Genre = Genre.RPG,
                Description = "Command the starship Perihelion through uncharted galaxies. Establish colonies, negotiate with alien civilizations, and uncover the mystery of the Void Echoes—signals from a universe that should not exist.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"),
                PublisherId = Guid.Parse("bad774ec-fc82-479d-800c-97c5bd602884"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Voidstrider+Odyssey"
            },
            new Game
            {
                Id = Guid.Parse("ed221215-c642-4077-b111-b770619b49c2"),
                Title = "Paradox Chamber",
                ReleaseDate = new DateTime(2011, 4, 19),
                Genre = Genre.Puzzle,
                Description = "Awaken in the Tesseract Facility with no memory and a device that folds space. Navigate impossible geometries, outsmart a sardonic AI overseer, and piece together the catastrophe that erased your identity.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("07dbe539-557e-4692-85e6-c875b1262a71"),
                PublisherId = Guid.Parse("e89f7368-94f7-4f55-8e0d-a03333659abb"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Paradox+Chamber"
            },
            new Game
            {
                Id = Guid.Parse("31af3372-60e4-4fa2-a2a6-35032f396601"),
                Title = "Whisper of the Daimyo",
                ReleaseDate = new DateTime(2020, 7, 17),
                Genre = Genre.Action,
                Description = "When the Takeda clan falls, one warrior survives. Traverse war-scorched Yamashiro Province, master both blade and shadow, and decide whether vengeance or honor will define your path through feudal turmoil.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("ae06f7d4-7f50-411d-86af-c97ac324972e"),
                PublisherId = Guid.Parse("3c77575e-bad2-4408-901b-9ec193a7cfba"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Whisper+of+Daimyo"
            },
            new Game
            {
                Id = Guid.Parse("faaa0756-7312-400c-9ba5-a581e4c90faf"),
                Title = "Colossus Hunt: Untamed",
                ReleaseDate = new DateTime(2018, 1, 26),
                Genre = Genre.ActionRPG,
                Description = "The wild frontier teems with titanic beasts. Track legendary creatures across volcanic peaks and frozen tundras, craft gear from their remains, and join hunting parties to bring down monsters of mythic proportions.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                PublisherId = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Colossus+Hunt+Untamed"
            },
            new Game
            {
                Id = Guid.Parse("bb09f7dd-442f-4e5e-b12c-e13a68fffa2f"),
                Title = "Veilguard Accord",
                ReleaseDate = new DateTime(2023, 8, 3),
                Genre = Genre.TacticalRPG,
                Description = "The realm fractures as noble houses wage shadow wars. Assemble your Veilguard from rogues, mages, and exiled knights, navigate deadly court intrigue, and determine which faction claims the shattered crown.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"),
                PublisherId = Guid.Parse("e2b91da3-3f00-4eb4-9785-643acbcf4c55"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Veilguard+Accord"
            },
            new Game
            {
                Id = Guid.Parse("2ab1107e-a9a0-4bfc-b366-034ebb4a73a1"),
                Title = "Iron Legends VI",
                ReleaseDate = new DateTime(2023, 6, 2),
                Genre = Genre.Fighting,
                Description = "The World Martial Arts Summit returns with 32 fighters from across the globe. Master unique fighting disciplines, uncover each warrior's personal vendetta, and claim the legendary Iron Championship.",
                IsDeleted = false,
                DeveloperId = Guid.Parse("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                PublisherId = Guid.Parse("e23228f4-77a4-448c-b816-ccb1826eed36"),
                ImageUrl = "https://via.placeholder.com/300x400?text=Iron+Fist+Legends+VI"
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
                ImageUrl = "https://via.placeholder.com/300x400?text=Wanderers+Shattered+Isles"
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
                ImageUrl = "https://via.placeholder.com/300x400?text=Stellarborne+Legionnaires+II"
            }
        };

        public void Configure(EntityTypeBuilder<Game> entity)
        {
            entity.HasData(games);
        }
    }
}
