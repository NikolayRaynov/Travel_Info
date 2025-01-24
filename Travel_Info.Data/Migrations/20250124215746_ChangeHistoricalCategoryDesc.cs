using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeHistoricalCategoryDesc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1b405a6b-1d62-4c86-a64b-064e9fa2c02f", "AQAAAAIAAYagAAAAEKqEXBV0g50csqghU+GbxvFb/7uKgp3kLnh6RJgXnbe9WHm0kk/UIDh1GWYIn7qEmw==", "4a4485d5-80f6-4b44-9150-e27c2fbc6e09" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2d2ad94a-b9b0-4805-bed5-75008ba799eb", "AQAAAAIAAYagAAAAEF32DgK0/v5KI0Wn7nSo5EwIPURRkzbiuYBdes9ykZ0a1vpfh97bsXS4jTs070L5Cw==", "9ad5fa0c-5763-4d84-8029-202745e36e90" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Крепостта „Царевец“ е била главната българска крепост по времето на Втората българска държава (1185-1393 г.),           има три входа, запазени и до днес. Главният е разположен в най-западната част, „Асеновата“ порта (или Малката         порта) се намира в северозападния край, а на югоизток е Френкхисарската порта. Последната е охранявана от             бойна кула, известна като „Балдуинова кула“. Според легендата след историческата битка при Одрин през 1205 г.,        в която цар Калоян пленява латинския император Балдуин Фландърски, той е затворен в тази кула до края на              живота си.");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 57, 45, 685, DateTimeKind.Local).AddTicks(368));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 57, 45, 685, DateTimeKind.Local).AddTicks(496));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 57, 45, 685, DateTimeKind.Local).AddTicks(501));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 57, 45, 685, DateTimeKind.Local).AddTicks(510));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b3d2e65-4498-4d45-9127-2fde83fef2a4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae5d7375-483d-4355-87bf-4bb85f57b848", "AQAAAAIAAYagAAAAELJ9sWddWWXLG8SlyCSjn841Z4SpTVIrjuma2R8gfwA8Ezp9aJa3/ECs0j5vAHZwIg==", "a399c4d9-3e04-481a-b64e-11859c1da23a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1439edd9-cdf2-4b40-8180-d49e0c771d61", "AQAAAAIAAYagAAAAENBJe0vHmbKebZz2eVgmHiXMa5Zbeu23xJaeMl4FkbPIC+ec8aWYpZWlyT9C+lSa5w==", "fbc6f00f-74a3-4677-bd46-a86c7765ed2f" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Екопътека Струилица-Калето-Лъката се намира в Родопите, по поречието на                                                   река Девинска, което е със статут на защитена местност от 2002г. Край                                                     реката са изградени места за отдих, подходящи за пикник и почивка на                                                      прохлада през лятото. Обособени са места за риболовен туризъм, а по                                                       маршрута са поставени  информационни табелки за растителния и животинския                                                 свят в района.");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 37, 26, 894, DateTimeKind.Local).AddTicks(8153));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 37, 26, 894, DateTimeKind.Local).AddTicks(8267));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 37, 26, 894, DateTimeKind.Local).AddTicks(8271));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 24, 23, 37, 26, 894, DateTimeKind.Local).AddTicks(8279));
        }
    }
}
