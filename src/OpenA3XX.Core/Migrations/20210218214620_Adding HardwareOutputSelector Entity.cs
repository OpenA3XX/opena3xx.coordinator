using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwareOutputSelectorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "HardwareOutputs");

            migrationBuilder.CreateTable(
                name: "HardwareOutputSelectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    HardwareOutputId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOutputSelectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardwareOutputSelectors_HardwareOutputs_HardwareOutputId",
                        column: x => x.HardwareOutputId,
                        principalTable: "HardwareOutputs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HardwareOutputSelectors_HardwareOutputId",
                table: "HardwareOutputSelectors",
                column: "HardwareOutputId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardwareOutputSelectors");

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "HardwareOutputs",
                type: "INTEGER",
                nullable: true);
        }
    }
}
