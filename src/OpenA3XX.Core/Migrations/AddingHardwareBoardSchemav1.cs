using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwareBoardSchemav1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IOExtenderBit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExtenderBusBitType = table.Column<int>(type: "INTEGER", nullable: false),
                    HardwareInputId = table.Column<int>(type: "INTEGER", nullable: false),
                    HardwareOutputId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOExtenderBit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IOExtenderBit_HardwareInputs_HardwareInputId",
                        column: x => x.HardwareInputId,
                        principalTable: "HardwareInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IOExtenderBit_HardwareOutputs_HardwareOutputId",
                        column: x => x.HardwareOutputId,
                        principalTable: "HardwareOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IOExtenderBuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Bit0Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit1Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit2Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit3Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit4Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit5Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit6Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit7Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit8Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit9Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit10Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit11Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit12Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit13Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit14Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bit15Id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IOExtenderBuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit0Id",
                        column: x => x.Bit0Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit10Id",
                        column: x => x.Bit10Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit11Id",
                        column: x => x.Bit11Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit12Id",
                        column: x => x.Bit12Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit13Id",
                        column: x => x.Bit13Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit14Id",
                        column: x => x.Bit14Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit15Id",
                        column: x => x.Bit15Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit1Id",
                        column: x => x.Bit1Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit2Id",
                        column: x => x.Bit2Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit3Id",
                        column: x => x.Bit3Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit4Id",
                        column: x => x.Bit4Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit5Id",
                        column: x => x.Bit5Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit6Id",
                        column: x => x.Bit6Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit7Id",
                        column: x => x.Bit7Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit8Id",
                        column: x => x.Bit8Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IOExtenderBuses_IOExtenderBit_Bit9Id",
                        column: x => x.Bit9Id,
                        principalTable: "IOExtenderBit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HardwareBoards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Bus1Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bus2Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bus3Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bus4Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bus5Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bus6Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bus7Id = table.Column<int>(type: "INTEGER", nullable: true),
                    Bus8Id = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareBoards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareBoards_IOExtenderBuses_Bus1Id",
                        column: x => x.Bus1Id,
                        principalTable: "IOExtenderBuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareBoards_IOExtenderBuses_Bus2Id",
                        column: x => x.Bus2Id,
                        principalTable: "IOExtenderBuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareBoards_IOExtenderBuses_Bus3Id",
                        column: x => x.Bus3Id,
                        principalTable: "IOExtenderBuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareBoards_IOExtenderBuses_Bus4Id",
                        column: x => x.Bus4Id,
                        principalTable: "IOExtenderBuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareBoards_IOExtenderBuses_Bus5Id",
                        column: x => x.Bus5Id,
                        principalTable: "IOExtenderBuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareBoards_IOExtenderBuses_Bus6Id",
                        column: x => x.Bus6Id,
                        principalTable: "IOExtenderBuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareBoards_IOExtenderBuses_Bus7Id",
                        column: x => x.Bus7Id,
                        principalTable: "IOExtenderBuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareBoards_IOExtenderBuses_Bus8Id",
                        column: x => x.Bus8Id,
                        principalTable: "IOExtenderBuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBit_HardwareInputId",
                table: "IOExtenderBit",
                column: "HardwareInputId");

            migrationBuilder.CreateIndex(
                name: "IX_IOExtenderBit_HardwareOutputId",
                table: "IOExtenderBit",
                column: "HardwareOutputId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareBoards");

            migrationBuilder.DropTable(
                name: "IOExtenderBuses");

            migrationBuilder.DropTable(
                name: "IOExtenderBit");
        }
    }
}
