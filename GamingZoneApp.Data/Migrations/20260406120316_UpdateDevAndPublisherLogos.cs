using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDevAndPublisherLogos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                column: "ImageUrl",
                value: "/images/logos/phoenixRising.png");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                column: "ImageUrl",
                value: "/images/logos/titanForge.png");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                column: "ImageUrl",
                value: "/images/logos/prismVoyage.png");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("c0d88616-83ce-416a-a034-11264b776cc1"),
                column: "ImageUrl",
                value: "/images/logos/pixelHollow.png");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                column: "ImageUrl",
                value: "/images/logos/ironTempest.png");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("f75ee60f-4615-4bca-8a21-be507c6d3a49"),
                column: "ImageUrl",
                value: "/images/logos/nightshade.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("556474fc-388b-48a7-9bd2-c45528b09bb1"),
                column: "ImageUrl",
                value: "/images/logos/prismVoyage.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("8baa78a8-4139-409e-b8ab-78aac5b5fc48"),
                column: "ImageUrl",
                value: "/images/logos/dragonCrown.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("a0529584-8eb3-4880-8674-c4e5cc67b487"),
                column: "ImageUrl",
                value: "/images/logos/phantomGate.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("b037a40e-b701-4bdc-a5c0-96b0fdd92619"),
                column: "ImageUrl",
                value: "/images/logos/pixelHollow.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("e23228f4-77a4-448c-b816-ccb1826eed36"),
                column: "ImageUrl",
                value: "/images/logos/phoenixRising.png");

            migrationBuilder.UpdateData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("ed820b54-96c2-4fbf-a533-09b193c08028"),
                column: "ImageUrl",
                value: "/images/logos/titanForge.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                column: "ImageUrl",
                value: "/images/developer.png");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                column: "ImageUrl",
                value: "/images/developer.png");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                column: "ImageUrl",
                value: "/images/developer.png");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("c0d88616-83ce-416a-a034-11264b776cc1"),
                column: "ImageUrl",
                value: "/images/developer.png");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                column: "ImageUrl",
                value: "/images/developer.png");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("f75ee60f-4615-4bca-8a21-be507c6d3a49"),
                column: "ImageUrl",
                value: "/images/developer.png");

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
    }
}
