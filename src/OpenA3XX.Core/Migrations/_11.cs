using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AircraftModels_Manufucturers_ManufucturerId",
                "AircraftModels");

            migrationBuilder.RenameColumn(
                "ManufucturerId",
                "AircraftModels",
                "ManufacturerId");

            migrationBuilder.RenameIndex(
                "IX_AircraftModels_ManufucturerId",
                table: "AircraftModels",
                newName: "IX_AircraftModels_ManufacturerId");

            migrationBuilder.AddForeignKey(
                "FK_AircraftModels_Manufucturers_ManufacturerId",
                "AircraftModels",
                "ManufacturerId",
                "Manufucturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_AircraftModels_Manufucturers_ManufacturerId",
                "AircraftModels");

            migrationBuilder.RenameColumn(
                "ManufacturerId",
                "AircraftModels",
                "ManufucturerId");

            migrationBuilder.RenameIndex(
                "IX_AircraftModels_ManufacturerId",
                table: "AircraftModels",
                newName: "IX_AircraftModels_ManufucturerId");

            migrationBuilder.AddForeignKey(
                "FK_AircraftModels_Manufucturers_ManufucturerId",
                "AircraftModels",
                "ManufucturerId",
                "Manufucturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}