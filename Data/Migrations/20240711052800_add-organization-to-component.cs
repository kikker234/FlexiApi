using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addorganizationtocomponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Components_OrganizationId",
                table: "Components",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Organisations_OrganizationId",
                table: "Components",
                column: "OrganizationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Organisations_OrganizationId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_OrganizationId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                table: "Components");
        }
    }
}
