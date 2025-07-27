using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSystemConfigurationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Check if the table exists before trying to drop it
            migrationBuilder.Sql("DROP TABLE IF EXISTS \"SystemConfiguration\"");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Key = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfiguration", x => x.Id);
                });
        }
    }
}
