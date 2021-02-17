using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingLastSeen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwarePanelTokens_HardwarePanels_HardwarePanelId",
                table: "HardwarePanelTokens");

            migrationBuilder.AlterColumn<int>(
                name: "HardwarePanelId",
                table: "HardwarePanelTokens",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSeen",
                table: "HardwarePanelTokens",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_HardwarePanelTokens_HardwarePanels_HardwarePanelId",
                table: "HardwarePanelTokens",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwarePanelTokens_HardwarePanels_HardwarePanelId",
                table: "HardwarePanelTokens");

            migrationBuilder.DropColumn(
                name: "LastSeen",
                table: "HardwarePanelTokens");

            migrationBuilder.AlterColumn<int>(
                name: "HardwarePanelId",
                table: "HardwarePanelTokens",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwarePanelTokens_HardwarePanels_HardwarePanelId",
                table: "HardwarePanelTokens",
                column: "HardwarePanelId",
                principalTable: "HardwarePanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
