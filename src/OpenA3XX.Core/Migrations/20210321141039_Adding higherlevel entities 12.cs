using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class Addinghigherlevelentities12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBits_HardwareInputs_HardwareInputId",
                table: "IOExtenderBits");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBits_HardwareOutputs_HardwareOutputId",
                table: "IOExtenderBits");

            migrationBuilder.RenameColumn(
                name: "HardwareOutputId",
                table: "IOExtenderBits",
                newName: "HardwareOutputSelectorId");

            migrationBuilder.RenameColumn(
                name: "HardwareInputId",
                table: "IOExtenderBits",
                newName: "HardwareInputSelectorId");

            migrationBuilder.RenameIndex(
                name: "IX_IOExtenderBits_HardwareOutputId",
                table: "IOExtenderBits",
                newName: "IX_IOExtenderBits_HardwareOutputSelectorId");

            migrationBuilder.RenameIndex(
                name: "IX_IOExtenderBits_HardwareInputId",
                table: "IOExtenderBits",
                newName: "IX_IOExtenderBits_HardwareInputSelectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBits_HardwareInputSelectors_HardwareInputSelectorId",
                table: "IOExtenderBits",
                column: "HardwareInputSelectorId",
                principalTable: "HardwareInputSelectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBits_HardwareOutputSelectors_HardwareOutputSelectorId",
                table: "IOExtenderBits",
                column: "HardwareOutputSelectorId",
                principalTable: "HardwareOutputSelectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBits_HardwareInputSelectors_HardwareInputSelectorId",
                table: "IOExtenderBits");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBits_HardwareOutputSelectors_HardwareOutputSelectorId",
                table: "IOExtenderBits");

            migrationBuilder.RenameColumn(
                name: "HardwareOutputSelectorId",
                table: "IOExtenderBits",
                newName: "HardwareOutputId");

            migrationBuilder.RenameColumn(
                name: "HardwareInputSelectorId",
                table: "IOExtenderBits",
                newName: "HardwareInputId");

            migrationBuilder.RenameIndex(
                name: "IX_IOExtenderBits_HardwareOutputSelectorId",
                table: "IOExtenderBits",
                newName: "IX_IOExtenderBits_HardwareOutputId");

            migrationBuilder.RenameIndex(
                name: "IX_IOExtenderBits_HardwareInputSelectorId",
                table: "IOExtenderBits",
                newName: "IX_IOExtenderBits_HardwareInputId");

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBits_HardwareInputs_HardwareInputId",
                table: "IOExtenderBits",
                column: "HardwareInputId",
                principalTable: "HardwareInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBits_HardwareOutputs_HardwareOutputId",
                table: "IOExtenderBits",
                column: "HardwareOutputId",
                principalTable: "HardwareOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
