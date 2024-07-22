using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class addeddeletion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ComponentObjects",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "ComponentObjects",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentObjects_DeletedById",
                table: "ComponentObjects",
                column: "DeletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ComponentObjects_AspNetUsers_DeletedById",
                table: "ComponentObjects",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComponentObjects_AspNetUsers_DeletedById",
                table: "ComponentObjects");

            migrationBuilder.DropIndex(
                name: "IX_ComponentObjects_DeletedById",
                table: "ComponentObjects");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ComponentObjects");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "ComponentObjects");
        }
    }
}
