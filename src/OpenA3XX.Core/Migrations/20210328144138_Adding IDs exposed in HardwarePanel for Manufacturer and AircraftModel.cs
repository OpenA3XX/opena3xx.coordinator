using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingIDsexposedinHardwarePanelforManufacturerandAircraftModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftModels_Manufacturers_ManufacturerId",
                table: "AircraftModels");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwarePanels_AircraftModels_AircraftModelId",
                table: "HardwarePanels");

            migrationBuilder.AlterColumn<int>(
                name: "AircraftModelId",
                table: "HardwarePanels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "AircraftModels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftModels_Manufacturers_ManufacturerId",
                table: "AircraftModels",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwarePanels_AircraftModels_AircraftModelId",
                table: "HardwarePanels",
                column: "AircraftModelId",
                principalTable: "AircraftModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AircraftModels_Manufacturers_ManufacturerId",
                table: "AircraftModels");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwarePanels_AircraftModels_AircraftModelId",
                table: "HardwarePanels");

            migrationBuilder.AlterColumn<int>(
                name: "AircraftModelId",
                table: "HardwarePanels",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "ManufacturerId",
                table: "AircraftModels",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_AircraftModels_Manufacturers_ManufacturerId",
                table: "AircraftModels",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwarePanels_AircraftModels_AircraftModelId",
                table: "HardwarePanels",
                column: "AircraftModelId",
                principalTable: "AircraftModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
