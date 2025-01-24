using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDescOfDestinationLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Destinations",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bc592b4-3d15-4901-85d8-1791f1d2e72d", "AQAAAAIAAYagAAAAEEPAPJOnbmzae9HBjhFxh/mL9+Cip4mz1JQx8xaC59BQDREH3PjgdpiNEHXdpiukzg==", "4c9096bc-957d-4b75-abf1-9b9bf169b543" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8326a69d-b07d-403b-822e-2806b9078b22", "AQAAAAIAAYagAAAAEIYU8+joDeV2IIrp/dMfpicJm3YDvT0MXuZoBqazmtvuV3LENxvgYoJA2fJjmoQ/kw==", "64015527-03fa-44c1-b4cf-c7953ea3730c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Destinations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(800)",
                oldMaxLength: 800);

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
        }
    }
}
