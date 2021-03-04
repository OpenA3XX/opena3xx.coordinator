using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_HardwareInputs_HardwareComponents_HardwareComponentId",
                "HardwareInputs");

            migrationBuilder.DropForeignKey(
                "FK_HardwareOutputs_HardwareComponents_HardwareComponentId",
                "HardwareOutputs");

            migrationBuilder.RenameColumn(
                "HardwareComponentId",
                "HardwareOutputs",
                "HardwarePanelId");

            migrationBuilder.RenameIndex(
                "IX_HardwareOutputs_HardwareComponentId",
                table: "HardwareOutputs",
                newName: "IX_HardwareOutputs_HardwarePanelId");

            migrationBuilder.RenameColumn(
                "HardwareComponentId",
                "HardwareInputs",
                "HardwarePanelId");

            migrationBuilder.RenameIndex(
                "IX_HardwareInputs_HardwareComponentId",
                table: "HardwareInputs",
                newName: "IX_HardwareInputs_HardwarePanelId");

            migrationBuilder.AddForeignKey(
                "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                "HardwareInputs",
                "HardwarePanelId",
                "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_HardwareOutputs_HardwarePanels_HardwarePanelId",
                "HardwareOutputs",
                "HardwarePanelId",
                "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_HardwareInputs_HardwarePanels_HardwarePanelId",
                "HardwareInputs");

            migrationBuilder.DropForeignKey(
                "FK_HardwareOutputs_HardwarePanels_HardwarePanelId",
                "HardwareOutputs");

            migrationBuilder.RenameColumn(
                "HardwarePanelId",
                "HardwareOutputs",
                "HardwareComponentId");

            migrationBuilder.RenameIndex(
                "IX_HardwareOutputs_HardwarePanelId",
                table: "HardwareOutputs",
                newName: "IX_HardwareOutputs_HardwareComponentId");

            migrationBuilder.RenameColumn(
                "HardwarePanelId",
                "HardwareInputs",
                "HardwareComponentId");

            migrationBuilder.RenameIndex(
                "IX_HardwareInputs_HardwarePanelId",
                table: "HardwareInputs",
                newName: "IX_HardwareInputs_HardwareComponentId");

            migrationBuilder.AddForeignKey(
                "FK_HardwareInputs_HardwareComponents_HardwareComponentId",
                "HardwareInputs",
                "HardwareComponentId",
                "HardwareComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_HardwareOutputs_HardwareComponents_HardwareComponentId",
                "HardwareOutputs",
                "HardwareComponentId",
                "HardwareComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}