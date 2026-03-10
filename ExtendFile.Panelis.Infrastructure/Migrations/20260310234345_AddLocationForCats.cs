using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtendFile.Panelis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocationForCats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Location",
                table: "Cats",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Cats");
        }
    }
}
