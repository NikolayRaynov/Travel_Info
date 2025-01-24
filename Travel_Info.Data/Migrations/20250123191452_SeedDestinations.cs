using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Travel_Info.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDestinations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "CategoryId", "Description", "FavoritePlaceId", "Name", "PlaceToVisistId" },
                values: new object[,]
                {
                    { 1, 1, "Парк Росенец (известен още като \"Отманли\") се намира в землището на село Атия, около 15 км южно от Бургас        и 19 километра северно от Созопол.За любителите на слънцето и морските бани съвсем близо е един от най-           дивите и слабонаселени плажове в близост до Бургас, който ще превърне почивката ви в истинско удоволствие         сред прекрасната природа, сгушена между тайнствената Странджа и красивото Черно море.", 1, "Парк                  Росенец",1 },
                    { 2, 2, "\"Дяволското гърло\", макар и да звучи зловещо, е една от най-интересните и необикновени пещери в България.        Тя е част от уникалния феномен Триградско ждрело, който се намира се в Рило-Родопската област. Пещерата ще        ви предложи неповторимата гледка на най-високия подземен водопад на Балканите (42м). Тя ще ви пренесе в           едно приключение, разкривайки част от тайните си галерии.Според легендата в Дяволското гърло Орфей губи           завинаги любимата Евридика и тя потъва в подземното царство на Хадес. Страховитото име произлиза от               скалната форма на входа й, която наподобява дяволск глава.", 2, "Дявoлcĸoтo гъpлo", 2 },
                    { 3, 3, "Екопътека Струилица-Калето-Лъката се намира в Родопите, по поречието на река Девинска, което е със статут          на защитена местност от 2002г. Край реката са изградени места за отдих, подходящи за пикник и почивка на          прохлада през лятото. Обособени са места за риболовен туризъм, а по маршрута са поставени информационни           табелки за растителния и животинския свят в района.", 1, "Екопътека Струилица", 2 },
                    { 4, 4, "Екопътека Струилица-Калето-Лъката се намира в Родопите, по поречието на река Девинска, което е със статут          на защитена местност от 2002г. Край реката са изградени места за отдих, подходящи за пикник и почивка на          прохлада през лятото. Обособени са места за риболовен туризъм, а по маршрута са поставени информационни           табелки за растителния и животинския свят в района.", 2, "Царевец", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 4);

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
    }
}
