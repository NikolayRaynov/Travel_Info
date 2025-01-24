using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedImages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Description", "DestinationId", "Url" },
                values: new object[,]
                {
                    { 1, "Парк Росенец", 1, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpzqgqIh5O8gA3lalctwh0VDhqphHdYRf1ow&s" },
                    { 2, "Дявoлcĸoтo гъpлo", 2, "https://sunrisinglife.com/wp-content/uploads/2020/02/DSC00496.jpg" },
                    { 3, "Екопътека Струилица", 3, "https://static.pochivka.bg/sights.bgstay.com/images/01/1565/54c568cb2fbea.jpg" },
                    { 4, "Царевец", 4, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQNiTp27Uafvlivnn89hGTJIirhYgLbYHhZbw&s" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "350d6541-7205-4a94-89f1-63a463d9ed24", "AQAAAAIAAYagAAAAEKE/WnxGMiCjvi7J8R6N9CBpeIagvafNhvRsbGPczsEpMOmZMh20SsYylGjLmITVUw==", "45a9116c-e159-4683-82bf-324c48e0f80a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "18c20491-09c7-4f4e-8f78-14c9178d8f42", "AQAAAAIAAYagAAAAEBd+YFf/aEbFfhj1MajKBxjYn8SSJm50SQHQuja2/Er2FGlE+AEPFL/9YHNzuqB3ng==", "73e7d8ca-c920-450c-a8ec-31da88c45d5e" });
        }
    }
}
