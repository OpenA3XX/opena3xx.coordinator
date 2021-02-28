using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddedSimulatorEventreferencingtoInputSelector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorEvents_HardwareInputSelectors_HardwareInputSelectorId",
                table: "SimulatorEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorEvents_HardwareOutputSelectors_HardwareOutputSelectorId",
                table: "SimulatorEvents");

            migrationBuilder.DropIndex(
                name: "IX_SimulatorEvents_HardwareInputSelectorId",
                table: "SimulatorEvents");

            migrationBuilder.DropIndex(
                name: "IX_SimulatorEvents_HardwareOutputSelectorId",
                table: "SimulatorEvents");

            migrationBuilder.DropColumn(
                name: "HardwareInputSelectorId",
                table: "SimulatorEvents");

            migrationBuilder.DropColumn(
                name: "HardwareOutputSelectorId",
                table: "SimulatorEvents");

            migrationBuilder.AddColumn<int>(
                name: "SimulatorEventId",
                table: "HardwareInputSelectors",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_HardwareInputSelectors_SimulatorEventId",
                table: "HardwareInputSelectors",
                column: "SimulatorEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInputSelectors_SimulatorEvents_SimulatorEventId",
                table: "HardwareInputSelectors",
                column: "SimulatorEventId",
                principalTable: "SimulatorEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInputSelectors_SimulatorEvents_SimulatorEventId",
                table: "HardwareInputSelectors");

            migrationBuilder.DropIndex(
                name: "IX_HardwareInputSelectors_SimulatorEventId",
                table: "HardwareInputSelectors");

            migrationBuilder.DropColumn(
                name: "SimulatorEventId",
                table: "HardwareInputSelectors");

            migrationBuilder.AddColumn<int>(
                name: "HardwareInputSelectorId",
                table: "SimulatorEvents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HardwareOutputSelectorId",
                table: "SimulatorEvents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorEvents_HardwareInputSelectorId",
                table: "SimulatorEvents",
                column: "HardwareInputSelectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorEvents_HardwareOutputSelectorId",
                table: "SimulatorEvents",
                column: "HardwareOutputSelectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorEvents_HardwareInputSelectors_HardwareInputSelectorId",
                table: "SimulatorEvents",
                column: "HardwareInputSelectorId",
                principalTable: "HardwareInputSelectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorEvents_HardwareOutputSelectors_HardwareOutputSelectorId",
                table: "SimulatorEvents",
                column: "HardwareOutputSelectorId",
                principalTable: "HardwareOutputSelectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
