using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AllowHardwareInputIdOutputIdasnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                table: "IOExtenderBit");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                table: "IOExtenderBit");

            migrationBuilder.AlterColumn<int>(
                name: "HardwareOutputId",
                table: "IOExtenderBit",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "HardwareInputId",
                table: "IOExtenderBit",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                table: "IOExtenderBit",
                column: "HardwareInputId",
                principalTable: "HardwareInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                table: "IOExtenderBit",
                column: "HardwareOutputId",
                principalTable: "HardwareOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                table: "IOExtenderBit");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                table: "IOExtenderBit");

            migrationBuilder.AlterColumn<int>(
                name: "HardwareOutputId",
                table: "IOExtenderBit",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HardwareInputId",
                table: "IOExtenderBit",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                table: "IOExtenderBit",
                column: "HardwareInputId",
                principalTable: "HardwareInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                table: "IOExtenderBit",
                column: "HardwareOutputId",
                principalTable: "HardwareOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
