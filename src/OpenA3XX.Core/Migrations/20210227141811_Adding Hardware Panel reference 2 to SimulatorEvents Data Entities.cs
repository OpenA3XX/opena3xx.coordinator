using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwarePanelreference2toSimulatorEventsDataEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                table: "SimulatorEvents");

            migrationBuilder.AlterColumn<int>(
                name: "HardwarePanelId",
                table: "SimulatorEvents",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                table: "SimulatorEvents",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                table: "SimulatorEvents");

            migrationBuilder.AlterColumn<int>(
                name: "HardwarePanelId",
                table: "SimulatorEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                table: "SimulatorEvents",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
