using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwarePanelreferencetoSimulatorEventsDataEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                table: "HardwareInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorEventLink_HardwareInputSelectors_HardwareOutputSelectorId",
                table: "SimulatorEventLink");

            migrationBuilder.AddColumn<int>(
                name: "HardwarePanelId",
                table: "SimulatorEvents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "HardwarePanelId",
                table: "HardwareInputs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorEvents_HardwarePanelId",
                table: "SimulatorEvents",
                column: "HardwarePanelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                table: "HardwareInputs",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorEventLink_HardwareOutputSelectors_HardwareOutputSelectorId",
                table: "SimulatorEventLink",
                column: "HardwareOutputSelectorId",
                principalTable: "HardwareOutputSelectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                table: "SimulatorEvents",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                table: "HardwareInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorEventLink_HardwareOutputSelectors_HardwareOutputSelectorId",
                table: "SimulatorEventLink");

            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                table: "SimulatorEvents");

            migrationBuilder.DropIndex(
                name: "IX_SimulatorEvents_HardwarePanelId",
                table: "SimulatorEvents");

            migrationBuilder.DropColumn(
                name: "HardwarePanelId",
                table: "SimulatorEvents");

            migrationBuilder.AlterColumn<int>(
                name: "HardwarePanelId",
                table: "HardwareInputs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                table: "HardwareInputs",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorEventLink_HardwareInputSelectors_HardwareOutputSelectorId",
                table: "SimulatorEventLink",
                column: "HardwareOutputSelectorId",
                principalTable: "HardwareInputSelectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
