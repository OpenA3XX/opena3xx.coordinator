using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftModels_Manufucturers_ManufucturerId",
                table: "AircraftModels");

            migrationBuilder.RenameColumn(
                name: "ManufucturerId",
                table: "AircraftModels",
                newName: "ManufacturerId");

            migrationBuilder.RenameIndex(
                name: "IX_AircraftModels_ManufucturerId",
                table: "AircraftModels",
                newName: "IX_AircraftModels_ManufacturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftModels_Manufucturers_ManufacturerId",
                table: "AircraftModels",
                column: "ManufacturerId",
                principalTable: "Manufucturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftModels_Manufucturers_ManufacturerId",
                table: "AircraftModels");

            migrationBuilder.RenameColumn(
                name: "ManufacturerId",
                table: "AircraftModels",
                newName: "ManufucturerId");

            migrationBuilder.RenameIndex(
                name: "IX_AircraftModels_ManufacturerId",
                table: "AircraftModels",
                newName: "IX_AircraftModels_ManufucturerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftModels_Manufucturers_ManufucturerId",
                table: "AircraftModels",
                column: "ManufucturerId",
                principalTable: "Manufucturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
