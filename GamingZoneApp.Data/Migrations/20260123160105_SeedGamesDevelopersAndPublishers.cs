using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedGamesDevelopersAndPublishers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Games",
                type: "DECIMAL(3,1)",
                nullable: false,
                comment: "Videogame's average rating. Default value used, because users will rate the game",
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(3,1)",
                oldComment: "Videogame's average rating.");

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("07dbe539-557e-4692-85e6-c875b1262a71"), "American video game developer and digital distribution company known for creating innovative, genre-defining games with exceptional design and storytelling.", "https://via.placeholder.com/300x300?text=Valve", "Valve" },
                    { new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"), "Japanese video game developer and publisher known for creating legendary franchises and survival horror masterpieces with intense action gameplay.", "https://via.placeholder.com/300x300?text=Capcom", "Capcom" },
                    { new Guid("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"), "American video game developer owned by Microsoft, famous for creating massive open-world RPGs with deep worlds and endless exploration opportunities.", "https://via.placeholder.com/300x300?text=Bethesda", "Bethesda Game Studios" },
                    { new Guid("166ff532-b1c1-40ad-b6ef-85a73d049d1e"), "American video game developer known for creating fast-paced action games with intense gameplay and faithful adaptations of popular franchises.", "https://via.placeholder.com/300x300?text=Saber+Interactive", "Saber Interactive" },
                    { new Guid("4af65147-9ea5-4e30-a16d-de4750debe73"), "American video game developer owned by Sony Interactive Entertainment, famous for creating epic narrative-driven action games with stunning visuals.", "https://via.placeholder.com/300x300?text=Santa+Monica+Studio", "Santa Monica Studio" },
                    { new Guid("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"), "Belgian video game developer known for creating deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.", "https://via.placeholder.com/300x300?text=Larian+Studios", "Larian Studios" },
                    { new Guid("7010a2f8-1545-48d8-bb52-86ea0995c45e"), "French video game developer founded by veteran developers, known for creating innovative action RPGs with unique artistic vision and creative gameplay.", "https://via.placeholder.com/300x300?text=Sandfall+Interactive", "Sandfall Interactive" },
                    { new Guid("7596ad8e-5eaa-44da-a716-6ab508873b52"), "Independent video game studio renowned for creating visually stunning games with exceptional soundtracks and innovative gameplay mechanics.", "https://via.placeholder.com/300x300?text=Supergiant+Games", "Supergiant Games" },
                    { new Guid("ae06f7d4-7f50-411d-86af-c97ac324972e"), "American video game developer known for creating open-world action games with compelling narratives, stunning visual artistry, and engaging gameplay mechanics.", "https://via.placeholder.com/300x300?text=Sucker+Punch", "Sucker Punch Productions" },
                    { new Guid("ba8230ef-c6b3-4be8-9408-43d57b2049c3"), "Polish video game studio renowned for creating story-driven, visually stunning games with deep narratives and meaningful player choice.", "https://via.placeholder.com/300x300?text=CD+Projekt+Red", "CD Projekt Red" },
                    { new Guid("c0d88616-83ce-416a-a034-11264b776cc1"), "Independent Australian video game developer known for creating meticulously crafted indie games with exceptional art direction and challenging gameplay.", "https://via.placeholder.com/300x300?text=Team+Cherry", "Team Cherry" },
                    { new Guid("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"), "Japanese video game developer and publisher known for creating epic JRPGs and iconic franchises with memorable characters and engaging storylines.", "https://via.placeholder.com/300x300?text=Square+Enix", "Square Enix" },
                    { new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"), "Renowned Japanese video game developer celebrated for crafting challenging and immersive gameplay experiences with intricate level design and compelling boss battles.", "https://via.placeholder.com/300x300?text=FromSoftware", "FromSoftware" },
                    { new Guid("f75ee60f-4615-4bca-8a21-be507c6d3a49"), "Polish video game developer known for creating atmospheric psychological horror experiences with cutting-edge graphics and immersive storytelling.", "https://via.placeholder.com/300x300?text=Bloober+Team", "Bloober Team" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"), "Independent video game publisher renowned for creating visually stunning games with exceptional soundtracks and innovative gameplay mechanics.", "https://via.placeholder.com/300x300?text=Supergiant+Games", "Supergiant Games" },
                    { new Guid("1a94a706-493a-4ec2-b3e1-3cd802128f59"), "Japanese video game publisher known for publishing epic JRPGs and iconic franchises with memorable characters and engaging storytelling.", "https://via.placeholder.com/300x300?text=Square+Enix", "Square Enix" },
                    { new Guid("3c77575e-bad2-4408-901b-9ec193a7cfba"), "American video game publisher and division of Sony, famous for publishing exclusive PlayStation titles and supporting innovative game development.", "https://via.placeholder.com/300x300?text=Sony+Interactive", "Sony Interactive Entertainment" },
                    { new Guid("556474fc-388b-48a7-9bd2-c45528b09bb1"), "Independent video game publisher known for supporting innovative action RPGs and artistic games from talented developers with unique creative vision.", "https://via.placeholder.com/300x300?text=Kepler+Interactive", "Kepler Interactive" },
                    { new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"), "Japanese video game publisher specializing in action-packed games and collaborations with renowned developers to create premium gaming experiences.", "https://via.placeholder.com/300x300?text=Bandai+Namco", "Bandai Namco Entertainment" },
                    { new Guid("a0529584-8eb3-4880-8674-c4e5cc67b487"), "Japanese video game publisher recognized for creating atmospheric and psychologically intense horror games that push the boundaries of interactive storytelling.", "https://via.placeholder.com/300x300?text=Konami", "Konami" },
                    { new Guid("b037a40e-b701-4bdc-a5c0-96b0fdd92619"), "Independent publisher known for releasing meticulously crafted indie games with exceptional artistic vision and challenging gameplay.", "https://via.placeholder.com/300x300?text=Team+Cherry", "Team Cherry" },
                    { new Guid("bad774ec-fc82-479d-800c-97c5bd602884"), "American video game publisher owned by Microsoft, famous for publishing massive open-world RPGs and supporting innovative game development.", "https://via.placeholder.com/300x300?text=Bethesda", "Bethesda Softworks" },
                    { new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"), "Japanese video game publisher known for creating legendary survival horror franchises and action games with exceptional design and storytelling.", "https://via.placeholder.com/300x300?text=Capcom", "Capcom" },
                    { new Guid("e2b91da3-3f00-4eb4-9785-643acbcf4c55"), "Belgian video game publisher known for publishing deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.", "https://via.placeholder.com/300x300?text=Larian+Studios", "Larian Studios" },
                    { new Guid("e89f7368-94f7-4f55-8e0d-a03333659abb"), "American video game publisher and digital distribution platform known for innovative, genre-defining games and digital distribution excellence.", "https://via.placeholder.com/300x300?text=Valve", "Valve" },
                    { new Guid("ed820b54-96c2-4fbf-a533-09b193c08028"), "European video game publisher known for publishing action-packed games and supporting diverse game genres from independent to AAA studios.", "https://via.placeholder.com/300x300?text=Focus+Entertainment", "Focus Entertainment" },
                    { new Guid("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"), "Polish video game publisher and distributor known for supporting independent studios and publishing award-winning narrative-driven games.", "https://via.placeholder.com/300x300?text=CD+Projekt", "CD Projekt" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "DeveloperId", "Genre", "ImageUrl", "IsDeleted", "PublisherId", "Rating", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("24c4a7ef-7552-4931-ba8b-c5dbe536b323"), "An intense third-person shooter set in the grim Warhammer 40K universe. Play as a Space Marine and battle hordes of enemies using devastating weapons and melee combat. Experience fast-paced action and epic battles.", new Guid("166ff532-b1c1-40ad-b6ef-85a73d049d1e"), 7, "https://via.placeholder.com/300x400?text=Space+Marine+2", false, new Guid("ed820b54-96c2-4fbf-a533-09b193c08028"), 0.0m, new DateTime(2024, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Warhammer 40,000: Space Marine 2" },
                    { new Guid("2ab1107e-a9a0-4bfc-b366-034ebb4a73a1"), "The latest entry in the legendary fighting game franchise. Master diverse characters, execute precise combos, and compete online or offline.", new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"), 5, "https://via.placeholder.com/300x400?text=Street+Fighter+6", false, new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"), 0.0m, new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Street Fighter 6" },
                    { new Guid("31af3372-60e4-4fa2-a2a6-35032f396601"), "An open-world action-adventure set in 13th-century Japan. Play as Jin Sakai, a samurai warrior defending his homeland from invading Mongol forces. Experience stunning visuals, intense sword combat, and a compelling tale of honor and sacrifice.", new Guid("ae06f7d4-7f50-411d-86af-c97ac324972e"), 0, "https://via.placeholder.com/300x400?text=Ghost+of+Tsushima", false, new Guid("3c77575e-bad2-4408-901b-9ec193a7cfba"), 0.0m, new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ghost of Tsushima" },
                    { new Guid("38c117e0-2416-4d69-a39a-2b4141c218b2"), "A vibrant action RPG set in a beautiful watercolor-inspired world. Embark on an expedition to defeat cosmic corruption and discover the secrets of the land. Features real-time tactical combat and rich exploration.", new Guid("7010a2f8-1545-48d8-bb52-86ea0995c45e"), 8, "https://via.placeholder.com/300x400?text=Clair+Obscur", false, new Guid("556474fc-388b-48a7-9bd2-c45528b09bb1"), 0.0m, new DateTime(2025, 4, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clair Obscur: Expedition 33" },
                    { new Guid("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"), "An ambitious dystopian RPG set in the megacity of Night City. Play as V, a customizable mercenary navigating a world filled with corporate intrigue, high-tech augmentations, and morally complex choices.", new Guid("ba8230ef-c6b3-4be8-9408-43d57b2049c3"), 8, "https://via.placeholder.com/300x400?text=Cyberpunk+2077", false, new Guid("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"), 0.0m, new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cyberpunk 2077" },
                    { new Guid("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"), "A challenging action-adventure game set in Sengoku-era Japan. Play as a shinobi warrior seeking revenge, utilizing parkour, sword combat, and skill-based gameplay. Defeat legendary bosses in this FromSoftware title.", new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"), 4, "https://via.placeholder.com/300x400?text=Sekiro", false, new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"), 0.0m, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sekiro: Shadows Die Twice" },
                    { new Guid("55562e8e-cdda-4bbb-8603-e9c619b46580"), "An open-world fantasy RPG where you play as Geralt of Rivia, a monster hunter. Complete quests, explore vast landscapes, and make choices that shape the world around you. The game features one of gaming's most compelling stories.", new Guid("ba8230ef-c6b3-4be8-9408-43d57b2049c3"), 8, "https://via.placeholder.com/300x400?text=Witcher+3", false, new Guid("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"), 0.0m, new DateTime(2015, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3: Wild Hunt" },
                    { new Guid("66bc4087-06f8-418e-9c4a-8908bc6b7771"), "A roguelike action game where you escape from Hades, the Greek underworld. Repeatedly attempt runs through procedurally generated dungeons, upgrading your abilities and uncovering rich mythology.", new Guid("7596ad8e-5eaa-44da-a716-6ab508873b52"), 15, "https://via.placeholder.com/300x400?text=Hades", false, new Guid("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"), 0.0m, new DateTime(2020, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hades" },
                    { new Guid("96c36ea6-284b-464b-8ece-fae17e8b8cb7"), "A challenging 2D metroidvania game set in a haunted kingdom of insects. Explore interconnected caverns, defeat powerful bosses, and uncover the dark secrets of Hallownest.", new Guid("c0d88616-83ce-416a-a034-11264b776cc1"), 21, "https://via.placeholder.com/300x400?text=Hollow+Knight", false, new Guid("b037a40e-b701-4bdc-a5c0-96b0fdd92619"), 0.0m, new DateTime(2017, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hollow Knight" },
                    { new Guid("ab827d8a-311d-46cd-ad74-3742e07bfc5e"), "Bethesda's ambitious space RPG featuring procedurally generated planets and a massive universe to explore. Create your character, build your spaceship, and embark on quests across hundreds of worlds.", new Guid("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"), 2, "https://via.placeholder.com/300x400?text=Starfield", false, new Guid("bad774ec-fc82-479d-800c-97c5bd602884"), 0.0m, new DateTime(2023, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Starfield" },
                    { new Guid("adaaad5d-49ee-4f9d-9de8-3d21f5333e6b"), "A faithful remake of the classic psychological horror masterpiece. Players navigate the fog-shrouded town of Silent Hill as James Sunderland, uncovering dark secrets and confronting manifestations of guilt and despair.", new Guid("f75ee60f-4615-4bca-8a21-be507c6d3a49"), 9, "https://via.placeholder.com/300x400?text=Silent+Hill+2", false, new Guid("a0529584-8eb3-4880-8674-c4e5cc67b487"), 0.0m, new DateTime(2024, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Silent Hill 2 Remake" },
                    { new Guid("b85a91b1-3d37-46fc-ba9a-db2593cd2616"), "The conclusion to the Dark Souls trilogy, featuring challenging combat, intricate level design, and dark fantasy storytelling. Explore interconnected worlds and defeat powerful enemies.", new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"), 4, "https://via.placeholder.com/300x400?text=Dark+Souls+III", false, new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"), 0.0m, new DateTime(2016, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dark Souls III" },
                    { new Guid("baaa2834-e824-4afb-8fd7-5911ad70390f"), "An open-world masterpiece combining challenging combat with expansive exploration. Traverse the Lands Between to restore the Elden Ring, facing formidable bosses and discovering hidden dungeons.", new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"), 4, "https://via.placeholder.com/300x400?text=Elden+Ring", false, new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"), 0.0m, new DateTime(2022, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elden Ring" },
                    { new Guid("bb09f7dd-442f-4e5e-b12c-e13a68fffa2f"), "A massive CRPG set in the Dungeons & Dragons universe. Create your character, make impactful choices, and enjoy an incredibly deep story with multiple endings.", new Guid("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"), 20, "https://via.placeholder.com/300x400?text=Baldurs+Gate+3", false, new Guid("e2b91da3-3f00-4eb4-9785-643acbcf4c55"), 0.0m, new DateTime(2023, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Baldur's Gate 3" },
                    { new Guid("bbbc7561-9fac-4795-9606-3f94c28ea92f"), "A survival horror game where you play as Ethan Winters searching for his daughter in a mysterious village. Encounter terrifying creatures, solve puzzles, and experience intense action.", new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"), 9, "https://via.placeholder.com/300x400?text=Resident+Evil", false, new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"), 0.0m, new DateTime(2021, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Resident Evil Village" },
                    { new Guid("ecbda2ec-594d-419d-8b55-9ecb4358ef41"), "A modern reimagining of the classic JRPG. Play as Cloud and his allies in the sprawling metropolis of Midgar. Experience real-time combat and an expanded story.", new Guid("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"), 16, "https://via.placeholder.com/300x400?text=Final+Fantasy+VII", false, new Guid("1a94a706-493a-4ec2-b3e1-3cd802128f59"), 0.0m, new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final Fantasy VII Remake" },
                    { new Guid("ed221215-c642-4077-b111-b770619b49c2"), "A puzzle-platformer featuring innovative portal mechanics. Use your portal gun to manipulate space, solve complex puzzles, and navigate through hilarious tests.", new Guid("07dbe539-557e-4692-85e6-c875b1262a71"), 11, "https://via.placeholder.com/300x400?text=Portal+2", false, new Guid("e89f7368-94f7-4f55-8e0d-a03333659abb"), 0.0m, new DateTime(2011, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Portal 2" },
                    { new Guid("ed8b79ef-f6a2-40bd-87a4-1ff887f72b4d"), "The epic conclusion to the Norse saga. Kratos and Atreus face the apocalypse as they prepare for the final battle. Experience stunning visuals, intense combat, and an emotional narrative.", new Guid("4af65147-9ea5-4e30-a16d-de4750debe73"), 0, "https://via.placeholder.com/300x400?text=God+of+War", false, new Guid("3c77575e-bad2-4408-901b-9ec193a7cfba"), 0.0m, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "God of War Ragnarök" },
                    { new Guid("faaa0756-7312-400c-9ba5-a581e4c90faf"), "Hunt massive monsters in a sprawling open world. Master various weapons, craft armor, and work with other hunters to take down legendary creatures.", new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"), 8, "https://via.placeholder.com/300x400?text=Monster+Hunter", false, new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"), 0.0m, new DateTime(2018, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monster Hunter: World" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("24c4a7ef-7552-4931-ba8b-c5dbe536b323"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2ab1107e-a9a0-4bfc-b366-034ebb4a73a1"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("31af3372-60e4-4fa2-a2a6-35032f396601"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("38c117e0-2416-4d69-a39a-2b4141c218b2"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("55562e8e-cdda-4bbb-8603-e9c619b46580"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("66bc4087-06f8-418e-9c4a-8908bc6b7771"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("96c36ea6-284b-464b-8ece-fae17e8b8cb7"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ab827d8a-311d-46cd-ad74-3742e07bfc5e"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("adaaad5d-49ee-4f9d-9de8-3d21f5333e6b"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b85a91b1-3d37-46fc-ba9a-db2593cd2616"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("baaa2834-e824-4afb-8fd7-5911ad70390f"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bb09f7dd-442f-4e5e-b12c-e13a68fffa2f"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bbbc7561-9fac-4795-9606-3f94c28ea92f"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ecbda2ec-594d-419d-8b55-9ecb4358ef41"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ed221215-c642-4077-b111-b770619b49c2"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ed8b79ef-f6a2-40bd-87a4-1ff887f72b4d"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("faaa0756-7312-400c-9ba5-a581e4c90faf"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("07dbe539-557e-4692-85e6-c875b1262a71"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("166ff532-b1c1-40ad-b6ef-85a73d049d1e"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("4af65147-9ea5-4e30-a16d-de4750debe73"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("7010a2f8-1545-48d8-bb52-86ea0995c45e"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("7596ad8e-5eaa-44da-a716-6ab508873b52"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("ae06f7d4-7f50-411d-86af-c97ac324972e"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("ba8230ef-c6b3-4be8-9408-43d57b2049c3"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("c0d88616-83ce-416a-a034-11264b776cc1"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("f75ee60f-4615-4bca-8a21-be507c6d3a49"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("1a94a706-493a-4ec2-b3e1-3cd802128f59"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("3c77575e-bad2-4408-901b-9ec193a7cfba"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("556474fc-388b-48a7-9bd2-c45528b09bb1"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("a0529584-8eb3-4880-8674-c4e5cc67b487"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("b037a40e-b701-4bdc-a5c0-96b0fdd92619"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("bad774ec-fc82-479d-800c-97c5bd602884"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e2b91da3-3f00-4eb4-9785-643acbcf4c55"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e89f7368-94f7-4f55-8e0d-a03333659abb"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("ed820b54-96c2-4fbf-a533-09b193c08028"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Games",
                type: "DECIMAL(3,1)",
                nullable: false,
                comment: "Videogame's average rating.",
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(3,1)",
                oldComment: "Videogame's average rating. Default value used, because users will rate the game");
        }
    }
}
