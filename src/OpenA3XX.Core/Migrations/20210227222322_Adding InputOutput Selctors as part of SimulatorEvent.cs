using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingInputOutputSelctorsaspartofSimulatorEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                table: "SimulatorEvents");

            migrationBuilder.DropTable(
                name: "SimulatorEventLink");

            migrationBuilder.RenameColumn(
                name: "HardwarePanelId",
                table: "SimulatorEvents",
                newName: "HardwareOutputSelectorId");

            migrationBuilder.RenameIndex(
                name: "IX_SimulatorEvents_HardwarePanelId",
                table: "SimulatorEvents",
                newName: "IX_SimulatorEvents_HardwareOutputSelectorId");

            migrationBuilder.AddColumn<int>(
                name: "HardwareInputSelectorId",
                table: "SimulatorEvents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorEvents_HardwareInputSelectorId",
                table: "SimulatorEvents",
                column: "HardwareInputSelectorId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "HardwareInputSelectorId",
                table: "SimulatorEvents");

            migrationBuilder.RenameColumn(
                name: "HardwareOutputSelectorId",
                table: "SimulatorEvents",
                newName: "HardwarePanelId");

            migrationBuilder.RenameIndex(
                name: "IX_SimulatorEvents_HardwareOutputSelectorId",
                table: "SimulatorEvents",
                newName: "IX_SimulatorEvents_HardwarePanelId");

            migrationBuilder.CreateTable(
                name: "SimulatorEventLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HardwareInputSelectorId = table.Column<int>(type: "INTEGER", nullable: true),
                    HardwareOutputSelectorId = table.Column<int>(type: "INTEGER", nullable: true),
                    SimulatorEventId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorEventLink", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SimulatorEventLink_HardwareInputSelectors_HardwareInputSelectorId",
                        column: x => x.HardwareInputSelectorId,
                        principalTable: "HardwareInputSelectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SimulatorEventLink_HardwareOutputSelectors_HardwareOutputSelectorId",
                        column: x => x.HardwareOutputSelectorId,
                        principalTable: "HardwareOutputSelectors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SimulatorEventLink_SimulatorEvents_SimulatorEventId",
                        column: x => x.SimulatorEventId,
                        principalTable: "SimulatorEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorEventLink_HardwareInputSelectorId",
                table: "SimulatorEventLink",
                column: "HardwareInputSelectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorEventLink_HardwareOutputSelectorId",
                table: "SimulatorEventLink",
                column: "HardwareOutputSelectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SimulatorEventLink_SimulatorEventId",
                table: "SimulatorEventLink",
                column: "SimulatorEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_SimulatorEvents_HardwarePanels_HardwarePanelId",
                table: "SimulatorEvents",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
