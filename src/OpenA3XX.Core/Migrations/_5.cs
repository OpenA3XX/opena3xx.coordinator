using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "HardwareComponents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "HardwareComponents",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HardwarePanelId = table.Column<int>("INTEGER", nullable: true),
                    InternalId = table.Column<string>("TEXT", nullable: true),
                    Name = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareComponents", x => x.Id);
                    table.ForeignKey(
                        "FK_HardwareComponents_HardwarePanels_HardwarePanelId",
                        x => x.HardwarePanelId,
                        "HardwarePanels",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_HardwareComponents_HardwarePanelId",
                "HardwareComponents",
                "HardwarePanelId");
        }
    }
}