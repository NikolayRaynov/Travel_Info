using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddConstForComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f2c215c-c242-4527-82c7-3fe8644c627d", "AQAAAAIAAYagAAAAECB4PqMivw6Cmk0eZwyeAAaX//8G9aBe0EoTTwlF1EYTNXHiLYeYJdy2JkhszMUu9Q==", "d9fbcaab-0ace-49c1-9896-e705c6ca8fd4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9341e4f7-c1e3-463f-89f8-2eaa664581b3", "AQAAAAIAAYagAAAAEDrJFxOmNfLS0INIYqPHoqK8TcjnR0zUqgSuVQqje92VYPGaOBSFbocyDnUY4GEBKA==", "5d5ce856-5d8b-462f-a035-19b989b7b764" });
        }
    }
}
