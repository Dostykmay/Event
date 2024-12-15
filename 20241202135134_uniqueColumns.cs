using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GiftNotation.Migrations
{
    /// <inheritdoc />
    public partial class uniqueColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Statuses_StatusName",
                table: "Statuses",
                column: "StatusName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RelpTypes_RelpTypeName",
                table: "RelpTypes",
                column: "RelpTypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventTypes_EventTypeName",
                table: "EventTypes",
                column: "EventTypeName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Statuses_StatusName",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_RelpTypes_RelpTypeName",
                table: "RelpTypes");

            migrationBuilder.DropIndex(
                name: "IX_EventTypes_EventTypeName",
                table: "EventTypes");
        }
    }
}
