using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fixcomponentagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Instances_InstanceId1",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_InstanceId1",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "InstanceId1",
                table: "Components");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstanceId1",
                table: "Components",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Components_InstanceId1",
                table: "Components",
                column: "InstanceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Instances_InstanceId1",
                table: "Components",
                column: "InstanceId1",
                principalTable: "Instances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
