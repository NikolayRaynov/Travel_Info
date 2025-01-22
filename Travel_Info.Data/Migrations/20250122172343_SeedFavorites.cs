using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "FavoritesPlaces",
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
                table: "FavoritesPlaces",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FavoritesPlaces",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "04798ae7-53d7-44ec-a5d0-068c6c28f340", "AQAAAAIAAYagAAAAEMoHI74TFqTxCpr8Rvp8yCLMy3dvTTQxuI3bM2D8rkDY2Bzz0cBVXOSWNxC6GT/bJg==", "25fae391-3639-41b2-a970-a68f53b0b341" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6b26f7a7-a876-4ae6-90f3-b8ca15c15816", "AQAAAAIAAYagAAAAELVmtsEjwQn5+7IJS7TyP/5DJ8pA5+nixMOX0RSbUUAMkNEr4vdtGUietLVWDxlV8Q==", "4433bc60-b7e6-49f6-a424-10dabe772ab7" });
        }
    }
}
