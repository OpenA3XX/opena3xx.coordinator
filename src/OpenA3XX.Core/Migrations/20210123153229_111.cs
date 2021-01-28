using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftModels_Manufucturers_ManufacturerId",
                table: "AircraftModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manufucturers",
                table: "Manufucturers");

            migrationBuilder.RenameTable(
                name: "Manufucturers",
                newName: "Manufacturers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manufacturers",
                table: "Manufacturers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftModels_Manufacturers_ManufacturerId",
                table: "AircraftModels",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftModels_Manufacturers_ManufacturerId",
                table: "AircraftModels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Manufacturers",
                table: "Manufacturers");

            migrationBuilder.RenameTable(
                name: "Manufacturers",
                newName: "Manufucturers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Manufucturers",
                table: "Manufucturers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftModels_Manufucturers_ManufacturerId",
                table: "AircraftModels",
                column: "ManufacturerId",
                principalTable: "Manufucturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
