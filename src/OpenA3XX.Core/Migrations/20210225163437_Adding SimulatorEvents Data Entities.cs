using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingSimulatorEventsDataEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SimulatorEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventName = table.Column<string>(type: "TEXT", nullable: true),
                    SimulatorEventType = table.Column<int>(type: "INTEGER", nullable: false),
                    SimulatorSoftware = table.Column<int>(type: "INTEGER", nullable: false),
                    SimulatorEventSdkType = table.Column<int>(type: "INTEGER", nullable: false),
                    EventCode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimulatorEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimulatorEventLink",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimulatorEventId = table.Column<int>(type: "INTEGER", nullable: true),
                    HardwareInputSelectorId = table.Column<int>(type: "INTEGER", nullable: true),
                    HardwareOutputSelectorId = table.Column<int>(type: "INTEGER", nullable: true)
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
                        name: "FK_SimulatorEventLink_HardwareInputSelectors_HardwareOutputSelectorId",
                        column: x => x.HardwareOutputSelectorId,
                        principalTable: "HardwareInputSelectors",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SimulatorEventLink");

            migrationBuilder.DropTable(
                name: "SimulatorEvents");
        }
    }
}
