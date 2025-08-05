using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInputs_HardwareInputType_HardwareInputTypeId",
                table: "HardwareInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareOutputs_HardwareOutputType_HardwareOutputTypeId",
                table: "HardwareOutputs");

            migrationBuilder.AlterColumn<string>(
                name: "FriendlyName",
                table: "SimulatorEvents",
                type: "TEXT",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                table: "SimulatorEvents",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Manufacturers",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceToken",
                table: "HardwarePanelTokens",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwarePanels",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareOutputType",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareOutputSelectors",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareOutputs",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareInputType",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareInputSelectors",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareInputs",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "AircraftModels",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AuditEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EntityName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Action = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    OldValues = table.Column<string>(type: "TEXT", nullable: true),
                    NewValues = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", maxLength: 45, nullable: true),
                    Context = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    HttpMethod = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Endpoint = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntries", x => x.Id);
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
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardwareComponents_HardwarePanelId",
                table: "HardwareComponents",
                column: "HardwarePanelId");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInputs_HardwareInputType_HardwareInputTypeId",
                table: "HardwareInputs",
                column: "HardwareInputTypeId",
                principalTable: "HardwareInputType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareOutputs_HardwareOutputType_HardwareOutputTypeId",
                table: "HardwareOutputs",
                column: "HardwareOutputTypeId",
                principalTable: "HardwareOutputType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInputs_HardwareInputType_HardwareInputTypeId",
                table: "HardwareInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareOutputs_HardwareOutputType_HardwareOutputTypeId",
                table: "HardwareOutputs");

            migrationBuilder.DropTable(
                name: "AuditEntries");

            migrationBuilder.DropTable(
                name: "HardwareComponents");

            migrationBuilder.AlterColumn<string>(
                name: "FriendlyName",
                table: "SimulatorEvents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                table: "SimulatorEvents",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Manufacturers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "DeviceToken",
                table: "HardwarePanelTokens",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwarePanels",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareOutputType",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareOutputSelectors",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareOutputs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareInputType",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareInputSelectors",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "HardwareInputs",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "AircraftModels",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInputs_HardwareInputType_HardwareInputTypeId",
                table: "HardwareInputs",
                column: "HardwareInputTypeId",
                principalTable: "HardwareInputType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareOutputs_HardwareOutputType_HardwareOutputTypeId",
                table: "HardwareOutputs",
                column: "HardwareOutputTypeId",
                principalTable: "HardwareOutputType",
                principalColumn: "Id");
        }
    }
}
