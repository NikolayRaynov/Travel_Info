using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCyrillicCategoryNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "NameBg",
                table: "Categories",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "",
                comment: "Name of the category in Cyrillic.");

            migrationBuilder.AddColumn<string>(
                name: "NameEn",
                table: "Categories",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "",
                comment: "Name of the category in English.");

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
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "NameBg", "NameEn" },
                values: new object[] { "Плаж", "Beach" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "NameBg", "NameEn" },
                values: new object[] { "Планина", "Mount" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "NameBg", "NameEn" },
                values: new object[] { "Разходка", "Stroll" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "NameBg", "NameEn" },
                values: new object[] { "Исторически", "Historical" });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameBg",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "NameEn",
                table: "Categories");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b405a6b-1d62-4c86-a64b-064e9fa2c02f", "AQAAAAIAAYagAAAAEKqEXBV0g50csqghU+GbxvFb/7uKgp3kLnh6RJgXnbe9WHm0kk/UIDh1GWYIn7qEmw==", "4a4485d5-80f6-4b44-9150-e27c2fbc6e09" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d2ad94a-b9b0-4805-bed5-75008ba799eb", "AQAAAAIAAYagAAAAEF32DgK0/v5KI0Wn7nSo5EwIPURRkzbiuYBdes9ykZ0a1vpfh97bsXS4jTs070L5Cw==", "9ad5fa0c-5763-4d84-8029-202745e36e90" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Beach");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Mount");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Stroll");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Historical");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 57, 45, 685, DateTimeKind.Local).AddTicks(368));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 57, 45, 685, DateTimeKind.Local).AddTicks(496));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 57, 45, 685, DateTimeKind.Local).AddTicks(501));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 57, 45, 685, DateTimeKind.Local).AddTicks(510));
        }
    }
}
