using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class Addinghigherlevelentities13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareOutputs_HardwarePanels_HardwarePanelId",
                table: "HardwareOutputs");

            migrationBuilder.AlterColumn<int>(
                name: "HardwarePanelId",
                table: "HardwareOutputs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareOutputs_HardwarePanels_HardwarePanelId",
                table: "HardwareOutputs",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareOutputs_HardwarePanels_HardwarePanelId",
                table: "HardwareOutputs");

            migrationBuilder.AlterColumn<int>(
                name: "HardwarePanelId",
                table: "HardwareOutputs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareOutputs_HardwarePanels_HardwarePanelId",
                table: "HardwareOutputs",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
