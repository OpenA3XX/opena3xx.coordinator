using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HardwarePanelOwner",
                table: "HardwarePanels",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HardwarePanelOwner",
                table: "HardwarePanels");
        }
    }
}
