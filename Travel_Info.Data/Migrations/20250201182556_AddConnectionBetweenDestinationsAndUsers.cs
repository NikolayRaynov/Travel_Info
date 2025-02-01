using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConnectionBetweenDestinationsAndUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Destinations",
                type: "nvarchar(450)",
                nullable: true,
                comment: "Identifier of the user who created the destination.");

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

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_UserId",
                table: "Destinations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_AspNetUsers_UserId",
                table: "Destinations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_AspNetUsers_UserId",
                table: "Destinations");

            migrationBuilder.DropIndex(
                name: "IX_Destinations_UserId",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Destinations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef5fd5b5-f836-4ff3-ae4f-f6d8e608745b", "AQAAAAIAAYagAAAAEHvNOxqd+5j9CP1yV7MITqyPtUj6MbXrs8V9UZAe1Dc+JZiJPuIqTATyFAR1h++ciw==", "d46a40b2-8d7e-4fb8-a98b-d6273b630cb6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0c8044a-44d2-435d-9e74-e1158fa67a3a", "AQAAAAIAAYagAAAAEKwZ9DUbmz9yRWH6C57u88Rq/Fce3FykgM8iZiFQR+EyotQKYiMifLURBdfE+bcwdQ==", "b78ec8f9-1407-46eb-9912-712f94241332" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 20, 6, 34, 560, DateTimeKind.Local).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 20, 6, 34, 560, DateTimeKind.Local).AddTicks(7442));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 20, 6, 34, 560, DateTimeKind.Local).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 20, 6, 34, 560, DateTimeKind.Local).AddTicks(7474));
        }
    }
}
