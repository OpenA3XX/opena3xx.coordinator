using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AllowHardwareInputIdOutputIdasnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                "IOExtenderBit");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                "IOExtenderBit");

            migrationBuilder.AlterColumn<int>(
                "HardwareOutputId",
                "IOExtenderBit",
                "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                "HardwareInputId",
                "IOExtenderBit",
                "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                "IOExtenderBit",
                "HardwareInputId",
                "HardwareInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                "IOExtenderBit",
                "HardwareOutputId",
                "HardwareOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                "IOExtenderBit");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                "IOExtenderBit");

            migrationBuilder.AlterColumn<int>(
                "HardwareOutputId",
                "IOExtenderBit",
                "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                "HardwareInputId",
                "IOExtenderBit",
                "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                "IOExtenderBit",
                "HardwareInputId",
                "HardwareInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                "IOExtenderBit",
                "HardwareOutputId",
                "HardwareOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}