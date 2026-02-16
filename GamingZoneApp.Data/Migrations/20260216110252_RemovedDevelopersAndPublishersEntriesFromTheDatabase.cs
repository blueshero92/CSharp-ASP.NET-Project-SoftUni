using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDevelopersAndPublishersEntriesFromTheDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("07dbe539-557e-4692-85e6-c875b1262a71"));

            migrationBuilder.DeleteData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"));

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
                keyValue: new Guid("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"));

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
                keyValue: new Guid("bad774ec-fc82-479d-800c-97c5bd602884"));

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
                keyValue: new Guid("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a0c1b05-ac2f-43ba-8eee-a12e7df38f24", "AQAAAAIAAYagAAAAEJXstDNYyN5/xyLjHu/NlqJtTjboQSZXhYrePMblQE2VM1Teys77eZHm0zSSCKF7mg==", "d1170ce6-a483-4bac-bbd8-5260b044b906" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2bafd9da-109c-42ec-85df-84ae68f3bd8b", "AQAAAAIAAYagAAAAEAW3QK+g6W5mE3raD6OVzNDepMSylukmujWvLryaAtbqNwbG69VxQWjKmh0PzgpA0g==", "efc02b54-b3ff-4fb9-8179-113a53289501" });

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("07dbe539-557e-4692-85e6-c875b1262a71"), "Innovative video game developer known for creating genre-defining games with exceptional design, storytelling, and groundbreaking mechanics.", "https://via.placeholder.com/300x300?text=Quantum+Rift", "Quantum Rift Software" },
                    { new Guid("0dd6b5e3-9d72-4a1a-89b0-a8651c6bebf4"), "Leading video game developer famous for creating massive open-world RPGs with deep worlds and endless exploration opportunities.", "https://via.placeholder.com/300x300?text=Starbound+Studios", "Starbound Studios" },
                    { new Guid("4af65147-9ea5-4e30-a16d-de4750debe73"), "Premier video game developer famous for creating epic narrative-driven action games with stunning visuals and cinematic presentation.", "https://via.placeholder.com/300x300?text=Olympus+Digital", "Olympus Digital" },
                    { new Guid("52753bfd-d052-4ac1-a648-c9ae2fda3ae4"), "European video game developer known for creating deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.", "https://via.placeholder.com/300x300?text=Arcane+Depths", "Arcane Depths Interactive" },
                    { new Guid("7596ad8e-5eaa-44da-a716-6ab508873b52"), "Independent video game studio renowned for creating visually stunning games with exceptional soundtracks and innovative gameplay mechanics.", "https://via.placeholder.com/300x300?text=Ember+Flame", "Ember Flame Games" },
                    { new Guid("ae06f7d4-7f50-411d-86af-c97ac324972e"), "Acclaimed video game developer known for creating open-world action games with compelling narratives, stunning visual artistry, and engaging gameplay.", "https://via.placeholder.com/300x300?text=Shadow+Blade", "Shadow Blade Productions" },
                    { new Guid("ba8230ef-c6b3-4be8-9408-43d57b2049c3"), "Award-winning studio renowned for creating story-driven, visually stunning games with deep narratives and meaningful player choice.", "https://via.placeholder.com/300x300?text=Crimson+Forge", "Crimson Forge Entertainment" },
                    { new Guid("c81c009f-66c6-4c32-9c14-f3c9b68e20a3"), "Major video game developer and publisher known for creating epic JRPGs and iconic franchises with memorable characters and engaging storylines.", "https://via.placeholder.com/300x300?text=Crystal+Dawn", "Crystal Dawn Entertainment" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("0039fd7b-fdc2-4218-b942-e24ec3f3cf1f"), "Independent video game publisher renowned for creating visually stunning games with exceptional soundtracks and innovative gameplay mechanics.", "https://via.placeholder.com/300x300?text=Ember+Flame+Pub", "Ember Flame Publishing" },
                    { new Guid("1a94a706-493a-4ec2-b3e1-3cd802128f59"), "Major video game publisher known for publishing epic JRPGs and iconic franchises with memorable characters and engaging storytelling.", "https://via.placeholder.com/300x300?text=Crystal+Dawn+Pub", "Crystal Dawn Publishing" },
                    { new Guid("3c77575e-bad2-4408-901b-9ec193a7cfba"), "Major video game publisher famous for publishing exclusive titles and supporting innovative game development across multiple platforms.", "https://via.placeholder.com/300x300?text=Nexus+Global", "Nexus Global Entertainment" },
                    { new Guid("bad774ec-fc82-479d-800c-97c5bd602884"), "Leading video game publisher famous for publishing massive open-world RPGs and supporting innovative game development.", "https://via.placeholder.com/300x300?text=Starbound+Pub", "Starbound Publishing" },
                    { new Guid("e2b91da3-3f00-4eb4-9785-643acbcf4c55"), "European video game publisher known for publishing deep, story-rich RPGs with complex mechanics, multiple endings, and exceptional player agency.", "https://via.placeholder.com/300x300?text=Arcane+Depths+Pub", "Arcane Depths Publishing" },
                    { new Guid("e89f7368-94f7-4f55-8e0d-a03333659abb"), "Innovative video game publisher and digital distribution platform known for genre-defining games and digital distribution excellence.", "https://via.placeholder.com/300x300?text=Quantum+Rift+Pub", "Quantum Rift Publishing" },
                    { new Guid("f128d30f-d5df-4ea2-9638-6a3f2aef82dd"), "European video game publisher and distributor known for supporting independent studios and publishing award-winning narrative-driven games.", "https://via.placeholder.com/300x300?text=Crimson+Forge+Pub", "Crimson Forge Publishing" }
                });
        }
    }
}
