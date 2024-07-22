using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fixed_component_object_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComponentId",
                table: "ComponentObjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ComponentObjects_ComponentId",
                table: "ComponentObjects",
                column: "ComponentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentObjects_Components_ComponentId",
                table: "ComponentObjects",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentObjects_Components_ComponentId",
                table: "ComponentObjects");

            migrationBuilder.DropIndex(
                name: "IX_ComponentObjects_ComponentId",
                table: "ComponentObjects");

            migrationBuilder.DropColumn(
                name: "ComponentId",
                table: "ComponentObjects");
        }
    }
}
