using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwareOutputSelectorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "State",
                "HardwareOutputs");

            migrationBuilder.CreateTable(
                "HardwareOutputSelectors",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    HardwareOutputId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareOutputSelectors", x => x.Id);
                    table.ForeignKey(
                        "FK_HardwareOutputSelectors_HardwareOutputs_HardwareOutputId",
                        x => x.HardwareOutputId,
                        "HardwareOutputs",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_HardwareOutputSelectors_HardwareOutputId",
                "HardwareOutputSelectors",
                "HardwareOutputId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "HardwareOutputSelectors");

            migrationBuilder.AddColumn<int>(
                "State",
                "HardwareOutputs",
                "INTEGER",
                nullable: true);
        }
    }
}