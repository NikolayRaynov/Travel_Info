using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentsToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Identifier of the user who wrote the review.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int",
                nullable: false,
                comment: "Rating given to the destination.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                comment: "Indicator for logical deletion of the review.",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Reviews",
                type: "int",
                nullable: false,
                comment: "Identifier of the destination for which the review is written.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                comment: "Date when the review was created.",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                comment: "Comment from the user about the destination.",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PlacesToVisit",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Identifier of the user who want to visit the place",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PlacesToVisit",
                type: "bit",
                nullable: false,
                comment: "Indicator for logical deletion of the favorite place.",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Images",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: false,
                comment: "URL of the image.",
                oldClrType: typeof(string),
                oldType: "nvarchar(2083)",
                oldMaxLength: 2083);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Images",
                type: "bit",
                nullable: false,
                comment: "Indicator for logical deletion of the image.",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Images",
                type: "int",
                nullable: false,
                comment: "Identifier of the destination to which the image belongs.",
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Images",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                comment: "Description of the image.",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FavoritesPlaces",
                type: "nvarchar(450)",
                nullable: false,
                comment: "Identifier of the user who marked the place as favorite.",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "FavoritesPlaces",
                type: "bit",
                nullable: false,
                comment: "Indicator for logical deletion of the favorite place.",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ef5fd5b5-f836-4ff3-ae4f-f6d8e608745b", "AQAAAAIAAYagAAAAEHvNOxqd+5j9CP1yV7MITqyPtUj6MbXrs8V9UZAe1Dc+JZiJPuIqTATyFAR1h++ciw==", "d46a40b2-8d7e-4fb8-a98b-d6273b630cb6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0c8044a-44d2-435d-9e74-e1158fa67a3a", "AQAAAAIAAYagAAAAEKwZ9DUbmz9yRWH6C57u88Rq/Fce3FykgM8iZiFQR+EyotQKYiMifLURBdfE+bcwdQ==", "b78ec8f9-1407-46eb-9912-712f94241332" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 20, 6, 34, 560, DateTimeKind.Local).AddTicks(7295));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 20, 6, 34, 560, DateTimeKind.Local).AddTicks(7442));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 20, 6, 34, 560, DateTimeKind.Local).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 20, 6, 34, 560, DateTimeKind.Local).AddTicks(7474));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Identifier of the user who wrote the review.");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Rating given to the destination.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Reviews",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicator for logical deletion of the review.");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Identifier of the destination for which the review is written.");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Reviews",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldComment: "Date when the review was created.");

            migrationBuilder.AlterColumn<string>(
                name: "Comment",
                table: "Reviews",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true,
                oldComment: "Comment from the user about the destination.");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "PlacesToVisit",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Identifier of the user who want to visit the place");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "PlacesToVisit",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicator for logical deletion of the favorite place.");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Images",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2083)",
                oldMaxLength: 2083,
                oldComment: "URL of the image.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Images",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicator for logical deletion of the image.");

            migrationBuilder.AlterColumn<int>(
                name: "DestinationId",
                table: "Images",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "Identifier of the destination to which the image belongs.");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Images",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldComment: "Description of the image.");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "FavoritesPlaces",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldComment: "Identifier of the user who marked the place as favorite.");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "FavoritesPlaces",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Indicator for logical deletion of the favorite place.");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba6e25fc-062c-4c0b-8c37-6770d7048a16", "AQAAAAIAAYagAAAAELBs9MyZa4/bbDr0C+aoZvIqLEBq1nG4s9G/2QOaIDK/sH4VYg14vKlBMsy/mKrs8g==", "666d172d-0fb9-45f3-a04f-b0c21c584f1d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0c7ca54-f18e-4f6f-a672-0715d1d59a23", "AQAAAAIAAYagAAAAEBtTx8TH2absM7/lhv8DrySRLlSVIRL7AOa+wouRE9MH6252ELec9dUFbxGjPGW0vg==", "68ea42ef-098c-4d71-a837-6e2d8ef9e0a6" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9354));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9509));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9525));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 25, 19, 24, 36, 405, DateTimeKind.Local).AddTicks(9530));
        }
    }
}
