using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c615427f-2f89-4e6c-9284-ec2dc9dd4726", "AQAAAAIAAYagAAAAEFFDnmS18eLFfSpSNCFmr3PB9eoX+cEbgnVEo/Yv+ucSSuR5t0XIt3PMz3C+LugVUw==", "9726b044-cde1-4726-ba03-51cdc5bcfe2c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "adc19172-91e3-43ca-9d87-1dde3f01e8d4", "AQAAAAIAAYagAAAAEDAJr+jf2Tg/DiO5WxGVK19nlSV7L7BL9SiOaGD3zlQaBFfWMRk7XLfaDn/JE3+VqA==", "5101caca-59c2-4470-b318-f2b0d5ed6aa4" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "CreatedAt", "DestinationId", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1, "Страхотно място!", new DateTime(2025, 1, 24, 21, 50, 8, 721, DateTimeKind.Local).AddTicks(3021), 1, 5, "dea12856-c198-4129-b3f3-b893d8395082" },
                    { 2, null, new DateTime(2025, 1, 24, 21, 50, 8, 721, DateTimeKind.Local).AddTicks(3502), 2, 5, "dea12856-c198-4129-b3f3-b893d8395082" },
                    { 3, "Страхотно място!", new DateTime(2025, 1, 24, 21, 50, 8, 721, DateTimeKind.Local).AddTicks(3517), 3, 5, "dea12856-c198-4129-b3f3-b893d8395082" },
                    { 4, null, new DateTime(2025, 1, 24, 21, 50, 8, 721, DateTimeKind.Local).AddTicks(3522), 4, 5, "dea12856-c198-4129-b3f3-b893d8395082" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "41dc3207-8f97-4f56-b7ff-4f9876f258de", "AQAAAAIAAYagAAAAEOYS5u3V84O3KRrsMkIvj6QXI84NLAod7hjvPoOGw4UvONAxRLaufAJMNhU9GS5o0A==", "396b0be5-4e65-4f37-927c-cb6d741a669c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fee8000d-a7b3-48d8-80bc-6f6dbee341fa", "AQAAAAIAAYagAAAAEKyPN7a7pBaf/mdaWbkMRap/2FtCz1afZcSx5gnDR004xaezHaZrxXk2O77UqIVIYA==", "4b2018cf-f65c-4728-90c3-d2276f574e4b" });
        }
    }
}
