using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddedSimulatorEventreferencingtoInputSelector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_SimulatorEvents_HardwareInputSelectors_HardwareInputSelectorId",
                "SimulatorEvents");

            migrationBuilder.DropForeignKey(
                "FK_SimulatorEvents_HardwareOutputSelectors_HardwareOutputSelectorId",
                "SimulatorEvents");

            migrationBuilder.DropIndex(
                "IX_SimulatorEvents_HardwareInputSelectorId",
                "SimulatorEvents");

            migrationBuilder.DropIndex(
                "IX_SimulatorEvents_HardwareOutputSelectorId",
                "SimulatorEvents");

            migrationBuilder.DropColumn(
                "HardwareInputSelectorId",
                "SimulatorEvents");

            migrationBuilder.DropColumn(
                "HardwareOutputSelectorId",
                "SimulatorEvents");

            migrationBuilder.AddColumn<int>(
                "SimulatorEventId",
                "HardwareInputSelectors",
                "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_HardwareInputSelectors_SimulatorEventId",
                "HardwareInputSelectors",
                "SimulatorEventId");

            migrationBuilder.AddForeignKey(
                "FK_HardwareInputSelectors_SimulatorEvents_SimulatorEventId",
                "HardwareInputSelectors",
                "SimulatorEventId",
                "SimulatorEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_HardwareInputSelectors_SimulatorEvents_SimulatorEventId",
                "HardwareInputSelectors");

            migrationBuilder.DropIndex(
                "IX_HardwareInputSelectors_SimulatorEventId",
                "HardwareInputSelectors");

            migrationBuilder.DropColumn(
                "SimulatorEventId",
                "HardwareInputSelectors");

            migrationBuilder.AddColumn<int>(
                "HardwareInputSelectorId",
                "SimulatorEvents",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "HardwareOutputSelectorId",
                "SimulatorEvents",
                "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_SimulatorEvents_HardwareInputSelectorId",
                "SimulatorEvents",
                "HardwareInputSelectorId");

            migrationBuilder.CreateIndex(
                "IX_SimulatorEvents_HardwareOutputSelectorId",
                "SimulatorEvents",
                "HardwareOutputSelectorId");

            migrationBuilder.AddForeignKey(
                "FK_SimulatorEvents_HardwareInputSelectors_HardwareInputSelectorId",
                "SimulatorEvents",
                "HardwareInputSelectorId",
                "HardwareInputSelectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_SimulatorEvents_HardwareOutputSelectors_HardwareOutputSelectorId",
                "SimulatorEvents",
                "HardwareOutputSelectorId",
                "HardwareOutputSelectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}