using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class reworked_components : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValidationData",
                table: "ComponentValidations",
                newName: "ValidationValue");

            migrationBuilder.AddColumn<string>(
                name: "ValidationType",
                table: "ComponentValidations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "ComponentFields",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidationType",
                table: "ComponentValidations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ComponentFields");

            migrationBuilder.RenameColumn(
                name: "ValidationValue",
                table: "ComponentValidations",
                newName: "ValidationData");
        }
    }
}
