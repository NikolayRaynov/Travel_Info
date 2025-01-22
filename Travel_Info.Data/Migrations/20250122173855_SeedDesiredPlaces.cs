using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDesiredPlaces : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ac8f186-ad0f-4b25-ab70-0c8c0f7471ca", "AQAAAAIAAYagAAAAEFB4RxlT+wa31MXFwpoAkBsL8k1c6jjADmxYLr/P5M2NdI26ghYla/4bv3UG9kgQyg==", "118e961f-dc4c-4d92-b11b-d00980e61414" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "264d7b54-a3b8-4b0f-acde-1f3a19ebf062", "AQAAAAIAAYagAAAAEIBk8aGxwK7sy58rgQubvcTJ0224DVXBTJEYmqBNatUmhJPebXAlRIj2IPfMBQv6ng==", "e290883e-11dc-42e8-b35a-1c28e1c01d55" });

            migrationBuilder.InsertData(
                table: "PlacesToVisit",
                columns: new[] { "Id", "UserId" },
                values: new object[,]
                {
                    { 1, "dea12856-c198-4129-b3f3-b893d8395082" },
                    { 2, "8b3d2e65-4498-4d45-9127-2fde83fef2a4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlacesToVisit",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlacesToVisit",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "406f386a-f084-48eb-a1ef-57322420f0bc", "AQAAAAIAAYagAAAAEEEneNb3kHzV1MsPcu7Ejn0c0YiRdiluhE2KHi7SKHM3gOBfmHl5wL4m7yPZobv9GA==", "d05aef8c-86be-473d-914c-bf2f794c7fa1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "206fd339-ad7e-4316-8225-4c10888ed816", "AQAAAAIAAYagAAAAEPaTxGHyg5VYryZQwwo+pE+VBp5pEfKiXdWgDix5WzxZTtff0sOopgYtxxzYEUnyGw==", "23a8897c-7078-4fd2-b005-1ac4d777bc3a" });
        }
    }
}
