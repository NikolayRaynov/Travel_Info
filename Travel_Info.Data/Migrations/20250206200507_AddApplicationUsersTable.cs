using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApplicationUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f58a6f3-4152-4929-a966-21d1ab2efd76", "AQAAAAIAAYagAAAAENyPnz7w1ftv744TatoHJOv12W6Sp143ic/WB79RhFjVbFuY16rYiIXAs2IgfP94kQ==", "a3b278ce-59f8-402e-b281-f4b0991dd190" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65d2094c-e360-4f69-82fe-d2d0ae614c15", "AQAAAAIAAYagAAAAEGwXJvB9SamnuF9jX5gBeNIzICivEhhB2SnkBH0qBahApVGUQcEa6dKKOlLhGlx7/g==", "c3545624-91cb-4a25-827d-5f56422f7d7d" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 1, 20, 25, 55, 250, DateTimeKind.Local).AddTicks(9583));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 1, 20, 25, 55, 250, DateTimeKind.Local).AddTicks(9669));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 1, 20, 25, 55, 250, DateTimeKind.Local).AddTicks(9684));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 1, 20, 25, 55, 250, DateTimeKind.Local).AddTicks(9689));
        }
    }
}
