using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeededData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("07dbe539-557e-4692-85e6-c875b1262a71"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Innovative video game developer known for creating genre-defining games with exceptional design, storytelling, and groundbreaking mechanics.", "https://via.placeholder.com/300x300?text=Quantum+Rift", "Quantum Rift Software" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Veteran video game developer and publisher known for creating legendary franchises and survival horror masterpieces with intense action gameplay.", "https://via.placeholder.com/300x300?text=Phoenix+Rising", "Phoenix Rising Interactive" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Leading video game developer famous for creating massive open-world RPGs with deep worlds and endless exploration opportunities.", "https://via.placeholder.com/300x300?text=Starbound+Studios", "Starbound Studios" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Dynamic video game developer known for creating fast-paced action games with intense gameplay and large-scale multiplayer experiences.", "https://via.placeholder.com/300x300?text=Titan+Forge", "Titan Forge Games" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("4af65147-9ea5-4e30-a16d-de4750debe73"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Premier video game developer famous for creating epic narrative-driven action games with stunning visuals and cinematic presentation.", "https://via.placeholder.com/300x300?text=Olympus+Digital", "Olympus Digital" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "European video game developer known for creating deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.", "https://via.placeholder.com/300x300?text=Arcane+Depths", "Arcane Depths Interactive" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Boutique video game developer founded by industry veterans, known for creating innovative action RPGs with unique artistic vision and creative gameplay.", "https://via.placeholder.com/300x300?text=Prism+Voyage", "Prism Voyage Studios" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("7596ad8e-5eaa-44da-a716-6ab508873b52"),
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://via.placeholder.com/300x300?text=Ember+Flame", "Ember Flame Games" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("ae06f7d4-7f50-411d-86af-c97ac324972e"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Acclaimed video game developer known for creating open-world action games with compelling narratives, stunning visual artistry, and engaging gameplay.", "https://via.placeholder.com/300x300?text=Shadow+Blade", "Shadow Blade Productions" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("ba8230ef-c6b3-4be8-9408-43d57b2049c3"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Award-winning studio renowned for creating story-driven, visually stunning games with deep narratives and meaningful player choice.", "https://via.placeholder.com/300x300?text=Crimson+Forge", "Crimson Forge Entertainment" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("c0d88616-83ce-416a-a034-11264b776cc1"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Independent video game developer known for creating meticulously crafted indie games with exceptional art direction and challenging gameplay.", "https://via.placeholder.com/300x300?text=Pixel+Hollow", "Pixel Hollow Studios" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Major video game developer and publisher known for creating epic JRPGs and iconic franchises with memorable characters and engaging storylines.", "https://via.placeholder.com/300x300?text=Crystal+Dawn", "Crystal Dawn Entertainment" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Renowned video game developer celebrated for crafting challenging and immersive gameplay experiences with intricate level design and compelling boss battles.", "https://via.placeholder.com/300x300?text=Iron+Tempest", "Iron Tempest Games" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("f75ee60f-4615-4bca-8a21-be507c6d3a49"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "European video game developer known for creating atmospheric psychological horror experiences with cutting-edge graphics and immersive storytelling.", "https://via.placeholder.com/300x300?text=Nightshade+Studios", "Nightshade Studios" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An intense third-person shooter set in a dark sci-fi universe. Play as an elite soldier battling alien swarms with devastating weaponry and brutal melee attacks. Experience epic large-scale warfare.", "https://via.placeholder.com/300x400?text=Galactic+Crusaders+II", "Galactic Crusaders II" });

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

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"),
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://via.placeholder.com/300x300?text=Ember+Flame+Pub", "Ember Flame Publishing" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("1a94a706-493a-4ec2-b3e1-3cd802128f59"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Major video game publisher known for publishing epic JRPGs and iconic franchises with memorable characters and engaging storytelling.", "https://via.placeholder.com/300x300?text=Crystal+Dawn+Pub", "Crystal Dawn Publishing" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("3c77575e-bad2-4408-901b-9ec193a7cfba"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Major video game publisher famous for publishing exclusive titles and supporting innovative game development across multiple platforms.", "https://via.placeholder.com/300x300?text=Nexus+Global", "Nexus Global Entertainment" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("556474fc-388b-48a7-9bd2-c45528b09bb1"),
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://via.placeholder.com/300x300?text=Prism+Voyage+Pub", "Prism Voyage Publishing" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Major video game publisher specializing in action-packed games and collaborations with renowned developers to create premium gaming experiences.", "https://via.placeholder.com/300x300?text=Dragon+Crown", "Dragon Crown Entertainment" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("a0529584-8eb3-4880-8674-c4e5cc67b487"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "International video game publisher recognized for creating atmospheric and psychologically intense horror games that push the boundaries of interactive storytelling.", "https://via.placeholder.com/300x300?text=Phantom+Gate", "Phantom Gate Publishing" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("b037a40e-b701-4bdc-a5c0-96b0fdd92619"),
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://via.placeholder.com/300x300?text=Pixel+Hollow+Pub", "Pixel Hollow Publishing" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("bad774ec-fc82-479d-800c-97c5bd602884"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Leading video game publisher famous for publishing massive open-world RPGs and supporting innovative game development.", "https://via.placeholder.com/300x300?text=Starbound+Pub", "Starbound Publishing" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Established video game publisher known for creating legendary survival horror franchises and action games with exceptional design and storytelling.", "https://via.placeholder.com/300x300?text=Phoenix+Rising+Pub", "Phoenix Rising Publishing" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e2b91da3-3f00-4eb4-9785-643acbcf4c55"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "European video game publisher known for publishing deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.", "https://via.placeholder.com/300x300?text=Arcane+Depths+Pub", "Arcane Depths Publishing" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e89f7368-94f7-4f55-8e0d-a03333659abb"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Innovative video game publisher and digital distribution platform known for genre-defining games and digital distribution excellence.", "https://via.placeholder.com/300x300?text=Quantum+Rift+Pub", "Quantum Rift Publishing" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("ed820b54-96c2-4fbf-a533-09b193c08028"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "International video game publisher known for publishing action-packed games and supporting diverse game genres from independent to AAA studios.", "https://via.placeholder.com/300x300?text=Titan+Forge+Pub", "Titan Forge Publishing" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "European video game publisher and distributor known for supporting independent studios and publishing award-winning narrative-driven games.", "https://via.placeholder.com/300x300?text=Crimson+Forge+Pub", "Crimson Forge Publishing" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("07dbe539-557e-4692-85e6-c875b1262a71"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "American video game developer and digital distribution company known for creating innovative, genre-defining games with exceptional design and storytelling.", "https://via.placeholder.com/300x300?text=Valve", "Valve" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Japanese video game developer and publisher known for creating legendary franchises and survival horror masterpieces with intense action gameplay.", "https://via.placeholder.com/300x300?text=Capcom", "Capcom" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "American video game developer owned by Microsoft, famous for creating massive open-world RPGs with deep worlds and endless exploration opportunities.", "https://via.placeholder.com/300x300?text=Bethesda", "Bethesda Game Studios" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "American video game developer known for creating fast-paced action games with intense gameplay and faithful adaptations of popular franchises.", "https://via.placeholder.com/300x300?text=Saber+Interactive", "Saber Interactive" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("4af65147-9ea5-4e30-a16d-de4750debe73"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "American video game developer owned by Sony Interactive Entertainment, famous for creating epic narrative-driven action games with stunning visuals.", "https://via.placeholder.com/300x300?text=Santa+Monica+Studio", "Santa Monica Studio" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Belgian video game developer known for creating deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.", "https://via.placeholder.com/300x300?text=Larian+Studios", "Larian Studios" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "French video game developer founded by veteran developers, known for creating innovative action RPGs with unique artistic vision and creative gameplay.", "https://via.placeholder.com/300x300?text=Sandfall+Interactive", "Sandfall Interactive" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("7596ad8e-5eaa-44da-a716-6ab508873b52"),
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://via.placeholder.com/300x300?text=Supergiant+Games", "Supergiant Games" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("ae06f7d4-7f50-411d-86af-c97ac324972e"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "American video game developer known for creating open-world action games with compelling narratives, stunning visual artistry, and engaging gameplay mechanics.", "https://via.placeholder.com/300x300?text=Sucker+Punch", "Sucker Punch Productions" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("ba8230ef-c6b3-4be8-9408-43d57b2049c3"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Polish video game studio renowned for creating story-driven, visually stunning games with deep narratives and meaningful player choice.", "https://via.placeholder.com/300x300?text=CD+Projekt+Red", "CD Projekt Red" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("c0d88616-83ce-416a-a034-11264b776cc1"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Independent Australian video game developer known for creating meticulously crafted indie games with exceptional art direction and challenging gameplay.", "https://via.placeholder.com/300x300?text=Team+Cherry", "Team Cherry" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Japanese video game developer and publisher known for creating epic JRPGs and iconic franchises with memorable characters and engaging storylines.", "https://via.placeholder.com/300x300?text=Square+Enix", "Square Enix" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Renowned Japanese video game developer celebrated for crafting challenging and immersive gameplay experiences with intricate level design and compelling boss battles.", "https://via.placeholder.com/300x300?text=FromSoftware", "FromSoftware" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("f75ee60f-4615-4bca-8a21-be507c6d3a49"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Polish video game developer known for creating atmospheric psychological horror experiences with cutting-edge graphics and immersive storytelling.", "https://via.placeholder.com/300x300?text=Bloober+Team", "Bloober Team" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An intense third-person shooter set in the grim Warhammer 40K universe. Play as a Space Marine and battle hordes of enemies using devastating weapons and melee combat. Experience fast-paced action and epic battles.", "https://via.placeholder.com/300x400?text=Space+Marine+2", "Warhammer 40,000: Space Marine 2" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2ab1107e-a9a0-4bfc-b366-034ebb4a73a1"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "The latest entry in the legendary fighting game franchise. Master diverse characters, execute precise combos, and compete online or offline.", "https://via.placeholder.com/300x400?text=Street+Fighter+6", "Street Fighter 6" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("31af3372-60e4-4fa2-a2a6-35032f396601"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An open-world action-adventure set in 13th-century Japan. Play as Jin Sakai, a samurai warrior defending his homeland from invading Mongol forces. Experience stunning visuals, intense sword combat, and a compelling tale of honor and sacrifice.", "https://via.placeholder.com/300x400?text=Ghost+of+Tsushima", "Ghost of Tsushima" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("38c117e0-2416-4d69-a39a-2b4141c218b2"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A vibrant action RPG set in a beautiful watercolor-inspired world. Embark on an expedition to defeat cosmic corruption and discover the secrets of the land. Features real-time tactical combat and rich exploration.", "https://via.placeholder.com/300x400?text=Clair+Obscur", "Clair Obscur: Expedition 33" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An ambitious dystopian RPG set in the megacity of Night City. Play as V, a customizable mercenary navigating a world filled with corporate intrigue, high-tech augmentations, and morally complex choices.", "https://via.placeholder.com/300x400?text=Cyberpunk+2077", "Cyberpunk 2077" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A challenging action-adventure game set in Sengoku-era Japan. Play as a shinobi warrior seeking revenge, utilizing parkour, sword combat, and skill-based gameplay. Defeat legendary bosses in this FromSoftware title.", "https://via.placeholder.com/300x400?text=Sekiro", "Sekiro: Shadows Die Twice" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("55562e8e-cdda-4bbb-8603-e9c619b46580"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An open-world fantasy RPG where you play as Geralt of Rivia, a monster hunter. Complete quests, explore vast landscapes, and make choices that shape the world around you. The game features one of gaming's most compelling stories.", "https://via.placeholder.com/300x400?text=Witcher+3", "The Witcher 3: Wild Hunt" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("66bc4087-06f8-418e-9c4a-8908bc6b7771"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A roguelike action game where you escape from Hades, the Greek underworld. Repeatedly attempt runs through procedurally generated dungeons, upgrading your abilities and uncovering rich mythology.", "https://via.placeholder.com/300x400?text=Hades", "Hades" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("96c36ea6-284b-464b-8ece-fae17e8b8cb7"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A challenging 2D metroidvania game set in a haunted kingdom of insects. Explore interconnected caverns, defeat powerful bosses, and uncover the dark secrets of Hallownest.", "https://via.placeholder.com/300x400?text=Hollow+Knight", "Hollow Knight" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ab827d8a-311d-46cd-ad74-3742e07bfc5e"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Bethesda's ambitious space RPG featuring procedurally generated planets and a massive universe to explore. Create your character, build your spaceship, and embark on quests across hundreds of worlds.", "https://via.placeholder.com/300x400?text=Starfield", "Starfield" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("adaaad5d-49ee-4f9d-9de8-3d21f5333e6b"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A faithful remake of the classic psychological horror masterpiece. Players navigate the fog-shrouded town of Silent Hill as James Sunderland, uncovering dark secrets and confronting manifestations of guilt and despair.", "https://via.placeholder.com/300x400?text=Silent+Hill+2", "Silent Hill 2 Remake" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b85a91b1-3d37-46fc-ba9a-db2593cd2616"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "The conclusion to the Dark Souls trilogy, featuring challenging combat, intricate level design, and dark fantasy storytelling. Explore interconnected worlds and defeat powerful enemies.", "https://via.placeholder.com/300x400?text=Dark+Souls+III", "Dark Souls III" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("baaa2834-e824-4afb-8fd7-5911ad70390f"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "An open-world masterpiece combining challenging combat with expansive exploration. Traverse the Lands Between to restore the Elden Ring, facing formidable bosses and discovering hidden dungeons.", "https://via.placeholder.com/300x400?text=Elden+Ring", "Elden Ring" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bb09f7dd-442f-4e5e-b12c-e13a68fffa2f"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A massive CRPG set in the Dungeons & Dragons universe. Create your character, make impactful choices, and enjoy an incredibly deep story with multiple endings.", "https://via.placeholder.com/300x400?text=Baldurs+Gate+3", "Baldur's Gate 3" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bbbc7561-9fac-4795-9606-3f94c28ea92f"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A survival horror game where you play as Ethan Winters searching for his daughter in a mysterious village. Encounter terrifying creatures, solve puzzles, and experience intense action.", "https://via.placeholder.com/300x400?text=Resident+Evil", "Resident Evil Village" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ecbda2ec-594d-419d-8b55-9ecb4358ef41"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A modern reimagining of the classic JRPG. Play as Cloud and his allies in the sprawling metropolis of Midgar. Experience real-time combat and an expanded story.", "https://via.placeholder.com/300x400?text=Final+Fantasy+VII", "Final Fantasy VII Remake" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ed221215-c642-4077-b111-b770619b49c2"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "A puzzle-platformer featuring innovative portal mechanics. Use your portal gun to manipulate space, solve complex puzzles, and navigate through hilarious tests.", "https://via.placeholder.com/300x400?text=Portal+2", "Portal 2" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ed8b79ef-f6a2-40bd-87a4-1ff887f72b4d"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "The epic conclusion to the Norse saga. Kratos and Atreus face the apocalypse as they prepare for the final battle. Experience stunning visuals, intense combat, and an emotional narrative.", "https://via.placeholder.com/300x400?text=God+of+War", "God of War Ragnarök" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("faaa0756-7312-400c-9ba5-a581e4c90faf"),
                columns: new[] { "Description", "ImageUrl", "Title" },
                values: new object[] { "Hunt massive monsters in a sprawling open world. Master various weapons, craft armor, and work with other hunters to take down legendary creatures.", "https://via.placeholder.com/300x400?text=Monster+Hunter", "Monster Hunter: World" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"),
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://via.placeholder.com/300x300?text=Supergiant+Games", "Supergiant Games" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("1a94a706-493a-4ec2-b3e1-3cd802128f59"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Japanese video game publisher known for publishing epic JRPGs and iconic franchises with memorable characters and engaging storytelling.", "https://via.placeholder.com/300x300?text=Square+Enix", "Square Enix" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("3c77575e-bad2-4408-901b-9ec193a7cfba"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "American video game publisher and division of Sony, famous for publishing exclusive PlayStation titles and supporting innovative game development.", "https://via.placeholder.com/300x300?text=Sony+Interactive", "Sony Interactive Entertainment" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("556474fc-388b-48a7-9bd2-c45528b09bb1"),
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://via.placeholder.com/300x300?text=Kepler+Interactive", "Kepler Interactive" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Japanese video game publisher specializing in action-packed games and collaborations with renowned developers to create premium gaming experiences.", "https://via.placeholder.com/300x300?text=Bandai+Namco", "Bandai Namco Entertainment" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("a0529584-8eb3-4880-8674-c4e5cc67b487"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Japanese video game publisher recognized for creating atmospheric and psychologically intense horror games that push the boundaries of interactive storytelling.", "https://via.placeholder.com/300x300?text=Konami", "Konami" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("b037a40e-b701-4bdc-a5c0-96b0fdd92619"),
                columns: new[] { "ImageUrl", "Name" },
                values: new object[] { "https://via.placeholder.com/300x300?text=Team+Cherry", "Team Cherry" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("bad774ec-fc82-479d-800c-97c5bd602884"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "American video game publisher owned by Microsoft, famous for publishing massive open-world RPGs and supporting innovative game development.", "https://via.placeholder.com/300x300?text=Bethesda", "Bethesda Softworks" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Japanese video game publisher known for creating legendary survival horror franchises and action games with exceptional design and storytelling.", "https://via.placeholder.com/300x300?text=Capcom", "Capcom" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e2b91da3-3f00-4eb4-9785-643acbcf4c55"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Belgian video game publisher known for publishing deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.", "https://via.placeholder.com/300x300?text=Larian+Studios", "Larian Studios" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e89f7368-94f7-4f55-8e0d-a03333659abb"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "American video game publisher and digital distribution platform known for innovative, genre-defining games and digital distribution excellence.", "https://via.placeholder.com/300x300?text=Valve", "Valve" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("ed820b54-96c2-4fbf-a533-09b193c08028"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "European video game publisher known for publishing action-packed games and supporting diverse game genres from independent to AAA studios.", "https://via.placeholder.com/300x300?text=Focus+Entertainment", "Focus Entertainment" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"),
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "Polish video game publisher and distributor known for supporting independent studios and publishing award-winning narrative-driven games.", "https://via.placeholder.com/300x300?text=CD+Projekt", "CD Projekt" });
        }
    }
}
