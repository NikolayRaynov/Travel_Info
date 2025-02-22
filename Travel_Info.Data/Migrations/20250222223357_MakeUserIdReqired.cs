using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserIdReqired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Destinations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                comment: "Identifier of the user who created the destination.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true,
                oldComment: "Identifier of the user who created the destination.");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f3bd343-30a7-4bd3-ad5c-a0edb04d9c4d", "AQAAAAIAAYagAAAAEJUL3pmnvhiWbKAW8IEvSiit97qH9Hzpo1jVUXhLWmXbiAb008rP1FojtPK+7x+9cg==", "266b900c-ef71-43ee-93ee-72f8e6b9a998" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9e165c7-8d4b-4eea-81ab-7c04d5ab3ece", "AQAAAAIAAYagAAAAEOCGV/RPH1hWtRhtAAlvoa1bf7lk+Yq+fUx1Y/XCdLpCGMk79yX+28vpm/7JbuTfKg==", "19ce0b07-94f7-4df3-8df7-bd7082a8b215" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 23, 0, 33, 54, 264, DateTimeKind.Local).AddTicks(2168));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 23, 0, 33, 54, 264, DateTimeKind.Local).AddTicks(2265));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 23, 0, 33, 54, 264, DateTimeKind.Local).AddTicks(2281));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 23, 0, 33, 54, 264, DateTimeKind.Local).AddTicks(2285));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Destinations",
                type: "nvarchar(450)",
                nullable: true,
                comment: "Identifier of the user who created the destination.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Identifier of the user who created the destination.");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3d62199f-5a50-4fc6-9eae-72c28e8615be", "AQAAAAIAAYagAAAAEMnzGblwuIRayqWoThKmdywiDGxYTh+Q9yE3jOGll8fwxYfwRptDy8QQpcypBjxx3w==", "f8622803-9b62-4920-a1cb-2916ae148b96" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17e98724-7ced-4f7e-a050-c8935826257d", "AQAAAAIAAYagAAAAECbfWob1mlBXFmmdubtGr+zdEGdOpnS5PuWPlZyeb2QsPQE2rXFok6EJgAVxKcL2Aw==", "760a6477-5b63-4a20-a85e-b1cbb3a3b28d" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 22, 19, 27, 172, DateTimeKind.Local).AddTicks(3617));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 22, 19, 27, 172, DateTimeKind.Local).AddTicks(3689));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 22, 19, 27, 172, DateTimeKind.Local).AddTicks(3693));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 22, 22, 19, 27, 172, DateTimeKind.Local).AddTicks(3703));
        }
    }
}
