using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwarePanelreference2toSimulatorEventsDataEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                "SimulatorEvents");

            migrationBuilder.AlterColumn<int>(
                "HardwarePanelId",
                "SimulatorEvents",
                "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                "SimulatorEvents",
                "HardwarePanelId",
                "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                "SimulatorEvents");

            migrationBuilder.AlterColumn<int>(
                "HardwarePanelId",
                "SimulatorEvents",
                "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                "SimulatorEvents",
                "HardwarePanelId",
                "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}