using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCategoryNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae5d7375-483d-4355-87bf-4bb85f57b848", "AQAAAAIAAYagAAAAELJ9sWddWWXLG8SlyCSjn841Z4SpTVIrjuma2R8gfwA8Ezp9aJa3/ECs0j5vAHZwIg==", "a399c4d9-3e04-481a-b64e-11859c1da23a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1439edd9-cdf2-4b40-8180-d49e0c771d61", "AQAAAAIAAYagAAAAENBJe0vHmbKebZz2eVgmHiXMa5Zbeu23xJaeMl4FkbPIC+ec8aWYpZWlyT9C+lSa5w==", "fbc6f00f-74a3-4677-bd46-a86c7765ed2f" });

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
                value: new DateTime(2025, 1, 24, 23, 37, 26, 894, DateTimeKind.Local).AddTicks(8153));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 37, 26, 894, DateTimeKind.Local).AddTicks(8267));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 37, 26, 894, DateTimeKind.Local).AddTicks(8271));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 37, 26, 894, DateTimeKind.Local).AddTicks(8279));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d65906da-4254-4358-817c-b019e1f404ff", "AQAAAAIAAYagAAAAEJLUXeIpnEuJfEjTzAn+gY5NGEHZvkwi3hNPT9O8c4ixq9ty5sYHr2QZ6b4cVvUVOQ==", "7aff6b65-06d6-47ca-b94d-6c58187d414a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96b52b79-428d-4226-834d-ea25f0263ef9", "AQAAAAIAAYagAAAAEFdrvpYppMw4T8Z4G5GmfRFzupKxV+sLf3PVPDCkc9J39yy5munfEHTflUQv0UrrSw==", "00061bd8-9efd-4d18-8a87-3d454da7a602" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Море");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Планина");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Разходка");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "История");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 12, 12, 787, DateTimeKind.Local).AddTicks(9005));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 12, 12, 787, DateTimeKind.Local).AddTicks(9139));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 12, 12, 787, DateTimeKind.Local).AddTicks(9153));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 12, 12, 787, DateTimeKind.Local).AddTicks(9157));
        }
    }
}
