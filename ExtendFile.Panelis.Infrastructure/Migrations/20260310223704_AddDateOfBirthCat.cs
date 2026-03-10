using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtendFile.Panelis.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDateOfBirthCat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Cats");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Cats",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Cats");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Cats",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
