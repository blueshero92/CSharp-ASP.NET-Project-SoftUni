using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImageUrlAddedForGamePublisherAndDeveloperEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Publishers",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                comment: "URL of the publisher logo image.");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Games",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                comment: "URL of the videogame image.");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Developers",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                comment: "URL of the developer logo image.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Publishers");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Developers");
        }
    }
}
