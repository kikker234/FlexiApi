using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fixfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomComponentField_Components_ComponentId",
                table: "CustomComponentField");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "CustomComponentField",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomComponentField_Components_ComponentId",
                table: "CustomComponentField",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomComponentField_Components_ComponentId",
                table: "CustomComponentField");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentId",
                table: "CustomComponentField",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomComponentField_Components_ComponentId",
                table: "CustomComponentField",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
