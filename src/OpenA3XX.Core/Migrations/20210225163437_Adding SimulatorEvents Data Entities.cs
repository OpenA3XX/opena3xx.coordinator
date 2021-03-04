using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingSimulatorEventsDataEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "SimulatorEvents",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventName = table.Column<string>("TEXT", nullable: true),
                    SimulatorEventType = table.Column<int>("INTEGER", nullable: false),
                    SimulatorSoftware = table.Column<int>("INTEGER", nullable: false),
                    SimulatorEventSdkType = table.Column<int>("INTEGER", nullable: false),
                    EventCode = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_SimulatorEvents", x => x.Id); });

            migrationBuilder.CreateTable(
                "SimulatorEventLink",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SimulatorEventId = table.Column<int>("INTEGER", nullable: true),
                    HardwareInputSelectorId = table.Column<int>("INTEGER", nullable: true),
                    HardwareOutputSelectorId = table.Column<int>("INTEGER", nullable: true)
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
                        "FK_SimulatorEventLink_HardwareInputSelectors_HardwareOutputSelectorId",
                        x => x.HardwareOutputSelectorId,
                        "HardwareInputSelectors",
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "SimulatorEventLink");

            migrationBuilder.DropTable(
                "SimulatorEvents");
        }
    }
}