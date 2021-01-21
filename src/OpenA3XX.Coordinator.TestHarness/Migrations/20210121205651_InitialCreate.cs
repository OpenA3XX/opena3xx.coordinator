using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HardwareComponents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareComponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HardwareInputType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOutputType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HardwareInput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HardwareInputTypeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    HardwareComponentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareInput", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareInput_HardwareComponents_HardwareComponentId",
                        column: x => x.HardwareComponentId,
                        principalTable: "HardwareComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareInput_HardwareInputType_HardwareInputTypeId",
                        column: x => x.HardwareInputTypeId,
                        principalTable: "HardwareInputType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HardwareOutput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HardwareOutputTypeId = table.Column<Guid>(type: "TEXT", nullable: true),
                    HardwareComponentId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOutput", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareOutput_HardwareComponents_HardwareComponentId",
                        column: x => x.HardwareComponentId,
                        principalTable: "HardwareComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HardwareOutput_HardwareOutputType_HardwareOutputTypeId",
                        column: x => x.HardwareOutputTypeId,
                        principalTable: "HardwareOutputType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardwareInput_HardwareComponentId",
                table: "HardwareInput",
                column: "HardwareComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareInput_HardwareInputTypeId",
                table: "HardwareInput",
                column: "HardwareInputTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareOutput_HardwareComponentId",
                table: "HardwareOutput",
                column: "HardwareComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_HardwareOutput_HardwareOutputTypeId",
                table: "HardwareOutput",
                column: "HardwareOutputTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareInput");

            migrationBuilder.DropTable(
                name: "HardwareOutput");

            migrationBuilder.DropTable(
                name: "HardwareInputType");

            migrationBuilder.DropTable(
                name: "HardwareComponents");

            migrationBuilder.DropTable(
                name: "HardwareOutputType");
        }
    }
}
