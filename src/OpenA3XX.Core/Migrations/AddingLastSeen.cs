using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingLastSeen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_HardwarePanelTokens_HardwarePanels_HardwarePanelId",
                "HardwarePanelTokens");

            migrationBuilder.AlterColumn<int>(
                "HardwarePanelId",
                "HardwarePanelTokens",
                "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                "LastSeen",
                "HardwarePanelTokens",
                "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                "FK_HardwarePanelTokens_HardwarePanels_HardwarePanelId",
                "HardwarePanelTokens",
                "HardwarePanelId",
                "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_HardwarePanelTokens_HardwarePanels_HardwarePanelId",
                "HardwarePanelTokens");

            migrationBuilder.DropColumn(
                "LastSeen",
                "HardwarePanelTokens");

            migrationBuilder.AlterColumn<int>(
                "HardwarePanelId",
                "HardwarePanelTokens",
                "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                "FK_HardwarePanelTokens_HardwarePanels_HardwarePanelId",
                "HardwarePanelTokens",
                "HardwarePanelId",
                "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}