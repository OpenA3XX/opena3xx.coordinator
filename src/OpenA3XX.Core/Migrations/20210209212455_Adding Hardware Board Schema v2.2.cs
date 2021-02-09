using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwareBoardSchemav22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HardwareBus",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HardwareBit",
                table: "IOExtenderBits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HardwareBus",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "HardwareBit",
                table: "IOExtenderBits");
        }
    }
}
