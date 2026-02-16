using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultImageForDevelopers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a52b8abd-4452-472f-b77b-992940382629", "AQAAAAIAAYagAAAAECEev6BWKLMYQaNcZnxZT6POcCI2kk8R7I93EpYzN2DkxVAiuh1pg1ltkqm8fF+1hA==", "3e2a942c-fe13-4c8c-aa1c-7753714dbf01" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e303f8ff-8c3d-4ec7-bdc9-bc5efa05325f"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a0c1b05-ac2f-43ba-8eee-a12e7df38f24", "AQAAAAIAAYagAAAAEJXstDNYyN5/xyLjHu/NlqJtTjboQSZXhYrePMblQE2VM1Teys77eZHm0zSSCKF7mg==", "d1170ce6-a483-4bac-bbd8-5260b044b906" });

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("0b4120ce-a6f9-4b1d-b8d8-de31e4173f7e"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Phoenix+Rising");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("166ff532-b1c1-40ad-b6ef-85a73d049d1e"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Titan+Forge");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("7010a2f8-1545-48d8-bb52-86ea0995c45e"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Prism+Voyage");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("c0d88616-83ce-416a-a034-11264b776cc1"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Pixel+Hollow");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("d7197d8b-d7f4-49ba-b24c-87b2152c6440"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Iron+Tempest");

            migrationBuilder.UpdateData(
                table: "Developers",
                keyColumn: "Id",
                keyValue: new Guid("f75ee60f-4615-4bca-8a21-be507c6d3a49"),
                column: "ImageUrl",
                value: "https://via.placeholder.com/300x300?text=Nightshade+Studios");
        }
    }
}
