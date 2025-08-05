using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditTableIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AuditEntries_EntityName_EntityId",
                table: "AuditEntries",
                columns: new[] { "EntityName", "EntityId" });

            migrationBuilder.CreateIndex(
                name: "IX_AuditEntries_Timestamp",
                table: "AuditEntries",
                column: "Timestamp");

            migrationBuilder.CreateIndex(
                name: "IX_AuditEntries_UserId",
                table: "AuditEntries",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AuditEntries_UserId",
                table: "AuditEntries");

            migrationBuilder.DropIndex(
                name: "IX_AuditEntries_Timestamp",
                table: "AuditEntries");

            migrationBuilder.DropIndex(
                name: "IX_AuditEntries_EntityName_EntityId",
                table: "AuditEntries");
        }
    }
}
