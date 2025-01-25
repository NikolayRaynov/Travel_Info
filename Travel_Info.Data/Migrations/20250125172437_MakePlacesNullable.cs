using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakePlacesNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_FavoritesPlaces_FavoritePlaceId",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_PlacesToVisit_PlaceToVisistId",
                table: "Destinations");

            migrationBuilder.AlterColumn<int>(
                name: "PlaceToVisistId",
                table: "Destinations",
                type: "int",
                nullable: true,
                comment: "Identifier of the place to visit associated with the destination.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Destinations",
                type: "nvarchar(85)",
                maxLength: 85,
                nullable: false,
                comment: "Name of the destination.",
                oldClrType: typeof(string),
                oldType: "nvarchar(85)",
                oldMaxLength: 85);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Destinations",
                type: "bit",
                nullable: false,
                comment: "Indicator for logical deletion of the destination.",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "FavoritePlaceId",
                table: "Destinations",
                type: "int",
                nullable: true,
                comment: "Identifier of the favorite place associated with the destination.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Destinations",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: false,
                comment: "Description of the destination.",
                oldClrType: typeof(string),
                oldType: "nvarchar(800)",
                oldMaxLength: 800);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Destinations",
                type: "int",
                nullable: false,
                comment: "Identifier of the category to which the destination belongs.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba6e25fc-062c-4c0b-8c37-6770d7048a16", "AQAAAAIAAYagAAAAELBs9MyZa4/bbDr0C+aoZvIqLEBq1nG4s9G/2QOaIDK/sH4VYg14vKlBMsy/mKrs8g==", "666d172d-0fb9-45f3-a04f-b0c21c584f1d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0c7ca54-f18e-4f6f-a672-0715d1d59a23", "AQAAAAIAAYagAAAAEBtTx8TH2absM7/lhv8DrySRLlSVIRL7AOa+wouRE9MH6252ELec9dUFbxGjPGW0vg==", "68ea42ef-098c-4d71-a837-6e2d8ef9e0a6" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9354));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9525));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9530));

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_FavoritesPlaces_FavoritePlaceId",
                table: "Destinations",
                column: "FavoritePlaceId",
                principalTable: "FavoritesPlaces",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_PlacesToVisit_PlaceToVisistId",
                table: "Destinations",
                column: "PlaceToVisistId",
                principalTable: "PlacesToVisit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_FavoritesPlaces_FavoritePlaceId",
                table: "Destinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_PlacesToVisit_PlaceToVisistId",
                table: "Destinations");

            migrationBuilder.AlterColumn<int>(
                name: "PlaceToVisistId",
                table: "Destinations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Identifier of the place to visit associated with the destination.");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Destinations",
                type: "nvarchar(85)",
                maxLength: 85,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(85)",
                oldMaxLength: 85,
                oldComment: "Name of the destination.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Destinations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicator for logical deletion of the destination.");

            migrationBuilder.AlterColumn<int>(
                name: "FavoritePlaceId",
                table: "Destinations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "Identifier of the favorite place associated with the destination.");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Destinations",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(800)",
                oldMaxLength: 800,
                oldComment: "Description of the destination.");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Destinations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Identifier of the category to which the destination belongs.");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63c258d4-693d-40e5-b07a-d3d443ee4435", "AQAAAAIAAYagAAAAELj6W0egc6nZ1xpL/SCPYnzWPzfmSuQgQbfSvmb1Oq/Ox6IZ9o7de3UxJw+jBHqp9A==", "5b5432b3-db8c-4f84-b6c5-09f6ba8db5f4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8ffa54d0-221d-417e-a00d-4e7ee8f6203f", "AQAAAAIAAYagAAAAEMsXm8SQ6LUXspem/iINHUjJJB1klFqIt1ehZUqs90Sq/FCqJUyysP3NMvB6NIEQwQ==", "cb1e0b01-7486-45cc-82c8-fa9d3d3763b6" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 5, 13, 117, DateTimeKind.Local).AddTicks(4918));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 5, 13, 117, DateTimeKind.Local).AddTicks(5049));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 5, 13, 117, DateTimeKind.Local).AddTicks(5054));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 5, 13, 117, DateTimeKind.Local).AddTicks(5067));

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_FavoritesPlaces_FavoritePlaceId",
                table: "Destinations",
                column: "FavoritePlaceId",
                principalTable: "FavoritesPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_PlacesToVisit_PlaceToVisistId",
                table: "Destinations",
                column: "PlaceToVisistId",
                principalTable: "PlacesToVisit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
