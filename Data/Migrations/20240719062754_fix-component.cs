using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fixcomponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Organisations_OrganizationId",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "OrganizationId",
                table: "Components",
                newName: "InstanceId1");

            migrationBuilder.RenameIndex(
                name: "IX_Components_OrganizationId",
                table: "Components",
                newName: "IX_Components_InstanceId1");

            migrationBuilder.AddColumn<string>(
                name: "InstanceId",
                table: "Components",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Instances_InstanceId1",
                table: "Components",
                column: "InstanceId1",
                principalTable: "Instances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Instances_InstanceId1",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "InstanceId",
                table: "Components");

            migrationBuilder.RenameColumn(
                name: "InstanceId1",
                table: "Components",
                newName: "OrganizationId");

            migrationBuilder.RenameIndex(
                name: "IX_Components_InstanceId1",
                table: "Components",
                newName: "IX_Components_OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Organisations_OrganizationId",
                table: "Components",
                column: "OrganizationId",
                principalTable: "Organisations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
