using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedGameEntriesFromTheDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                keyValue: new Guid("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"));

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
                keyValue: new Guid("ab827d8a-311d-46cd-ad74-3742e07bfc5e"));

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bafd9da-109c-42ec-85df-84ae68f3bd8b", "AQAAAAIAAYagAAAAEAW3QK+g6W5mE3raD6OVzNDepMSylukmujWvLryaAtbqNwbG69VxQWjKmh0PzgpA0g==", "efc02b54-b3ff-4fb9-8179-113a53289501" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a7baa931-fad9-4db1-845b-784c40674c8f", "AQAAAAIAAYagAAAAEOzyJ5Kezia4l2IlstE5GUVRTLemg+BBpOTWrKz2e2CxtL9BJWMc7IXGhztoPWPjug==", "1068daec-8d01-4a7e-91ae-6f49e2f6ebe8" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "DeveloperId", "Genre", "ImageUrl", "IsDeleted", "PublisherId", "Rating", "ReleaseDate", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("2ab1107e-a9a0-4bfc-b366-034ebb4a73a1"), "The World Martial Arts Summit returns with 32 fighters from across the globe. Master unique fighting disciplines, uncover each warrior's personal vendetta, and claim the legendary Iron Championship.", new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"), 5, "https://via.placeholder.com/300x400?text=Iron+Fist+Legends+VI", false, new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"), 0.0m, new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iron Legends VI", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("31af3372-60e4-4fa2-a2a6-35032f396601"), "When the Takeda clan falls, one warrior survives. Traverse war-scorched Yamashiro Province, master both blade and shadow, and decide whether vengeance or honor will define your path through feudal turmoil.", new Guid("ae06f7d4-7f50-411d-86af-c97ac324972e"), 0, "https://via.placeholder.com/300x400?text=Whisper+of+Daimyo", false, new Guid("3c77575e-bad2-4408-901b-9ec193a7cfba"), 0.0m, new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Whisper of the Daimyo", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"), "In the towering megastructure of Arcturus Prime, play as Cipher-7, a rogue synthetic seeking answers about their origins. Infiltrate corporate enclaves, hack neural networks, and question what it means to be alive.", new Guid("ba8230ef-c6b3-4be8-9408-43d57b2049c3"), 8, "https://via.placeholder.com/300x400?text=Synthetica+Fractured+Grid", false, new Guid("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"), 0.0m, new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Synthetica: Fractured Grid", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("55562e8e-cdda-4bbb-8603-e9c619b46580"), "Become Varen Duskwood, a wandering archivist thrust into a continental war over forbidden knowledge. Traverse five distinct kingdoms, forge pacts with ancient spirits, and decide which truths deserve to survive.", new Guid("ba8230ef-c6b3-4be8-9408-43d57b2049c3"), 8, "https://via.placeholder.com/300x400?text=Lorekeepers+of+Aethermoor", false, new Guid("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"), 0.0m, new DateTime(2015, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lorekeepers of Aethermoor", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("66bc4087-06f8-418e-9c4a-8908bc6b7771"), "Condemned to the Scorching Pits, your only escape lies upward. Battle through procedurally shifting infernal layers, bargain with imprisoned demigods for boons, and unravel why you were cast into eternal fire.", new Guid("7596ad8e-5eaa-44da-a716-6ab508873b52"), 15, "https://via.placeholder.com/300x400?text=Pyreclimb+Ascendancy", false, new Guid("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"), 0.0m, new DateTime(2020, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pyreclimb Ascendancy", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("ab827d8a-311d-46cd-ad74-3742e07bfc5e"), "Command the starship Perihelion through uncharted galaxies. Establish colonies, negotiate with alien civilizations, and uncover the mystery of the Void Echoes—signals from a universe that should not exist.", new Guid("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"), 2, "https://via.placeholder.com/300x400?text=Voidstrider+Odyssey", false, new Guid("bad774ec-fc82-479d-800c-97c5bd602884"), 0.0m, new DateTime(2023, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Voidstrider Odyssey", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("b85a91b1-3d37-46fc-ba9a-db2593cd2616"), "Descend into the smoldering ruins of the Embervault, where the First Flame's remnants twist reality. Face relentless horrors, uncover the Pyrekeepers' tragic fate, and choose whether to rekindle hope or embrace oblivion.", new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"), 4, "https://via.placeholder.com/300x400?text=Embervault+Requiem", false, new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"), 0.0m, new DateTime(2016, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Embervault Requiem", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("baaa2834-e824-4afb-8fd7-5911ad70390f"), "The Blighted Throne awaits a new sovereign. Cross treacherous mirelands, challenge colossal guardians born of dying gods, and piece together a shattered realm where every victory carves your legend into stone.", new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"), 4, "https://via.placeholder.com/300x400?text=Thornveil+Dominion", false, new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"), 0.0m, new DateTime(2022, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thornveil Dominion", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("bb09f7dd-442f-4e5e-b12c-e13a68fffa2f"), "The realm fractures as noble houses wage shadow wars. Assemble your Veilguard from rogues, mages, and exiled knights, navigate deadly court intrigue, and determine which faction claims the shattered crown.", new Guid("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"), 20, "https://via.placeholder.com/300x400?text=Veilguard+Accord", false, new Guid("e2b91da3-3f00-4eb4-9785-643acbcf4c55"), 0.0m, new DateTime(2023, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Veilguard Accord", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("ecbda2ec-594d-419d-8b55-9ecb4358ef41"), "Join Zephyr and the Tempest Vanguard in overthrowing the Ironhaven Directorate. Wield crystallized aether in spectacular combat, forge bonds with unlikely allies, and witness a revolution that will reshape the world.", new Guid("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"), 16, "https://via.placeholder.com/300x400?text=Aethershard+Chronicles", false, new Guid("1a94a706-493a-4ec2-b3e1-3cd802128f59"), 0.0m, new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aethershard Chronicles", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("ed221215-c642-4077-b111-b770619b49c2"), "Awaken in the Tesseract Facility with no memory and a device that folds space. Navigate impossible geometries, outsmart a sardonic AI overseer, and piece together the catastrophe that erased your identity.", new Guid("07dbe539-557e-4692-85e6-c875b1262a71"), 11, "https://via.placeholder.com/300x400?text=Paradox+Chamber", false, new Guid("e89f7368-94f7-4f55-8e0d-a03333659abb"), 0.0m, new DateTime(2011, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Paradox Chamber", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("ed8b79ef-f6a2-40bd-87a4-1ff887f72b4d"), "Ragnarok approaches as Halvard and his son Eirik defy fate itself. Wield thunder-forged weapons, traverse the Nine Branches, and challenge beings of primordial power in this climactic tale of sacrifice and legacy.", new Guid("4af65147-9ea5-4e30-a16d-de4750debe73"), 0, "https://via.placeholder.com/300x400?text=Fury+of+Stormborn", false, new Guid("3c77575e-bad2-4408-901b-9ec193a7cfba"), 0.0m, new DateTime(2022, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fury of the Stormborn", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") },
                    { new Guid("faaa0756-7312-400c-9ba5-a581e4c90faf"), "The wild frontier teems with titanic beasts. Track legendary creatures across volcanic peaks and frozen tundras, craft gear from their remains, and join hunting parties to bring down monsters of mythic proportions.", new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"), 8, "https://via.placeholder.com/300x400?text=Colossus+Hunt+Untamed", false, new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"), 0.0m, new DateTime(2018, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Colossus Hunt: Untamed", new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f") }
                });
        }
    }
}
