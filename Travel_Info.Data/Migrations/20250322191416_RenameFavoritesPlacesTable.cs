using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameFavoritesPlacesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_FavoritesPlaces_FavoritePlaceId",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritesPlaces_AspNetUsers_UserId",
                table: "FavoritesPlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritesPlaces",
                table: "FavoritesPlaces");

            migrationBuilder.RenameTable(
                name: "FavoritesPlaces",
                newName: "FavoritePlaces");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritesPlaces_UserId",
                table: "FavoritePlaces",
                newName: "IX_FavoritePlaces_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritePlaces",
                table: "FavoritePlaces",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e4d46493-7649-4ff1-b9e4-ead1720147c7", "AQAAAAIAAYagAAAAENWn2dpqfO2V7fx56skI8ppayL9+w3FQkYgEvRO8FBRYgwNV3Xwv20Q8Mb78oWx3Mg==", "6619642d-8d10-46ba-aa02-a1793b159529" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "085b6d29-b0dd-49ad-92d3-4dec8b98e229", "AQAAAAIAAYagAAAAEP8PfBktJYo7OcHKkHzYfm4E5/o8x4Jy7MIWt8v02K+A+PU9AoQ8IBj+6WcFJfyvkQ==", "ecb1e8fd-ab7b-4e07-b8b8-2fd026cfb566" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 14, 14, 855, DateTimeKind.Local).AddTicks(1660));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 14, 14, 855, DateTimeKind.Local).AddTicks(1743));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 14, 14, 855, DateTimeKind.Local).AddTicks(1753));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 14, 14, 855, DateTimeKind.Local).AddTicks(1758));

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_FavoritePlaces_FavoritePlaceId",
                table: "Destinations",
                column: "FavoritePlaceId",
                principalTable: "FavoritePlaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritePlaces_AspNetUsers_UserId",
                table: "FavoritePlaces",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_FavoritePlaces_FavoritePlaceId",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_FavoritePlaces_AspNetUsers_UserId",
                table: "FavoritePlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoritePlaces",
                table: "FavoritePlaces");

            migrationBuilder.RenameTable(
                name: "FavoritePlaces",
                newName: "FavoritesPlaces");

            migrationBuilder.RenameIndex(
                name: "IX_FavoritePlaces_UserId",
                table: "FavoritesPlaces",
                newName: "IX_FavoritesPlaces_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoritesPlaces",
                table: "FavoritesPlaces",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14f08dc5-a420-4a92-8af6-31cc3256155f", "AQAAAAIAAYagAAAAEB7mjLcSzWOc3onxpOq4R19V990GqsZsuP6Ui2J9N1eIMYpTlGGm5dJYWd8+FcsvWQ==", "bffda8b4-496a-42fa-b169-d4c9f51f5f50" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fef9b98b-f4c5-4a5a-be50-8ccf72e0c44a", "AQAAAAIAAYagAAAAEH8STor96qjcDVVbvPvR4ur6SsoaDjOIp50MYth/CbuPn2aZB9M1uE6sAMg9RQYCLA==", "99968ecf-0cfb-43f4-8d6f-2561902ab097" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 11, 8, 97, DateTimeKind.Local).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 11, 8, 97, DateTimeKind.Local).AddTicks(302));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 11, 8, 97, DateTimeKind.Local).AddTicks(307));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 11, 8, 97, DateTimeKind.Local).AddTicks(314));

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_FavoritesPlaces_FavoritePlaceId",
                table: "Destinations",
                column: "FavoritePlaceId",
                principalTable: "FavoritesPlaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoritesPlaces_AspNetUsers_UserId",
                table: "FavoritesPlaces",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
