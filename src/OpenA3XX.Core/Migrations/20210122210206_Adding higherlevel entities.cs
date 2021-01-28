using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class Addinghigherlevelentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HardwareInputType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareInputType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HardwareOutputType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOutputType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufucturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufucturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AircraftModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(type: "TEXT", nullable: true),
                    ManufucturerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AircraftModels_Manufucturers_ManufucturerId",
                        column: x => x.ManufucturerId,
                        principalTable: "Manufucturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HardwarePanels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    AircraftModelId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwarePanels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwarePanels_AircraftModels_AircraftModelId",
                        column: x => x.AircraftModelId,
                        principalTable: "AircraftModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HardwareComponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HardwarePanelId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    InternalId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareComponents_HardwarePanels_HardwarePanelId",
                        column: x => x.HardwarePanelId,
                        principalTable: "HardwarePanels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HardwareInputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HardwareInputTypeId = table.Column<int>(type: "INTEGER", nullable: true),
                    HardwareComponentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareInputs_HardwareComponents_HardwareComponentId",
                        column: x => x.HardwareComponentId,
                        principalTable: "HardwareComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareInputs_HardwareInputType_HardwareInputTypeId",
                        column: x => x.HardwareInputTypeId,
                        principalTable: "HardwareInputType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HardwareOutputs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HardwareOutputTypeId = table.Column<int>(type: "INTEGER", nullable: true),
                    HardwareComponentId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareOutputs_HardwareComponents_HardwareComponentId",
                        column: x => x.HardwareComponentId,
                        principalTable: "HardwareComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareOutputs_HardwareOutputType_HardwareOutputTypeId",
                        column: x => x.HardwareOutputTypeId,
                        principalTable: "HardwareOutputType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AircraftModels_ManufucturerId",
                table: "AircraftModels",
                column: "ManufucturerId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareComponents_HardwarePanelId",
                table: "HardwareComponents",
                column: "HardwarePanelId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareInputs_HardwareComponentId",
                table: "HardwareInputs",
                column: "HardwareComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareInputs_HardwareInputTypeId",
                table: "HardwareInputs",
                column: "HardwareInputTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareOutputs_HardwareComponentId",
                table: "HardwareOutputs",
                column: "HardwareComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareOutputs_HardwareOutputTypeId",
                table: "HardwareOutputs",
                column: "HardwareOutputTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwarePanels_AircraftModelId",
                table: "HardwarePanels",
                column: "AircraftModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareInputs");

            migrationBuilder.DropTable(
                name: "HardwareOutputs");

            migrationBuilder.DropTable(
                name: "HardwareInputType");

            migrationBuilder.DropTable(
                name: "HardwareComponents");

            migrationBuilder.DropTable(
                name: "HardwareOutputType");

            migrationBuilder.DropTable(
                name: "HardwarePanels");

            migrationBuilder.DropTable(
                name: "AircraftModels");

            migrationBuilder.DropTable(
                name: "Manufucturers");
        }
    }
}
