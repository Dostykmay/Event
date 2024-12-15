using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GiftNotation.Migrations
{
    /// <inheritdoc />
    public partial class addRelpTypeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RelpTypes",
                columns: new[] { "RelpTypeId", "RelpTypeName" },
                values: new object[,]
                {
                    { 1, "Друг" },
                    { 2, "Родственник" },
                    { 3, "Коллега" },
                    { 4, "Знакомый" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RelpTypes",
                keyColumn: "RelpTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RelpTypes",
                keyColumn: "RelpTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RelpTypes",
                keyColumn: "RelpTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "RelpTypes",
                keyColumn: "RelpTypeId",
                keyValue: 4);
        }
    }
}
