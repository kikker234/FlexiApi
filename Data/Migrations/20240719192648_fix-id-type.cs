using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class fixidtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentObjects_AspNetUsers_CreatedById1",
                table: "ComponentObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentObjects_AspNetUsers_UpdatedById1",
                table: "ComponentObjects");

            migrationBuilder.DropIndex(
                name: "IX_ComponentObjects_CreatedById1",
                table: "ComponentObjects");

            migrationBuilder.DropIndex(
                name: "IX_ComponentObjects_UpdatedById1",
                table: "ComponentObjects");

            migrationBuilder.DropColumn(
                name: "CreatedById1",
                table: "ComponentObjects");

            migrationBuilder.DropColumn(
                name: "UpdatedById1",
                table: "ComponentObjects");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedById",
                table: "ComponentObjects",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "ComponentObjects",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentObjects_CreatedById",
                table: "ComponentObjects",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentObjects_UpdatedById",
                table: "ComponentObjects",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentObjects_AspNetUsers_CreatedById",
                table: "ComponentObjects",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentObjects_AspNetUsers_UpdatedById",
                table: "ComponentObjects",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentObjects_AspNetUsers_CreatedById",
                table: "ComponentObjects");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentObjects_AspNetUsers_UpdatedById",
                table: "ComponentObjects");

            migrationBuilder.DropIndex(
                name: "IX_ComponentObjects_CreatedById",
                table: "ComponentObjects");

            migrationBuilder.DropIndex(
                name: "IX_ComponentObjects_UpdatedById",
                table: "ComponentObjects");

            migrationBuilder.AlterColumn<int>(
                name: "UpdatedById",
                table: "ComponentObjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "ComponentObjects",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById1",
                table: "ComponentObjects",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById1",
                table: "ComponentObjects",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentObjects_CreatedById1",
                table: "ComponentObjects",
                column: "CreatedById1");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentObjects_UpdatedById1",
                table: "ComponentObjects",
                column: "UpdatedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentObjects_AspNetUsers_CreatedById1",
                table: "ComponentObjects",
                column: "CreatedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentObjects_AspNetUsers_UpdatedById1",
                table: "ComponentObjects",
                column: "UpdatedById1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
