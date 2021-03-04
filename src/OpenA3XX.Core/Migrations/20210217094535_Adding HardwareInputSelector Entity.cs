using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class AddingHardwareInputSelectorEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "State",
                "HardwareInputs");

            migrationBuilder.CreateTable(
                "HardwareInputSelectors",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>("TEXT", nullable: true),
                    HardwareInputId = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareInputSelectors", x => x.Id);
                    table.ForeignKey(
                        "FK_HardwareInputSelectors_HardwareInputs_HardwareInputId",
                        x => x.HardwareInputId,
                        "HardwareInputs",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_HardwareInputSelectors_HardwareInputId",
                "HardwareInputSelectors",
                "HardwareInputId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "HardwareInputSelectors");

            migrationBuilder.AddColumn<int>(
                "State",
                "HardwareInputs",
                "INTEGER",
                nullable: true);
        }
    }
}