using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeEveryDestinationHasAUserWhoAddedIt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d62199f-5a50-4fc6-9eae-72c28e8615be", "AQAAAAIAAYagAAAAEMnzGblwuIRayqWoThKmdywiDGxYTh+Q9yE3jOGll8fwxYfwRptDy8QQpcypBjxx3w==", "f8622803-9b62-4920-a1cb-2916ae148b96" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17e98724-7ced-4f7e-a050-c8935826257d", "AQAAAAIAAYagAAAAECbfWob1mlBXFmmdubtGr+zdEGdOpnS5PuWPlZyeb2QsPQE2rXFok6EJgAVxKcL2Aw==", "760a6477-5b63-4a20-a85e-b1cbb3a3b28d" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 22, 19, 27, 172, DateTimeKind.Local).AddTicks(3617));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 22, 19, 27, 172, DateTimeKind.Local).AddTicks(3689));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 22, 19, 27, 172, DateTimeKind.Local).AddTicks(3693));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 22, 19, 27, 172, DateTimeKind.Local).AddTicks(3703));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                column: "UserId",
                value: null);

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
        }
    }
}
