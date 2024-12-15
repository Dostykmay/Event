using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GiftNotation.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "EventTypeId", "EventTypeName" },
                values: new object[,]
                {
                    { 1, "День Рождения" },
                    { 2, "23 февраля" },
                    { 3, "Годовщина" },
                    { 4, "Новый год" },
                    { 5, "8 марта" },
                    { 6, "9 мая" },
                    { 7, "Рождество" },
                    { 8, "Свадьба" },
                    { 9, "Просто подарочек" },
                    { 10, "Важное событие" }
                });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "В процессе покупки" },
                    { 2, "Куплен" },
                    { 3, "Упакован" },
                    { 4, "Подарен" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "EventTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "StatusId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "StatusId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "StatusId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "StatusId",
                keyValue: 4);
        }
    }
}
