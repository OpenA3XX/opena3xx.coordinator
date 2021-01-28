using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInputs_HardwareComponents_HardwareComponentId",
                table: "HardwareInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareOutputs_HardwareComponents_HardwareComponentId",
                table: "HardwareOutputs");

            migrationBuilder.RenameColumn(
                name: "HardwareComponentId",
                table: "HardwareOutputs",
                newName: "HardwarePanelId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareOutputs_HardwareComponentId",
                table: "HardwareOutputs",
                newName: "IX_HardwareOutputs_HardwarePanelId");

            migrationBuilder.RenameColumn(
                name: "HardwareComponentId",
                table: "HardwareInputs",
                newName: "HardwarePanelId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareInputs_HardwareComponentId",
                table: "HardwareInputs",
                newName: "IX_HardwareInputs_HardwarePanelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                table: "HardwareInputs",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareOutputs_HardwarePanels_HardwarePanelId",
                table: "HardwareOutputs",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                table: "HardwareInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareOutputs_HardwarePanels_HardwarePanelId",
                table: "HardwareOutputs");

            migrationBuilder.RenameColumn(
                name: "HardwarePanelId",
                table: "HardwareOutputs",
                newName: "HardwareComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareOutputs_HardwarePanelId",
                table: "HardwareOutputs",
                newName: "IX_HardwareOutputs_HardwareComponentId");

            migrationBuilder.RenameColumn(
                name: "HardwarePanelId",
                table: "HardwareInputs",
                newName: "HardwareComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareInputs_HardwarePanelId",
                table: "HardwareInputs",
                newName: "IX_HardwareInputs_HardwareComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInputs_HardwareComponents_HardwareComponentId",
                table: "HardwareInputs",
                column: "HardwareComponentId",
                principalTable: "HardwareComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareOutputs_HardwareComponents_HardwareComponentId",
                table: "HardwareOutputs",
                column: "HardwareComponentId",
                principalTable: "HardwareComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
