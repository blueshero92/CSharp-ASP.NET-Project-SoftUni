using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrlsMadeNullableForAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Publishers",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                comment: "URL of the publisher logo image.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldComment: "URL of the publisher logo image.");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Games",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                comment: "URL of the videogame image.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldComment: "URL of the videogame image.");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Developers",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                comment: "URL of the developer logo image.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldComment: "URL of the developer logo image.");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fb5c0e3-4bec-40d4-b595-c198557ff4dc", "AQAAAAIAAYagAAAAEBDE8G0EpwvQ7QgLI7Z66Ud+Cwx5AfeH73VQg5ntahYG/1Xw+Fl2j/nAXxlHN4y5JQ==", "2b3e0426-4cf4-4043-8b54-6990a22f8060" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Publishers",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                comment: "URL of the publisher logo image.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true,
                oldComment: "URL of the publisher logo image.");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Games",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                comment: "URL of the videogame image.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true,
                oldComment: "URL of the videogame image.");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Developers",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                comment: "URL of the developer logo image.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true,
                oldComment: "URL of the developer logo image.");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d34b619f-1b58-4523-af5c-fa96c66ac534", "AQAAAAIAAYagAAAAEG9HZPp5FTB/Cqyw0gIbk1yapERckChftWPm5K35IW0RiWugOIR3zhEYPGbq/IvI4g==", "d4d281b0-6ded-4d3a-a69f-770d1e2ef178" });
        }
    }
}
