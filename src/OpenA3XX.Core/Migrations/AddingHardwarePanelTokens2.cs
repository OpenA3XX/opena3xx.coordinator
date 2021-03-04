using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwarePanelTokens2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "HardwarePanelTokens",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DeviceToken = table.Column<string>("TEXT", nullable: true),
                    CreatedDateTime = table.Column<DateTime>("TEXT", nullable: false),
                    DeviceIpAddress = table.Column<string>("TEXT", nullable: true),
                    HardwarePanelId = table.Column<int>("INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwarePanelTokens", x => x.Id);
                    table.ForeignKey(
                        "FK_HardwarePanelTokens_HardwarePanels_HardwarePanelId",
                        x => x.HardwarePanelId,
                        "HardwarePanels",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_HardwarePanelTokens_HardwarePanelId",
                "HardwarePanelTokens",
                "HardwarePanelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "HardwarePanelTokens");
        }
    }
}