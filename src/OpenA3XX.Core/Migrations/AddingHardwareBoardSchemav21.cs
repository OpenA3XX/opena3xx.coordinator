using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwareBoardSchemav21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus1Id",
                "HardwareBoards");

            migrationBuilder.DropForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus2Id",
                "HardwareBoards");

            migrationBuilder.DropForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus3Id",
                "HardwareBoards");

            migrationBuilder.DropForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus4Id",
                "HardwareBoards");

            migrationBuilder.DropForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus5Id",
                "HardwareBoards");

            migrationBuilder.DropForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus6Id",
                "HardwareBoards");

            migrationBuilder.DropForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus7Id",
                "HardwareBoards");

            migrationBuilder.DropForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus8Id",
                "HardwareBoards");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                "IOExtenderBit");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                "IOExtenderBit");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit0Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit10Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit11Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit12Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit13Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit14Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit15Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit1Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit2Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit3Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit4Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit5Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit6Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit7Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit8Id",
                "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit9Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit0Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit10Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit11Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit12Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit13Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit14Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit15Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit1Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit2Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit3Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit4Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit5Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit6Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit7Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit8Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_Bit9Id",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_HardwareBoards_Bus1Id",
                "HardwareBoards");

            migrationBuilder.DropIndex(
                "IX_HardwareBoards_Bus2Id",
                "HardwareBoards");

            migrationBuilder.DropIndex(
                "IX_HardwareBoards_Bus3Id",
                "HardwareBoards");

            migrationBuilder.DropIndex(
                "IX_HardwareBoards_Bus4Id",
                "HardwareBoards");

            migrationBuilder.DropIndex(
                "IX_HardwareBoards_Bus5Id",
                "HardwareBoards");

            migrationBuilder.DropIndex(
                "IX_HardwareBoards_Bus6Id",
                "HardwareBoards");

            migrationBuilder.DropIndex(
                "IX_HardwareBoards_Bus7Id",
                "HardwareBoards");

            migrationBuilder.DropIndex(
                "IX_HardwareBoards_Bus8Id",
                "HardwareBoards");

            migrationBuilder.DropPrimaryKey(
                "PK_IOExtenderBit",
                "IOExtenderBit");

            migrationBuilder.DropColumn(
                "Bit0Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit10Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit11Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit12Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit13Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit14Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit15Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit1Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit2Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit3Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit4Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit5Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit6Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit7Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit8Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bit9Id",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "Bus1Id",
                "HardwareBoards");

            migrationBuilder.DropColumn(
                "Bus2Id",
                "HardwareBoards");

            migrationBuilder.DropColumn(
                "Bus3Id",
                "HardwareBoards");

            migrationBuilder.DropColumn(
                "Bus4Id",
                "HardwareBoards");

            migrationBuilder.DropColumn(
                "Bus5Id",
                "HardwareBoards");

            migrationBuilder.DropColumn(
                "Bus6Id",
                "HardwareBoards");

            migrationBuilder.DropColumn(
                "Bus7Id",
                "HardwareBoards");

            migrationBuilder.DropColumn(
                "Bus8Id",
                "HardwareBoards");

            migrationBuilder.RenameTable(
                "IOExtenderBit",
                newName: "IOExtenderBits");

            migrationBuilder.RenameIndex(
                "IX_IOExtenderBit_HardwareOutputId",
                table: "IOExtenderBits",
                newName: "IX_IOExtenderBits_HardwareOutputId");

            migrationBuilder.RenameIndex(
                "IX_IOExtenderBit_HardwareInputId",
                table: "IOExtenderBits",
                newName: "IX_IOExtenderBits_HardwareInputId");

            migrationBuilder.AddColumn<int>(
                "HardwareBoardId",
                "IOExtenderBuses",
                "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                "IOExtenderBusId",
                "IOExtenderBits",
                "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                "PK_IOExtenderBits",
                "IOExtenderBits",
                "Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_HardwareBoardId",
                "IOExtenderBuses",
                "HardwareBoardId");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBits_IOExtenderBusId",
                "IOExtenderBits",
                "IOExtenderBusId");

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBits_HardwareInputs_HardwareInputId",
                "IOExtenderBits",
                "HardwareInputId",
                "HardwareInputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBits_HardwareOutputs_HardwareOutputId",
                "IOExtenderBits",
                "HardwareOutputId",
                "HardwareOutputs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBits_IOExtenderBuses_IOExtenderBusId",
                "IOExtenderBits",
                "IOExtenderBusId",
                "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_HardwareBoards_HardwareBoardId",
                "IOExtenderBuses",
                "HardwareBoardId",
                "HardwareBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBits_HardwareInputs_HardwareInputId",
                "IOExtenderBits");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBits_HardwareOutputs_HardwareOutputId",
                "IOExtenderBits");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBits_IOExtenderBuses_IOExtenderBusId",
                "IOExtenderBits");

            migrationBuilder.DropForeignKey(
                "FK_IOExtenderBuses_HardwareBoards_HardwareBoardId",
                "IOExtenderBuses");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBuses_HardwareBoardId",
                "IOExtenderBuses");

            migrationBuilder.DropPrimaryKey(
                "PK_IOExtenderBits",
                "IOExtenderBits");

            migrationBuilder.DropIndex(
                "IX_IOExtenderBits_IOExtenderBusId",
                "IOExtenderBits");

            migrationBuilder.DropColumn(
                "HardwareBoardId",
                "IOExtenderBuses");

            migrationBuilder.DropColumn(
                "IOExtenderBusId",
                "IOExtenderBits");

            migrationBuilder.RenameTable(
                "IOExtenderBits",
                newName: "IOExtenderBit");

            migrationBuilder.RenameIndex(
                "IX_IOExtenderBits_HardwareOutputId",
                table: "IOExtenderBit",
                newName: "IX_IOExtenderBit_HardwareOutputId");

            migrationBuilder.RenameIndex(
                "IX_IOExtenderBits_HardwareInputId",
                table: "IOExtenderBit",
                newName: "IX_IOExtenderBit_HardwareInputId");

            migrationBuilder.AddColumn<int>(
                "Bit0Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit10Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit11Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit12Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit13Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit14Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit15Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit1Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit2Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit3Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit4Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit5Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit6Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit7Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit8Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bit9Id",
                "IOExtenderBuses",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bus1Id",
                "HardwareBoards",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bus2Id",
                "HardwareBoards",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bus3Id",
                "HardwareBoards",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bus4Id",
                "HardwareBoards",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bus5Id",
                "HardwareBoards",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bus6Id",
                "HardwareBoards",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bus7Id",
                "HardwareBoards",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                "Bus8Id",
                "HardwareBoards",
                "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                "PK_IOExtenderBit",
                "IOExtenderBit",
                "Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit0Id",
                "IOExtenderBuses",
                "Bit0Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit10Id",
                "IOExtenderBuses",
                "Bit10Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit11Id",
                "IOExtenderBuses",
                "Bit11Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit12Id",
                "IOExtenderBuses",
                "Bit12Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit13Id",
                "IOExtenderBuses",
                "Bit13Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit14Id",
                "IOExtenderBuses",
                "Bit14Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit15Id",
                "IOExtenderBuses",
                "Bit15Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit1Id",
                "IOExtenderBuses",
                "Bit1Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit2Id",
                "IOExtenderBuses",
                "Bit2Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit3Id",
                "IOExtenderBuses",
                "Bit3Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit4Id",
                "IOExtenderBuses",
                "Bit4Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit5Id",
                "IOExtenderBuses",
                "Bit5Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit6Id",
                "IOExtenderBuses",
                "Bit6Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit7Id",
                "IOExtenderBuses",
                "Bit7Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit8Id",
                "IOExtenderBuses",
                "Bit8Id");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBuses_Bit9Id",
                "IOExtenderBuses",
                "Bit9Id");

            migrationBuilder.CreateIndex(
                "IX_HardwareBoards_Bus1Id",
                "HardwareBoards",
                "Bus1Id");

            migrationBuilder.CreateIndex(
                "IX_HardwareBoards_Bus2Id",
                "HardwareBoards",
                "Bus2Id");

            migrationBuilder.CreateIndex(
                "IX_HardwareBoards_Bus3Id",
                "HardwareBoards",
                "Bus3Id");

            migrationBuilder.CreateIndex(
                "IX_HardwareBoards_Bus4Id",
                "HardwareBoards",
                "Bus4Id");

            migrationBuilder.CreateIndex(
                "IX_HardwareBoards_Bus5Id",
                "HardwareBoards",
                "Bus5Id");

            migrationBuilder.CreateIndex(
                "IX_HardwareBoards_Bus6Id",
                "HardwareBoards",
                "Bus6Id");

            migrationBuilder.CreateIndex(
                "IX_HardwareBoards_Bus7Id",
                "HardwareBoards",
                "Bus7Id");

            migrationBuilder.CreateIndex(
                "IX_HardwareBoards_Bus8Id",
                "HardwareBoards",
                "Bus8Id");

            migrationBuilder.AddForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus1Id",
                "HardwareBoards",
                "Bus1Id",
                "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus2Id",
                "HardwareBoards",
                "Bus2Id",
                "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus3Id",
                "HardwareBoards",
                "Bus3Id",
                "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus4Id",
                "HardwareBoards",
                "Bus4Id",
                "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus5Id",
                "HardwareBoards",
                "Bus5Id",
                "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus6Id",
                "HardwareBoards",
                "Bus6Id",
                "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus7Id",
                "HardwareBoards",
                "Bus7Id",
                "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_HardwareBoards_IOExtenderBuses_Bus8Id",
                "HardwareBoards",
                "Bus8Id",
                "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit0Id",
                "IOExtenderBuses",
                "Bit0Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit10Id",
                "IOExtenderBuses",
                "Bit10Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit11Id",
                "IOExtenderBuses",
                "Bit11Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit12Id",
                "IOExtenderBuses",
                "Bit12Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit13Id",
                "IOExtenderBuses",
                "Bit13Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit14Id",
                "IOExtenderBuses",
                "Bit14Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit15Id",
                "IOExtenderBuses",
                "Bit15Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit1Id",
                "IOExtenderBuses",
                "Bit1Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit2Id",
                "IOExtenderBuses",
                "Bit2Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit3Id",
                "IOExtenderBuses",
                "Bit3Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit4Id",
                "IOExtenderBuses",
                "Bit4Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit5Id",
                "IOExtenderBuses",
                "Bit5Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit6Id",
                "IOExtenderBuses",
                "Bit6Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit7Id",
                "IOExtenderBuses",
                "Bit7Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit8Id",
                "IOExtenderBuses",
                "Bit8Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_IOExtenderBuses_IOExtenderBit_Bit9Id",
                "IOExtenderBuses",
                "Bit9Id",
                "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}