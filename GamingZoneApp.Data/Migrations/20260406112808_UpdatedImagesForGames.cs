using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedImagesForGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                column: "ImageUrl",
                value: "/images/game/galacticCrusadersII.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("38c117e0-2416-4d69-a39a-2b4141c218b2"),
                column: "ImageUrl",
                value: "/images/game/wanderers.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"),
                column: "ImageUrl",
                value: "/images/game/oathVermilionRonin.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("96c36ea6-284b-464b-8ece-fae17e8b8cb7"),
                column: "ImageUrl",
                value: "/images/game/gearwrightCaverns.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("adaaad5d-49ee-4f9d-9de8-3d21f5333e6b"),
                column: "ImageUrl",
                value: "/images/game/bloodlightSanatorium.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bbbc7561-9fac-4795-9606-3f94c28ea92f"),
                column: "ImageUrl",
                value: "/images/game/ravencrest.png");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_UserId",
                table: "Games");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"), 0, "0fb5c0e3-4bec-40d4-b595-c198557ff4dc", "admin@example.com", true, false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEBDE8G0EpwvQ7QgLI7Z66Ud+Cwx5AfeH73VQg5ntahYG/1Xw+Fl2j/nAXxlHN4y5JQ==", null, false, "2b3e0426-4cf4-4043-8b54-6990a22f8060", false, "admin" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                column: "ImageUrl",
                value: "/images/game/gameDefault.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("38c117e0-2416-4d69-a39a-2b4141c218b2"),
                column: "ImageUrl",
                value: "/images/game/gameDefault.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"),
                column: "ImageUrl",
                value: "/images/game/gameDefault.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("96c36ea6-284b-464b-8ece-fae17e8b8cb7"),
                column: "ImageUrl",
                value: "/images/game/gameDefault.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("adaaad5d-49ee-4f9d-9de8-3d21f5333e6b"),
                column: "ImageUrl",
                value: "/images/game/gameDefault.png");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bbbc7561-9fac-4795-9606-3f94c28ea92f"),
                column: "ImageUrl",
                value: "/images/game/gameDefault.png");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_UserId",
                table: "Games",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
