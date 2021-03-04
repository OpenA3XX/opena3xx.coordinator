using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AircraftModels_Manufucturers_ManufacturerId",
                "AircraftModels");

            migrationBuilder.DropPrimaryKey(
                "PK_Manufucturers",
                "Manufucturers");

            migrationBuilder.RenameTable(
                "Manufucturers",
                newName: "Manufacturers");

            migrationBuilder.AddPrimaryKey(
                "PK_Manufacturers",
                "Manufacturers",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_AircraftModels_Manufacturers_ManufacturerId",
                "AircraftModels",
                "ManufacturerId",
                "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AircraftModels_Manufacturers_ManufacturerId",
                "AircraftModels");

            migrationBuilder.DropPrimaryKey(
                "PK_Manufacturers",
                "Manufacturers");

            migrationBuilder.RenameTable(
                "Manufacturers",
                newName: "Manufucturers");

            migrationBuilder.AddPrimaryKey(
                "PK_Manufucturers",
                "Manufucturers",
                "Id");

            migrationBuilder.AddForeignKey(
                "FK_AircraftModels_Manufucturers_ManufacturerId",
                "AircraftModels",
                "ManufacturerId",
                "Manufucturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}