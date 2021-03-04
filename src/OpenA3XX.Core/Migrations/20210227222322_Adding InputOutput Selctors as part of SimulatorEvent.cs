using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingInputOutputSelctorsaspartofSimulatorEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                "SimulatorEvents");

            migrationBuilder.DropTable(
                "SimulatorEventLink");

            migrationBuilder.RenameColumn(
                "HardwarePanelId",
                "SimulatorEvents",
                "HardwareOutputSelectorId");

            migrationBuilder.RenameIndex(
                "IX_SimulatorEvents_HardwarePanelId",
                table: "SimulatorEvents",
                newName: "IX_SimulatorEvents_HardwareOutputSelectorId");

            migrationBuilder.AddColumn<int>(
                "HardwareInputSelectorId",
                "SimulatorEvents",
                "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_SimulatorEvents_HardwareInputSelectorId",
                "SimulatorEvents",
                "HardwareInputSelectorId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                "HardwareInputSelectorId",
                "SimulatorEvents");

            migrationBuilder.RenameColumn(
                "HardwareOutputSelectorId",
                "SimulatorEvents",
                "HardwarePanelId");

            migrationBuilder.RenameIndex(
                "IX_SimulatorEvents_HardwareOutputSelectorId",
                table: "SimulatorEvents",
                newName: "IX_SimulatorEvents_HardwarePanelId");

            migrationBuilder.CreateTable(
                "SimulatorEventLink",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HardwareInputSelectorId = table.Column<int>("INTEGER", nullable: true),
                    HardwareOutputSelectorId = table.Column<int>("INTEGER", nullable: true),
                    SimulatorEventId = table.Column<int>("INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorEventLink", x => x.Id);
                    table.ForeignKey(
                        "FK_SimulatorEventLink_HardwareInputSelectors_HardwareInputSelectorId",
                        x => x.HardwareInputSelectorId,
                        "HardwareInputSelectors",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_SimulatorEventLink_HardwareOutputSelectors_HardwareOutputSelectorId",
                        x => x.HardwareOutputSelectorId,
                        "HardwareOutputSelectors",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_SimulatorEventLink_SimulatorEvents_SimulatorEventId",
                        x => x.SimulatorEventId,
                        "SimulatorEvents",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_SimulatorEventLink_HardwareInputSelectorId",
                "SimulatorEventLink",
                "HardwareInputSelectorId");

            migrationBuilder.CreateIndex(
                "IX_SimulatorEventLink_HardwareOutputSelectorId",
                "SimulatorEventLink",
                "HardwareOutputSelectorId");

            migrationBuilder.CreateIndex(
                "IX_SimulatorEventLink_SimulatorEventId",
                "SimulatorEventLink",
                "SimulatorEventId");

            migrationBuilder.AddForeignKey(
                "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                "SimulatorEvents",
                "HardwarePanelId",
                "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}