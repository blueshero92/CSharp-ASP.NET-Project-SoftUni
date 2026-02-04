using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGameTitlesAndDescriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "The Hegemony's elite answer humanity's darkest hour. Lead Sergeant Kira Vance through xeno-infested worlds, command squad tactics in brutal firefights, and uncover a conspiracy that threatens the entire frontier.", "https://via.placeholder.com/300x400?text=Stellarborne+Legionnaires+II" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2ab1107e-a9a0-4bfc-b366-034ebb4a73a1"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "The World Martial Arts Summit returns with 32 fighters from across the globe. Master unique fighting disciplines, uncover each warrior's personal vendetta, and claim the legendary Iron Championship.", "https://via.placeholder.com/300x400?text=Iron+Fist+Legends+VI", "Iron Legends VI" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("31af3372-60e4-4fa2-a2a6-35032f396601"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "When the Takeda clan falls, one warrior survives. Traverse war-scorched Yamashiro Province, master both blade and shadow, and decide whether vengeance or honor will define your path through feudal turmoil.", "https://via.placeholder.com/300x400?text=Whisper+of+Daimyo", "Whisper of the Daimyo" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("38c117e0-2416-4d69-a39a-2b4141c218b2"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Sail between floating archipelagos in a world torn asunder by magical cataclysm. Recruit a crew of outcasts, uncover the secrets of the Sundering, and restore balance before the remaining islands fall into the void.", "https://via.placeholder.com/300x400?text=Wanderers+Shattered+Isles", "Wanderers of the Shattered Isles" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "In the towering megastructure of Arcturus Prime, play as Cipher-7, a rogue synthetic seeking answers about their origins. Infiltrate corporate enclaves, hack neural networks, and question what it means to be alive.", "https://via.placeholder.com/300x400?text=Synthetica+Fractured+Grid", "Synthetica: Fractured Grid" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Disgraced and left for dead, rise as a nameless swordsman in the war-ravaged Kiyohara Province. Master the forbidden Crimson Style, hunt those who betrayed you, and carve a path through oni and warlord alike.", "https://via.placeholder.com/300x400?text=Oath+of+Vermillion+Ronin", "Oath of the Vermillion Ronin" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("55562e8e-cdda-4bbb-8603-e9c619b46580"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Become Varen Duskwood, a wandering archivist thrust into a continental war over forbidden knowledge. Traverse five distinct kingdoms, forge pacts with ancient spirits, and decide which truths deserve to survive.", "https://via.placeholder.com/300x400?text=Lorekeepers+of+Aethermoor", "Lorekeepers of Aethermoor" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("66bc4087-06f8-418e-9c4a-8908bc6b7771"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Condemned to the Scorching Pits, your only escape lies upward. Battle through procedurally shifting infernal layers, bargain with imprisoned demigods for boons, and unravel why you were cast into eternal fire.", "https://via.placeholder.com/300x400?text=Pyreclimb+Ascendancy", "Pyreclimb Ascendancy" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("96c36ea6-284b-464b-8ece-fae17e8b8cb7"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Deep within the Brass Hollows, a clockwork civilization crumbles. Guide the last Tinker through labyrinthine foundries, rewire ancient mechanisms, and discover why the Great Engine fell silent centuries ago.", "https://via.placeholder.com/300x400?text=Gearwright+Caverns", "Gearwright Caverns" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ab827d8a-311d-46cd-ad74-3742e07bfc5e"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Command the starship Perihelion through uncharted galaxies. Establish colonies, negotiate with alien civilizations, and uncover the mystery of the Void Echoes—signals from a universe that should not exist.", "https://via.placeholder.com/300x400?text=Voidstrider+Odyssey", "Voidstrider Odyssey" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("adaaad5d-49ee-4f9d-9de8-3d21f5333e6b"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Venture into the abandoned Bloodlight Sanatorium where shadows breathe and walls remember. As paranormal investigator Elena Voss, unravel decades of buried secrets while evading entities that feed on fear itself.", "https://via.placeholder.com/300x400?text=Whispers+of+Hollowmere", "Bloodlight Sanatorium" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b85a91b1-3d37-46fc-ba9a-db2593cd2616"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Descend into the smoldering ruins of the Embervault, where the First Flame's remnants twist reality. Face relentless horrors, uncover the Pyrekeepers' tragic fate, and choose whether to rekindle hope or embrace oblivion.", "https://via.placeholder.com/300x400?text=Embervault+Requiem", "Embervault Requiem" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("baaa2834-e824-4afb-8fd7-5911ad70390f"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "The Blighted Throne awaits a new sovereign. Cross treacherous mirelands, challenge colossal guardians born of dying gods, and piece together a shattered realm where every victory carves your legend into stone.", "https://via.placeholder.com/300x400?text=Thornveil+Dominion", "Thornveil Dominion" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bb09f7dd-442f-4e5e-b12c-e13a68fffa2f"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "The realm fractures as noble houses wage shadow wars. Assemble your Veilguard from rogues, mages, and exiled knights, navigate deadly court intrigue, and determine which faction claims the shattered crown.", "https://via.placeholder.com/300x400?text=Veilguard+Accord", "Veilguard Accord" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bbbc7561-9fac-4795-9606-3f94c28ea92f"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Something ancient stirs beneath Ravencrest Estate. As Dominic Ashford, search for your missing daughter through decaying halls, confront manifestations of familial guilt, and survive a night where the house itself hunts you.", "https://via.placeholder.com/300x400?text=Ravencrest+Estate", "The Mystery of Ravencrest" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ecbda2ec-594d-419d-8b55-9ecb4358ef41"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Join Zephyr and the Tempest Vanguard in overthrowing the Ironhaven Directorate. Wield crystallized aether in spectacular combat, forge bonds with unlikely allies, and witness a revolution that will reshape the world.", "https://via.placeholder.com/300x400?text=Aethershard+Chronicles", "Aethershard Chronicles" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ed221215-c642-4077-b111-b770619b49c2"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Awaken in the Tesseract Facility with no memory and a device that folds space. Navigate impossible geometries, outsmart a sardonic AI overseer, and piece together the catastrophe that erased your identity.", "https://via.placeholder.com/300x400?text=Paradox+Chamber", "Paradox Chamber" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ed8b79ef-f6a2-40bd-87a4-1ff887f72b4d"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Ragnarok approaches as Halvard and his son Eirik defy fate itself. Wield thunder-forged weapons, traverse the Nine Branches, and challenge beings of primordial power in this climactic tale of sacrifice and legacy.", "https://via.placeholder.com/300x400?text=Fury+of+Stormborn", "Fury of the Stormborn" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("faaa0756-7312-400c-9ba5-a581e4c90faf"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "The wild frontier teems with titanic beasts. Track legendary creatures across volcanic peaks and frozen tundras, craft gear from their remains, and join hunting parties to bring down monsters of mythic proportions.", "https://via.placeholder.com/300x400?text=Colossus+Hunt+Untamed", "Colossus Hunt: Untamed" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "An intense third-person shooter set in a dark sci-fi universe. Play as an elite soldier battling alien swarms with devastating weaponry and brutal melee attacks. Experience epic large-scale warfare.", "https://via.placeholder.com/300x400?text=Galactic+Crusaders+II" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2ab1107e-a9a0-4bfc-b366-034ebb4a73a1"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "The newest entry in the legendary martial arts franchise. Master a diverse roster of fighters, execute devastating combos, and prove your skills in online tournaments.", "https://via.placeholder.com/300x400?text=Fist+of+Fury+6", "Fist of Fury 6" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("31af3372-60e4-4fa2-a2a6-35032f396601"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An open-world samurai epic set in war-torn ancient Japan. Play as Takeshi Yamamoto, a masterless warrior defending his village from invading forces. Master the blade, embrace stealth, and forge your legend.", "https://via.placeholder.com/300x400?text=Shadow+of+Shogun", "Shadow of the Shogun" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("38c117e0-2416-4d69-a39a-2b4141c218b2"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A stunning action RPG set in a watercolor-painted fantasy realm. Lead an expedition against the Void Corruption and unlock the mysteries of the Prismatic Stones. Features tactical real-time combat.", "https://via.placeholder.com/300x400?text=Prismatic+Dawn", "Prismatic Dawn: Quest 33" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A futuristic RPG set in the sprawling megacity of Nova Prime. Play as Zero, a cybernetically enhanced operative navigating corporate warfare, digital conspiracies, and the blurred line between human and machine.", "https://via.placeholder.com/300x400?text=Neon+Horizon+2088", "Neon Horizon 2088" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A challenging action game set in feudal-era Ashina. Play as a disgraced ronin seeking vengeance, mastering the way of the sword, and facing impossible odds against demonic warlords.", "https://via.placeholder.com/300x400?text=Blade+of+Crimson+Moon", "Blade of the Crimson Moon" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("55562e8e-cdda-4bbb-8603-e9c619b46580"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An epic open-world fantasy RPG where you play as Kael the Wanderer, a legendary bounty hunter. Traverse mystical kingdoms, battle mythical beasts, and shape the fate of Valdoria through your choices.", "https://via.placeholder.com/300x400?text=Chronicles+of+Valdoria", "Chronicles of Valdoria" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("66bc4087-06f8-418e-9c4a-8908bc6b7771"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A roguelike action game where you escape the depths of Tartarus. Battle through ever-changing demonic chambers, unlock divine boons, and unravel the mysteries of the underworld.", "https://via.placeholder.com/300x400?text=Infernal+Descent", "Infernal Descent" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("96c36ea6-284b-464b-8ece-fae17e8b8cb7"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A challenging 2D metroidvania set in a dying mechanical world. Explore interconnected clockwork ruins, battle corrupted automatons, and uncover the secrets of the last organic beings.", "https://via.placeholder.com/300x400?text=Voidborne", "Voidborne" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ab827d8a-311d-46cd-ad74-3742e07bfc5e"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An ambitious space exploration RPG featuring procedurally generated star systems and countless planets to discover. Build your fleet, recruit crew members, and chart your destiny among the stars.", "https://via.placeholder.com/300x400?text=Cosmic+Frontier", "Cosmic Frontier" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("adaaad5d-49ee-4f9d-9de8-3d21f5333e6b"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A psychological horror experience where players explore a fog-covered abandoned asylum. Uncover the dark history of Dr. Mallory's experiments while evading twisted manifestations of past patients.", "https://via.placeholder.com/300x400?text=Echoes+of+the+Abyss", "Echoes of the Abyss" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b85a91b1-3d37-46fc-ba9a-db2593cd2616"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "The final chapter in the Ashen saga, featuring brutal combat, interconnected gothic environments, and haunting lore. Venture through cursed kingdoms to end the cycle of undeath.", "https://via.placeholder.com/300x400?text=Ashen+Legacy", "Ashen Legacy" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("baaa2834-e824-4afb-8fd7-5911ad70390f"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An expansive dark fantasy adventure combining punishing combat with vast exploration. Journey through the Fractured Lands to restore the Eternal Crown, facing legendary guardians and uncovering ancient secrets.", "https://via.placeholder.com/300x400?text=Shattered+Realms", "Shattered Realms" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bb09f7dd-442f-4e5e-b12c-e13a68fffa2f"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An epic tactical RPG set in a world of magic and intrigue. Create your hero, forge alliances, and experience a branching narrative with countless endings based on your choices.", "https://via.placeholder.com/300x400?text=Realm+of+Shadows", "Realm of Shadows" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bbbc7561-9fac-4795-9606-3f94c28ea92f"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A survival horror experience where you play as Marcus Vale searching for his missing sister in a cursed European estate. Face grotesque horrors, solve cryptic puzzles, and survive the night.", "https://via.placeholder.com/300x400?text=Nightmare+Manor", "Nightmare Manor" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ecbda2ec-594d-419d-8b55-9ecb4358ef41"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A reimagining of the beloved JRPG classic. Join Strife and the Avalanche rebels in the industrial city of Midheim. Experience dynamic real-time combat and an expanded narrative.", "https://via.placeholder.com/300x400?text=Crystal+Saga+Rebirth", "Crystal Saga: Rebirth" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ed221215-c642-4077-b111-b770619b49c2"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A mind-bending puzzle-platformer featuring reality-warping mechanics. Manipulate space and time with your Rift Device, solve intricate challenges, and escape the mysterious testing facility.", "https://via.placeholder.com/300x400?text=Dimensional+Rift", "Dimensional Rift" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ed8b79ef-f6a2-40bd-87a4-1ff887f72b4d"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "The epic finale of the Greek mythology saga. Ares and his son Atlas face the wrath of the gods as prophecy unfolds. Experience breathtaking combat, divine powers, and an emotionally charged story.", "https://via.placeholder.com/300x400?text=Titans+of+Olympus", "Titans of Olympus: Ascension" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("faaa0756-7312-400c-9ba5-a581e4c90faf"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Hunt colossal creatures across diverse ecosystems. Master unique weapon styles, craft legendary gear, and team up with fellow hunters to take down apex predators.", "https://via.placeholder.com/300x400?text=Beast+Slayer+Wilds", "Beast Slayer: Wilds" });
        }
    }
}
