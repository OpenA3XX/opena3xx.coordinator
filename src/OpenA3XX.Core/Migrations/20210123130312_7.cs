using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareInputTypeState");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "HardwareOutputs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "HardwareInputs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "HardwareOutputs");

            migrationBuilder.DropColumn(
                name: "State",
                table: "HardwareInputs");

            migrationBuilder.CreateTable(
                name: "HardwareInputTypeState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HardwareInputId = table.Column<int>(type: "INTEGER", nullable: true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareInputTypeState", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareInputTypeState_HardwareInputs_HardwareInputId",
                        column: x => x.HardwareInputId,
                        principalTable: "HardwareInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardwareInputTypeState_HardwareInputId",
                table: "HardwareInputTypeState",
                column: "HardwareInputId");
        }
    }
}
