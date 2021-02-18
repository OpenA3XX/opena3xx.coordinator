using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwareInputSelectorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "HardwareInputs");

            migrationBuilder.CreateTable(
                name: "HardwareInputSelectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HardwareInputId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareInputSelectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareInputSelectors_HardwareInputs_HardwareInputId",
                        column: x => x.HardwareInputId,
                        principalTable: "HardwareInputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardwareInputSelectors_HardwareInputId",
                table: "HardwareInputSelectors",
                column: "HardwareInputId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareInputSelectors");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "HardwareInputs",
                type: "INTEGER",
                nullable: true);
        }
    }
}
