using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultImageForGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d34b619f-1b58-4523-af5c-fa96c66ac534", "AQAAAAIAAYagAAAAEG9HZPp5FTB/Cqyw0gIbk1yapERckChftWPm5K35IW0RiWugOIR3zhEYPGbq/IvI4g==", "d4d281b0-6ded-4d3a-a69f-770d1e2ef178" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "328fdc1f-d267-4c9a-a3bb-50a9de737c5b", "AQAAAAIAAYagAAAAEKkxf4LTrtNo2T5yXUYgdliZRauPT960zWk7GblBGvPz1AQwsoDMTMtHCswYC6FgDw==", "04f1d89c-9917-4958-9c26-f486cb3d5796" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x400?text=Stellarborne+Legionnaires+II");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("38c117e0-2416-4d69-a39a-2b4141c218b2"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x400?text=Wanderers+Shattered+Isles");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x400?text=Oath+of+Vermillion+Ronin");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("96c36ea6-284b-464b-8ece-fae17e8b8cb7"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x400?text=Gearwright+Caverns");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("adaaad5d-49ee-4f9d-9de8-3d21f5333e6b"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x400?text=Whispers+of+Hollowmere");

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bbbc7561-9fac-4795-9606-3f94c28ea92f"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x400?text=Ravencrest+Estate");
        }
    }
}
