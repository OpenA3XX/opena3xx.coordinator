using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class Addinghigherlevelentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "HardwareInputType",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_HardwareInputType", x => x.Id); });

            migrationBuilder.CreateTable(
                "HardwareOutputType",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_HardwareOutputType", x => x.Id); });

            migrationBuilder.CreateTable(
                "Manufucturers",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Manufucturers", x => x.Id); });

            migrationBuilder.CreateTable(
                "AircraftModels",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>("TEXT", nullable: true),
                    ManufucturerId = table.Column<int>("INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AircraftModels", x => x.Id);
                    table.ForeignKey(
                        "FK_AircraftModels_Manufucturers_ManufucturerId",
                        x => x.ManufucturerId,
                        "Manufucturers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "HardwarePanels",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    AircraftModelId = table.Column<int>("INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwarePanels", x => x.Id);
                    table.ForeignKey(
                        "FK_HardwarePanels_AircraftModels_AircraftModelId",
                        x => x.AircraftModelId,
                        "AircraftModels",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "HardwareComponents",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HardwarePanelId = table.Column<int>("INTEGER", nullable: true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    InternalId = table.Column<string>("TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareComponents", x => x.Id);
                    table.ForeignKey(
                        "FK_HardwareComponents_HardwarePanels_HardwarePanelId",
                        x => x.HardwarePanelId,
                        "HardwarePanels",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "HardwareInputs",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    HardwareInputTypeId = table.Column<int>("INTEGER", nullable: true),
                    HardwareComponentId = table.Column<int>("INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareInputs", x => x.Id);
                    table.ForeignKey(
                        "FK_HardwareInputs_HardwareComponents_HardwareComponentId",
                        x => x.HardwareComponentId,
                        "HardwareComponents",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_HardwareInputs_HardwareInputType_HardwareInputTypeId",
                        x => x.HardwareInputTypeId,
                        "HardwareInputType",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "HardwareOutputs",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    HardwareOutputTypeId = table.Column<int>("INTEGER", nullable: true),
                    HardwareComponentId = table.Column<int>("INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOutputs", x => x.Id);
                    table.ForeignKey(
                        "FK_HardwareOutputs_HardwareComponents_HardwareComponentId",
                        x => x.HardwareComponentId,
                        "HardwareComponents",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_HardwareOutputs_HardwareOutputType_HardwareOutputTypeId",
                        x => x.HardwareOutputTypeId,
                        "HardwareOutputType",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_AircraftModels_ManufucturerId",
                "AircraftModels",
                "ManufucturerId");

            migrationBuilder.CreateIndex(
                "IX_HardwareComponents_HardwarePanelId",
                "HardwareComponents",
                "HardwarePanelId");

            migrationBuilder.CreateIndex(
                "IX_HardwareInputs_HardwareComponentId",
                "HardwareInputs",
                "HardwareComponentId");

            migrationBuilder.CreateIndex(
                "IX_HardwareInputs_HardwareInputTypeId",
                "HardwareInputs",
                "HardwareInputTypeId");

            migrationBuilder.CreateIndex(
                "IX_HardwareOutputs_HardwareComponentId",
                "HardwareOutputs",
                "HardwareComponentId");

            migrationBuilder.CreateIndex(
                "IX_HardwareOutputs_HardwareOutputTypeId",
                "HardwareOutputs",
                "HardwareOutputTypeId");

            migrationBuilder.CreateIndex(
                "IX_HardwarePanels_AircraftModelId",
                "HardwarePanels",
                "AircraftModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "HardwareInputs");

            migrationBuilder.DropTable(
                "HardwareOutputs");

            migrationBuilder.DropTable(
                "HardwareInputType");

            migrationBuilder.DropTable(
                "HardwareComponents");

            migrationBuilder.DropTable(
                "HardwareOutputType");

            migrationBuilder.DropTable(
                "HardwarePanels");

            migrationBuilder.DropTable(
                "AircraftModels");

            migrationBuilder.DropTable(
                "Manufucturers");
        }
    }
}