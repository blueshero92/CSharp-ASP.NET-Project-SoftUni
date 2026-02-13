using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamingZoneApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOneToManyRelationForGameAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Games",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("24c4a7ef-7552-4931-ba8b-c5dbe536b323"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("2ab1107e-a9a0-4bfc-b366-034ebb4a73a1"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("31af3372-60e4-4fa2-a2a6-35032f396601"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("38c117e0-2416-4d69-a39a-2b4141c218b2"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4ad3f8ca-874e-41b3-ba2d-bfe4eaa2b334"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4e7d3a02-5a8b-487c-ae11-8a408e6a87c1"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("55562e8e-cdda-4bbb-8603-e9c619b46580"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("66bc4087-06f8-418e-9c4a-8908bc6b7771"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("96c36ea6-284b-464b-8ece-fae17e8b8cb7"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ab827d8a-311d-46cd-ad74-3742e07bfc5e"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("adaaad5d-49ee-4f9d-9de8-3d21f5333e6b"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b85a91b1-3d37-46fc-ba9a-db2593cd2616"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("baaa2834-e824-4afb-8fd7-5911ad70390f"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bb09f7dd-442f-4e5e-b12c-e13a68fffa2f"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("bbbc7561-9fac-4795-9606-3f94c28ea92f"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ecbda2ec-594d-419d-8b55-9ecb4358ef41"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ed221215-c642-4077-b111-b770619b49c2"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("ed8b79ef-f6a2-40bd-87a4-1ff887f72b4d"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("faaa0756-7312-400c-9ba5-a581e4c90faf"),
                column: "UserId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserId",
                table: "Games",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_UserId",
                table: "Games",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_UserId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_UserId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Games");
        }
    }
}
