using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwarePanelTokens2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HardwarePanelTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceToken = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeviceIpAddress = table.Column<string>(type: "TEXT", nullable: true),
                    HardwarePanelId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwarePanelTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwarePanelTokens_HardwarePanels_HardwarePanelId",
                        column: x => x.HardwarePanelId,
                        principalTable: "HardwarePanels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardwarePanelTokens_HardwarePanelId",
                table: "HardwarePanelTokens",
                column: "HardwarePanelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwarePanelTokens");
        }
    }
}
