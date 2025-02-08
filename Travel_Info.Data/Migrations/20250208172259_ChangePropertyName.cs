using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangePropertyName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_PlacesToVisit_PlaceToVisistId",
                table: "Destinations");

            migrationBuilder.RenameColumn(
                name: "PlaceToVisistId",
                table: "Destinations",
                newName: "PlaceToVisitId");

            migrationBuilder.RenameIndex(
                name: "IX_Destinations_PlaceToVisistId",
                table: "Destinations",
                newName: "IX_Destinations_PlaceToVisitId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6f24bb86-7963-4e12-bacc-a647aa332e8f", "AQAAAAIAAYagAAAAELH91RheoHouxQIUl6TqHmQcVLnGsJcke6E1XIrLLjgX4E6dnjQKoe9wkCP6dlfM2A==", "68aa9a38-fefa-47c1-8266-b8d4cc9b4c58" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f88452d1-27e5-4647-938b-6dbe14a8fe08", "AQAAAAIAAYagAAAAEOW/u0LBvUSjk7y4PqhqJZ56We/fFDc+baA92nKV5wItxTxm+2QhGSVh1KzyN+xnHw==", "df30eca6-9c8e-4e7f-869b-c246e73862d8" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 8, 19, 22, 56, 811, DateTimeKind.Local).AddTicks(8766));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 8, 19, 22, 56, 811, DateTimeKind.Local).AddTicks(8849));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 8, 19, 22, 56, 811, DateTimeKind.Local).AddTicks(8861));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 8, 19, 22, 56, 811, DateTimeKind.Local).AddTicks(8866));

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_PlacesToVisit_PlaceToVisitId",
                table: "Destinations",
                column: "PlaceToVisitId",
                principalTable: "PlacesToVisit",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_PlacesToVisit_PlaceToVisitId",
                table: "Destinations");

            migrationBuilder.RenameColumn(
                name: "PlaceToVisitId",
                table: "Destinations",
                newName: "PlaceToVisistId");

            migrationBuilder.RenameIndex(
                name: "IX_Destinations_PlaceToVisitId",
                table: "Destinations",
                newName: "IX_Destinations_PlaceToVisistId");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f63ae0c3-68e9-47b7-9b58-4fc23e1e307f", "AQAAAAIAAYagAAAAEAwMLasQSlX2TdloxxOHO7VtQ9mhOAIYq5O+vTNxo6Yo+Lx9y83ZMrZA/FsJ1Ype7g==", "109089d2-5784-4078-8156-b8e7334b4660" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de737e4f-56ff-4b71-af2a-296c1246a816", "AQAAAAIAAYagAAAAEDJvC4MlQI116cqfbeNetuXpcizQyIBs8igp9SPEm/fWDv2bxgbH6jIhJNseql1iNw==", "6907523f-29a9-40e4-8cce-7a9cb1dd3f9f" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 6, 22, 5, 6, 824, DateTimeKind.Local).AddTicks(3759));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 6, 22, 5, 6, 824, DateTimeKind.Local).AddTicks(3827));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 6, 22, 5, 6, 824, DateTimeKind.Local).AddTicks(3838));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 6, 22, 5, 6, 824, DateTimeKind.Local).AddTicks(3841));

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_PlacesToVisit_PlaceToVisistId",
                table: "Destinations",
                column: "PlaceToVisistId",
                principalTable: "PlacesToVisit",
                principalColumn: "Id");
        }
    }
}
