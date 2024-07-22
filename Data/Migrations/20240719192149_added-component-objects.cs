using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addedcomponentobjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentData_ComponentFields_ComponentFieldId",
                table: "ComponentData");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentFieldId",
                table: "ComponentData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComponentObjectId",
                table: "ComponentData",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComponentObjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedById1 = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UpdatedById1 = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentObjects_AspNetUsers_CreatedById1",
                        column: x => x.CreatedById1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ComponentObjects_AspNetUsers_UpdatedById1",
                        column: x => x.UpdatedById1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentData_ComponentObjectId",
                table: "ComponentData",
                column: "ComponentObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentObjects_CreatedById1",
                table: "ComponentObjects",
                column: "CreatedById1");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentObjects_UpdatedById1",
                table: "ComponentObjects",
                column: "UpdatedById1");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentData_ComponentFields_ComponentFieldId",
                table: "ComponentData",
                column: "ComponentFieldId",
                principalTable: "ComponentFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentData_ComponentObjects_ComponentObjectId",
                table: "ComponentData",
                column: "ComponentObjectId",
                principalTable: "ComponentObjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentData_ComponentFields_ComponentFieldId",
                table: "ComponentData");

            migrationBuilder.DropForeignKey(
                name: "FK_ComponentData_ComponentObjects_ComponentObjectId",
                table: "ComponentData");

            migrationBuilder.DropTable(
                name: "ComponentObjects");

            migrationBuilder.DropIndex(
                name: "IX_ComponentData_ComponentObjectId",
                table: "ComponentData");

            migrationBuilder.DropColumn(
                name: "ComponentObjectId",
                table: "ComponentData");

            migrationBuilder.AlterColumn<int>(
                name: "ComponentFieldId",
                table: "ComponentData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentData_ComponentFields_ComponentFieldId",
                table: "ComponentData",
                column: "ComponentFieldId",
                principalTable: "ComponentFields",
                principalColumn: "Id");
        }
    }
}
