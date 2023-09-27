using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SEPTA_App.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Carpenter");

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Chelten Avenue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Carpenter Station");

            migrationBuilder.UpdateData(
                table: "Stations",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Chelten Avenue station");
        }
    }
}
