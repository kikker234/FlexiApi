using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fixagin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentFields_ComponentData_ComponentDataId",
                table: "ComponentFields");

            migrationBuilder.DropIndex(
                name: "IX_ComponentFields_ComponentDataId",
                table: "ComponentFields");

            migrationBuilder.DropColumn(
                name: "ComponentDataId",
                table: "ComponentFields");

            migrationBuilder.AddColumn<int>(
                name: "ComponentFieldId",
                table: "ComponentData",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentData_ComponentFieldId",
                table: "ComponentData",
                column: "ComponentFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentData_ComponentFields_ComponentFieldId",
                table: "ComponentData",
                column: "ComponentFieldId",
                principalTable: "ComponentFields",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentData_ComponentFields_ComponentFieldId",
                table: "ComponentData");

            migrationBuilder.DropIndex(
                name: "IX_ComponentData_ComponentFieldId",
                table: "ComponentData");

            migrationBuilder.DropColumn(
                name: "ComponentFieldId",
                table: "ComponentData");

            migrationBuilder.AddColumn<int>(
                name: "ComponentDataId",
                table: "ComponentFields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentFields_ComponentDataId",
                table: "ComponentFields",
                column: "ComponentDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentFields_ComponentData_ComponentDataId",
                table: "ComponentFields",
                column: "ComponentDataId",
                principalTable: "ComponentData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
