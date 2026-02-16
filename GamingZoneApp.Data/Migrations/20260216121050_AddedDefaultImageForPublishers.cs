using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultImageForPublishers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "328fdc1f-d267-4c9a-a3bb-50a9de737c5b", "AQAAAAIAAYagAAAAEKkxf4LTrtNo2T5yXUYgdliZRauPT960zWk7GblBGvPz1AQwsoDMTMtHCswYC6FgDw==", "04f1d89c-9917-4958-9c26-f486cb3d5796" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("556474fc-388b-48a7-9bd2-c45528b09bb1"),
                column: "ImageUrl",
                value: "/images/publisher.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                column: "ImageUrl",
                value: "/images/publisher.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("a0529584-8eb3-4880-8674-c4e5cc67b487"),
                column: "ImageUrl",
                value: "/images/publisher.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("b037a40e-b701-4bdc-a5c0-96b0fdd92619"),
                column: "ImageUrl",
                value: "/images/publisher.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"),
                column: "ImageUrl",
                value: "/images/publisher.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("ed820b54-96c2-4fbf-a533-09b193c08028"),
                column: "ImageUrl",
                value: "/images/publisher.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a52b8abd-4452-472f-b77b-992940382629", "AQAAAAIAAYagAAAAECEev6BWKLMYQaNcZnxZT6POcCI2kk8R7I93EpYzN2DkxVAiuh1pg1ltkqm8fF+1hA==", "3e2a942c-fe13-4c8c-aa1c-7753714dbf01" });

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("556474fc-388b-48a7-9bd2-c45528b09bb1"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Prism+Voyage+Pub");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Dragon+Crown");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("a0529584-8eb3-4880-8674-c4e5cc67b487"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Phantom+Gate");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("b037a40e-b701-4bdc-a5c0-96b0fdd92619"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Pixel+Hollow+Pub");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Phoenix+Rising+Pub");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("ed820b54-96c2-4fbf-a533-09b193c08028"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Titan+Forge+Pub");
        }
    }
}
