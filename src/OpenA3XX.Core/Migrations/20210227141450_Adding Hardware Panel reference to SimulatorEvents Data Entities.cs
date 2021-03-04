using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwarePanelreferencetoSimulatorEventsDataEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                "HardwareInputs");

            migrationBuilder.DropForeignKey(
                "FK_SimulatorEventLink_HardwareInputSelectors_HardwareOutputSelectorId",
                "SimulatorEventLink");

            migrationBuilder.AddColumn<int>(
                "HardwarePanelId",
                "SimulatorEvents",
                "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                "HardwarePanelId",
                "HardwareInputs",
                "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                "IX_SimulatorEvents_HardwarePanelId",
                "SimulatorEvents",
                "HardwarePanelId");

            migrationBuilder.AddForeignKey(
                "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                "HardwareInputs",
                "HardwarePanelId",
                "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_SimulatorEventLink_HardwareOutputSelectors_HardwareOutputSelectorId",
                "SimulatorEventLink",
                "HardwareOutputSelectorId",
                "HardwareOutputSelectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                "SimulatorEvents",
                "HardwarePanelId",
                "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                "HardwareInputs");

            migrationBuilder.DropForeignKey(
                "FK_SimulatorEventLink_HardwareOutputSelectors_HardwareOutputSelectorId",
                "SimulatorEventLink");

            migrationBuilder.DropForeignKey(
                "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                "SimulatorEvents");

            migrationBuilder.DropIndex(
                "IX_SimulatorEvents_HardwarePanelId",
                "SimulatorEvents");

            migrationBuilder.DropColumn(
                "HardwarePanelId",
                "SimulatorEvents");

            migrationBuilder.AlterColumn<int>(
                "HardwarePanelId",
                "HardwareInputs",
                "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                "HardwareInputs",
                "HardwarePanelId",
                "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_SimulatorEventLink_HardwareInputSelectors_HardwareOutputSelectorId",
                "SimulatorEventLink",
                "HardwareOutputSelectorId",
                "HardwareInputSelectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}