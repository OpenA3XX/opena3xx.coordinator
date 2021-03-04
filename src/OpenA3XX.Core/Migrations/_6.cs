using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "HardwareInputTypeState",
                table => new
                {
                    Id = table.Column<int>("INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HardwareInputId = table.Column<int>("INTEGER", nullable: true),
                    Value = table.Column<int>("INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareInputTypeState", x => x.Id);
                    table.ForeignKey(
                        "FK_HardwareInputTypeState_HardwareInputs_HardwareInputId",
                        x => x.HardwareInputId,
                        "HardwareInputs",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_HardwareInputTypeState_HardwareInputId",
                "HardwareInputTypeState",
                "HardwareInputId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "HardwareInputTypeState");
        }
    }
}