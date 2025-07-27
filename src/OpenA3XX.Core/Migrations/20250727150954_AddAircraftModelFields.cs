using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    /// <inheritdoc />
    public partial class AddAircraftModelFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if the table exists before trying to drop it
            migrationBuilder.Sql("DROP TABLE IF EXISTS \"SystemConfiguration\"");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AircraftModels",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AircraftModels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AircraftModels",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "AircraftModels",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AircraftModels",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AircraftModels");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AircraftModels");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AircraftModels");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AircraftModels");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AircraftModels");

            migrationBuilder.CreateTable(
                name: "SystemConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfiguration", x => x.Id);
                });
        }
    }
}
