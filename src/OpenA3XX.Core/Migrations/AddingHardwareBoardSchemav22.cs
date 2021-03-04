using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwareBoardSchemav22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "HardwareBus",
                "IOExtenderBuses",
                "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "HardwareBit",
                "IOExtenderBits",
                "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "HardwareBus",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "HardwareBit",
                "IOExtenderBits");
        }
    }
}