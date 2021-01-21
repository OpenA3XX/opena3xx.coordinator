using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenA3XX.Coordinator.TestHarness.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInput_HardwareComponents_HardwareComponentId",
                table: "HardwareInput");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInput_HardwareInputType_HardwareInputTypeId",
                table: "HardwareInput");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareOutput_HardwareComponents_HardwareComponentId",
                table: "HardwareOutput");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareOutput_HardwareOutputType_HardwareOutputTypeId",
                table: "HardwareOutput");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HardwareOutput",
                table: "HardwareOutput");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HardwareInput",
                table: "HardwareInput");

            migrationBuilder.RenameTable(
                name: "HardwareOutput",
                newName: "HardwareOutputs");

            migrationBuilder.RenameTable(
                name: "HardwareInput",
                newName: "HardwareInputs");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareOutput_HardwareOutputTypeId",
                table: "HardwareOutputs",
                newName: "IX_HardwareOutputs_HardwareOutputTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareOutput_HardwareComponentId",
                table: "HardwareOutputs",
                newName: "IX_HardwareOutputs_HardwareComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareInput_HardwareInputTypeId",
                table: "HardwareInputs",
                newName: "IX_HardwareInputs_HardwareInputTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareInput_HardwareComponentId",
                table: "HardwareInputs",
                newName: "IX_HardwareInputs_HardwareComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HardwareOutputs",
                table: "HardwareOutputs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HardwareInputs",
                table: "HardwareInputs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInputs_HardwareComponents_HardwareComponentId",
                table: "HardwareInputs",
                column: "HardwareComponentId",
                principalTable: "HardwareComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInputs_HardwareInputType_HardwareInputTypeId",
                table: "HardwareInputs",
                column: "HardwareInputTypeId",
                principalTable: "HardwareInputType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareOutputs_HardwareComponents_HardwareComponentId",
                table: "HardwareOutputs",
                column: "HardwareComponentId",
                principalTable: "HardwareComponents",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInputs_HardwareComponents_HardwareComponentId",
                table: "HardwareInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareInputs_HardwareInputType_HardwareInputTypeId",
                table: "HardwareInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareOutputs_HardwareComponents_HardwareComponentId",
                table: "HardwareOutputs");

            migrationBuilder.DropForeignKey(
                name: "FK_HardwareOutputs_HardwareOutputType_HardwareOutputTypeId",
                table: "HardwareOutputs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HardwareOutputs",
                table: "HardwareOutputs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HardwareInputs",
                table: "HardwareInputs");

            migrationBuilder.RenameTable(
                name: "HardwareOutputs",
                newName: "HardwareOutput");

            migrationBuilder.RenameTable(
                name: "HardwareInputs",
                newName: "HardwareInput");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareOutputs_HardwareOutputTypeId",
                table: "HardwareOutput",
                newName: "IX_HardwareOutput_HardwareOutputTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareOutputs_HardwareComponentId",
                table: "HardwareOutput",
                newName: "IX_HardwareOutput_HardwareComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareInputs_HardwareInputTypeId",
                table: "HardwareInput",
                newName: "IX_HardwareInput_HardwareInputTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_HardwareInputs_HardwareComponentId",
                table: "HardwareInput",
                newName: "IX_HardwareInput_HardwareComponentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HardwareOutput",
                table: "HardwareOutput",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HardwareInput",
                table: "HardwareInput",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInput_HardwareComponents_HardwareComponentId",
                table: "HardwareInput",
                column: "HardwareComponentId",
                principalTable: "HardwareComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareInput_HardwareInputType_HardwareInputTypeId",
                table: "HardwareInput",
                column: "HardwareInputTypeId",
                principalTable: "HardwareInputType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareOutput_HardwareComponents_HardwareComponentId",
                table: "HardwareOutput",
                column: "HardwareComponentId",
                principalTable: "HardwareComponents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HardwareOutput_HardwareOutputType_HardwareOutputTypeId",
                table: "HardwareOutput",
                column: "HardwareOutputTypeId",
                principalTable: "HardwareOutputType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
