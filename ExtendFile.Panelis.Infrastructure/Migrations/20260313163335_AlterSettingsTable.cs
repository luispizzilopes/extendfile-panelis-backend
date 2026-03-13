using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtendFile.Panelis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterSettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DaysWithoutEatingForAlert",
                table: "Settings",
                type: "integer",
                nullable: false,
                defaultValue: 3);

            migrationBuilder.AddColumn<int>(
                name: "DaysWithoutEatingForWarning",
                table: "Settings",
                type: "integer",
                nullable: false,
                defaultValue: 5);

            migrationBuilder.AddColumn<int>(
                name: "DaysWithoutEating",
                table: "Cats",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DaysWithoutEatingForAlert",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DaysWithoutEatingForWarning",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DaysWithoutEating",
                table: "Cats");
        }
    }
}
