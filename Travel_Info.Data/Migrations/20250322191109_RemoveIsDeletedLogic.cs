using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveIsDeletedLogic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PlacesToVisit");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FavoritesPlaces");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Destinations");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14f08dc5-a420-4a92-8af6-31cc3256155f", "AQAAAAIAAYagAAAAEB7mjLcSzWOc3onxpOq4R19V990GqsZsuP6Ui2J9N1eIMYpTlGGm5dJYWd8+FcsvWQ==", "bffda8b4-496a-42fa-b169-d4c9f51f5f50" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fef9b98b-f4c5-4a5a-be50-8ccf72e0c44a", "AQAAAAIAAYagAAAAEH8STor96qjcDVVbvPvR4ur6SsoaDjOIp50MYth/CbuPn2aZB9M1uE6sAMg9RQYCLA==", "99968ecf-0cfb-43f4-8d6f-2561902ab097" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 11, 8, 97, DateTimeKind.Local).AddTicks(224));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 11, 8, 97, DateTimeKind.Local).AddTicks(302));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 11, 8, 97, DateTimeKind.Local).AddTicks(307));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 3, 22, 21, 11, 8, 97, DateTimeKind.Local).AddTicks(314));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicator for logical deletion of the review.");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PlacesToVisit",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicator for logical deletion of the favorite place.");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Images",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicator for logical deletion of the image.");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FavoritesPlaces",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicator for logical deletion of the favorite place.");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Destinations",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Indicator for logical deletion of the destination.");

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
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "FavoritesPlaces",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "FavoritesPlaces",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Images",
                keyColumn: "Id",
                keyValue: 4,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PlacesToVisit",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "PlacesToVisit",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsDeleted",
                value: false);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "IsDeleted" },
                values: new object[] { new DateTime(2025, 2, 23, 0, 33, 54, 264, DateTimeKind.Local).AddTicks(2168), false });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "IsDeleted" },
                values: new object[] { new DateTime(2025, 2, 23, 0, 33, 54, 264, DateTimeKind.Local).AddTicks(2265), false });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "IsDeleted" },
                values: new object[] { new DateTime(2025, 2, 23, 0, 33, 54, 264, DateTimeKind.Local).AddTicks(2281), false });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "IsDeleted" },
                values: new object[] { new DateTime(2025, 2, 23, 0, 33, 54, 264, DateTimeKind.Local).AddTicks(2285), false });
        }
    }
}
