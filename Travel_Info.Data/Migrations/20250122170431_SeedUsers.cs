using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8b3d2e65-4498-4d45-9127-2fde83fef2a4", 0, "04798ae7-53d7-44ec-a5d0-068c6c28f340", "admin@mail.com", false, false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAIAAYagAAAAEMoHI74TFqTxCpr8Rvp8yCLMy3dvTTQxuI3bM2D8rkDY2Bzz0cBVXOSWNxC6GT/bJg==", null, false, "25fae391-3639-41b2-a970-a68f53b0b341", false, "admin@mail.com" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "6b26f7a7-a876-4ae6-90f3-b8ca15c15816", "user@mail.com", false, false, null, "USER@MAIL.COM", "USER@MAIL.COM", "AQAAAAIAAYagAAAAELVmtsEjwQn5+7IJS7TyP/5DJ8pA5+nixMOX0RSbUUAMkNEr4vdtGUietLVWDxlV8Q==", null, false, "4433bc60-b7e6-49f6-a424-10dabe772ab7", false, "user@mail.com" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");
        }
    }
}
