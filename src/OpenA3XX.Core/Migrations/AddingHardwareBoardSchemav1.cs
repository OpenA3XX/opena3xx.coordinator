using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwareBoardSchemav1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "IOExtenderBit",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExtenderBusBitType = table.Column<int>("INTEGER", nullable: false),
                    HardwareInputId = table.Column<int>("INTEGER", nullable: false),
                    HardwareOutputId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOExtenderBit", x => x.Id);
                    table.ForeignKey(
                        "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                        x => x.HardwareInputId,
                        "HardwareInputs",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                        x => x.HardwareOutputId,
                        "HardwareOutputs",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "IOExtenderBuses",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Bit0Id = table.Column<int>("INTEGER", nullable: true),
                    Bit1Id = table.Column<int>("INTEGER", nullable: true),
                    Bit2Id = table.Column<int>("INTEGER", nullable: true),
                    Bit3Id = table.Column<int>("INTEGER", nullable: true),
                    Bit4Id = table.Column<int>("INTEGER", nullable: true),
                    Bit5Id = table.Column<int>("INTEGER", nullable: true),
                    Bit6Id = table.Column<int>("INTEGER", nullable: true),
                    Bit7Id = table.Column<int>("INTEGER", nullable: true),
                    Bit8Id = table.Column<int>("INTEGER", nullable: true),
                    Bit9Id = table.Column<int>("INTEGER", nullable: true),
                    Bit10Id = table.Column<int>("INTEGER", nullable: true),
                    Bit11Id = table.Column<int>("INTEGER", nullable: true),
                    Bit12Id = table.Column<int>("INTEGER", nullable: true),
                    Bit13Id = table.Column<int>("INTEGER", nullable: true),
                    Bit14Id = table.Column<int>("INTEGER", nullable: true),
                    Bit15Id = table.Column<int>("INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOExtenderBuses", x => x.Id);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit0Id",
                        x => x.Bit0Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit10Id",
                        x => x.Bit10Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit11Id",
                        x => x.Bit11Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit12Id",
                        x => x.Bit12Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit13Id",
                        x => x.Bit13Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit14Id",
                        x => x.Bit14Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit15Id",
                        x => x.Bit15Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit1Id",
                        x => x.Bit1Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit2Id",
                        x => x.Bit2Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit3Id",
                        x => x.Bit3Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit4Id",
                        x => x.Bit4Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit5Id",
                        x => x.Bit5Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit6Id",
                        x => x.Bit6Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit7Id",
                        x => x.Bit7Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit8Id",
                        x => x.Bit8Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_IOExtenderBuses_IOExtenderBit_Bit9Id",
                        x => x.Bit9Id,
                        "IOExtenderBit",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "HardwareBoards",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    Bus1Id = table.Column<int>("INTEGER", nullable: true),
                    Bus2Id = table.Column<int>("INTEGER", nullable: true),
                    Bus3Id = table.Column<int>("INTEGER", nullable: true),
                    Bus4Id = table.Column<int>("INTEGER", nullable: true),
                    Bus5Id = table.Column<int>("INTEGER", nullable: true),
                    Bus6Id = table.Column<int>("INTEGER", nullable: true),
                    Bus7Id = table.Column<int>("INTEGER", nullable: true),
                    Bus8Id = table.Column<int>("INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareBoards", x => x.Id);
                    table.ForeignKey(
                        "FK_HardwareBoards_IOExtenderBuses_Bus1Id",
                        x => x.Bus1Id,
                        "IOExtenderBuses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_HardwareBoards_IOExtenderBuses_Bus2Id",
                        x => x.Bus2Id,
                        "IOExtenderBuses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_HardwareBoards_IOExtenderBuses_Bus3Id",
                        x => x.Bus3Id,
                        "IOExtenderBuses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_HardwareBoards_IOExtenderBuses_Bus4Id",
                        x => x.Bus4Id,
                        "IOExtenderBuses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_HardwareBoards_IOExtenderBuses_Bus5Id",
                        x => x.Bus5Id,
                        "IOExtenderBuses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_HardwareBoards_IOExtenderBuses_Bus6Id",
                        x => x.Bus6Id,
                        "IOExtenderBuses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_HardwareBoards_IOExtenderBuses_Bus7Id",
                        x => x.Bus7Id,
                        "IOExtenderBuses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_HardwareBoards_IOExtenderBuses_Bus8Id",
                        x => x.Bus8Id,
                        "IOExtenderBuses",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBit_HardwareInputId",
                "IOExtenderBit",
                "HardwareInputId");

            migrationBuilder.CreateIndex(
                "IX_IOExtenderBit_HardwareOutputId",
                "IOExtenderBit",
                "HardwareOutputId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "HardwareBoards");

            migrationBuilder.DropTable(
                "IOExtenderBuses");

            migrationBuilder.DropTable(
                "IOExtenderBit");
        }
    }
}