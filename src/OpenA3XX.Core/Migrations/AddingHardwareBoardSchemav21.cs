using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwareBoardSchemav21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus1Id",
                table: "HardwareBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus2Id",
                table: "HardwareBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus3Id",
                table: "HardwareBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus4Id",
                table: "HardwareBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus5Id",
                table: "HardwareBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus6Id",
                table: "HardwareBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus7Id",
                table: "HardwareBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus8Id",
                table: "HardwareBoards");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                table: "IOExtenderBit");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                table: "IOExtenderBit");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit0Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit10Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit11Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit12Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit13Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit14Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit15Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit1Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit2Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit3Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit4Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit5Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit6Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit7Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit8Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit9Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit0Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit10Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit11Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit12Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit13Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit14Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit15Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit1Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit2Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit3Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit4Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit5Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit6Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit7Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit8Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_Bit9Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_HardwareBoards_Bus1Id",
                table: "HardwareBoards");

            migrationBuilder.DropIndex(
                name: "IX_HardwareBoards_Bus2Id",
                table: "HardwareBoards");

            migrationBuilder.DropIndex(
                name: "IX_HardwareBoards_Bus3Id",
                table: "HardwareBoards");

            migrationBuilder.DropIndex(
                name: "IX_HardwareBoards_Bus4Id",
                table: "HardwareBoards");

            migrationBuilder.DropIndex(
                name: "IX_HardwareBoards_Bus5Id",
                table: "HardwareBoards");

            migrationBuilder.DropIndex(
                name: "IX_HardwareBoards_Bus6Id",
                table: "HardwareBoards");

            migrationBuilder.DropIndex(
                name: "IX_HardwareBoards_Bus7Id",
                table: "HardwareBoards");

            migrationBuilder.DropIndex(
                name: "IX_HardwareBoards_Bus8Id",
                table: "HardwareBoards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IOExtenderBit",
                table: "IOExtenderBit");

            migrationBuilder.DropColumn(
                name: "Bit0Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit10Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit11Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit12Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit13Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit14Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit15Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit1Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit2Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit3Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit4Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit5Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit6Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit7Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit8Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bit9Id",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "Bus1Id",
                table: "HardwareBoards");

            migrationBuilder.DropColumn(
                name: "Bus2Id",
                table: "HardwareBoards");

            migrationBuilder.DropColumn(
                name: "Bus3Id",
                table: "HardwareBoards");

            migrationBuilder.DropColumn(
                name: "Bus4Id",
                table: "HardwareBoards");

            migrationBuilder.DropColumn(
                name: "Bus5Id",
                table: "HardwareBoards");

            migrationBuilder.DropColumn(
                name: "Bus6Id",
                table: "HardwareBoards");

            migrationBuilder.DropColumn(
                name: "Bus7Id",
                table: "HardwareBoards");

            migrationBuilder.DropColumn(
                name: "Bus8Id",
                table: "HardwareBoards");

            migrationBuilder.RenameTable(
                name: "IOExtenderBit",
                newName: "IOExtenderBits");

            migrationBuilder.RenameIndex(
                name: "IX_IOExtenderBit_HardwareOutputId",
                table: "IOExtenderBits",
                newName: "IX_IOExtenderBits_HardwareOutputId");

            migrationBuilder.RenameIndex(
                name: "IX_IOExtenderBit_HardwareInputId",
                table: "IOExtenderBits",
                newName: "IX_IOExtenderBits_HardwareInputId");

            migrationBuilder.AddColumn<int>(
                name: "HardwareBoardId",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IOExtenderBusId",
                table: "IOExtenderBits",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IOExtenderBits",
                table: "IOExtenderBits",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_HardwareBoardId",
                table: "IOExtenderBuses",
                column: "HardwareBoardId");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBits_IOExtenderBusId",
                table: "IOExtenderBits",
                column: "IOExtenderBusId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBits_IOExtenderBuses_IOExtenderBusId",
                table: "IOExtenderBits",
                column: "IOExtenderBusId",
                principalTable: "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_HardwareBoards_HardwareBoardId",
                table: "IOExtenderBuses",
                column: "HardwareBoardId",
                principalTable: "HardwareBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBits_HardwareInputs_HardwareInputId",
                table: "IOExtenderBits");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBits_HardwareOutputs_HardwareOutputId",
                table: "IOExtenderBits");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBits_IOExtenderBuses_IOExtenderBusId",
                table: "IOExtenderBits");

            migrationBuilder.DropForeignKey(
                name: "FK_IOExtenderBuses_HardwareBoards_HardwareBoardId",
                table: "IOExtenderBuses");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBuses_HardwareBoardId",
                table: "IOExtenderBuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IOExtenderBits",
                table: "IOExtenderBits");

            migrationBuilder.DropIndex(
                name: "IX_IOExtenderBits_IOExtenderBusId",
                table: "IOExtenderBits");

            migrationBuilder.DropColumn(
                name: "HardwareBoardId",
                table: "IOExtenderBuses");

            migrationBuilder.DropColumn(
                name: "IOExtenderBusId",
                table: "IOExtenderBits");

            migrationBuilder.RenameTable(
                name: "IOExtenderBits",
                newName: "IOExtenderBit");

            migrationBuilder.RenameIndex(
                name: "IX_IOExtenderBits_HardwareOutputId",
                table: "IOExtenderBit",
                newName: "IX_IOExtenderBit_HardwareOutputId");

            migrationBuilder.RenameIndex(
                name: "IX_IOExtenderBits_HardwareInputId",
                table: "IOExtenderBit",
                newName: "IX_IOExtenderBit_HardwareInputId");

            migrationBuilder.AddColumn<int>(
                name: "Bit0Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit10Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit11Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit12Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit13Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit14Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit15Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit1Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit2Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit3Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit4Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit5Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit6Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit7Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit8Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bit9Id",
                table: "IOExtenderBuses",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bus1Id",
                table: "HardwareBoards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bus2Id",
                table: "HardwareBoards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bus3Id",
                table: "HardwareBoards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bus4Id",
                table: "HardwareBoards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bus5Id",
                table: "HardwareBoards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bus6Id",
                table: "HardwareBoards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bus7Id",
                table: "HardwareBoards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Bus8Id",
                table: "HardwareBoards",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_IOExtenderBit",
                table: "IOExtenderBit",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit0Id",
                table: "IOExtenderBuses",
                column: "Bit0Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit10Id",
                table: "IOExtenderBuses",
                column: "Bit10Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit11Id",
                table: "IOExtenderBuses",
                column: "Bit11Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit12Id",
                table: "IOExtenderBuses",
                column: "Bit12Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit13Id",
                table: "IOExtenderBuses",
                column: "Bit13Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit14Id",
                table: "IOExtenderBuses",
                column: "Bit14Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit15Id",
                table: "IOExtenderBuses",
                column: "Bit15Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit1Id",
                table: "IOExtenderBuses",
                column: "Bit1Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit2Id",
                table: "IOExtenderBuses",
                column: "Bit2Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit3Id",
                table: "IOExtenderBuses",
                column: "Bit3Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit4Id",
                table: "IOExtenderBuses",
                column: "Bit4Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit5Id",
                table: "IOExtenderBuses",
                column: "Bit5Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit6Id",
                table: "IOExtenderBuses",
                column: "Bit6Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit7Id",
                table: "IOExtenderBuses",
                column: "Bit7Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit8Id",
                table: "IOExtenderBuses",
                column: "Bit8Id");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBuses_Bit9Id",
                table: "IOExtenderBuses",
                column: "Bit9Id");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareBoards_Bus1Id",
                table: "HardwareBoards",
                column: "Bus1Id");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareBoards_Bus2Id",
                table: "HardwareBoards",
                column: "Bus2Id");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareBoards_Bus3Id",
                table: "HardwareBoards",
                column: "Bus3Id");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareBoards_Bus4Id",
                table: "HardwareBoards",
                column: "Bus4Id");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareBoards_Bus5Id",
                table: "HardwareBoards",
                column: "Bus5Id");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareBoards_Bus6Id",
                table: "HardwareBoards",
                column: "Bus6Id");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareBoards_Bus7Id",
                table: "HardwareBoards",
                column: "Bus7Id");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareBoards_Bus8Id",
                table: "HardwareBoards",
                column: "Bus8Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus1Id",
                table: "HardwareBoards",
                column: "Bus1Id",
                principalTable: "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus2Id",
                table: "HardwareBoards",
                column: "Bus2Id",
                principalTable: "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus3Id",
                table: "HardwareBoards",
                column: "Bus3Id",
                principalTable: "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus4Id",
                table: "HardwareBoards",
                column: "Bus4Id",
                principalTable: "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus5Id",
                table: "HardwareBoards",
                column: "Bus5Id",
                principalTable: "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus6Id",
                table: "HardwareBoards",
                column: "Bus6Id",
                principalTable: "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus7Id",
                table: "HardwareBoards",
                column: "Bus7Id",
                principalTable: "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareBoards_IOExtenderBuses_Bus8Id",
                table: "HardwareBoards",
                column: "Bus8Id",
                principalTable: "IOExtenderBuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit0Id",
                table: "IOExtenderBuses",
                column: "Bit0Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit10Id",
                table: "IOExtenderBuses",
                column: "Bit10Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit11Id",
                table: "IOExtenderBuses",
                column: "Bit11Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit12Id",
                table: "IOExtenderBuses",
                column: "Bit12Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit13Id",
                table: "IOExtenderBuses",
                column: "Bit13Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit14Id",
                table: "IOExtenderBuses",
                column: "Bit14Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit15Id",
                table: "IOExtenderBuses",
                column: "Bit15Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit1Id",
                table: "IOExtenderBuses",
                column: "Bit1Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit2Id",
                table: "IOExtenderBuses",
                column: "Bit2Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit3Id",
                table: "IOExtenderBuses",
                column: "Bit3Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit4Id",
                table: "IOExtenderBuses",
                column: "Bit4Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit5Id",
                table: "IOExtenderBuses",
                column: "Bit5Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit6Id",
                table: "IOExtenderBuses",
                column: "Bit6Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit7Id",
                table: "IOExtenderBuses",
                column: "Bit7Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit8Id",
                table: "IOExtenderBuses",
                column: "Bit8Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IOExtenderBuses_IOExtenderBit_Bit9Id",
                table: "IOExtenderBuses",
                column: "Bit9Id",
                principalTable: "IOExtenderBit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
