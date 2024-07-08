using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class shitload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InstanceId",
                table: "Organisations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlanId",
                table: "Organisations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Organisations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Plans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    InstanceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plans_Instances_InstanceId",
                        column: x => x.InstanceId,
                        principalTable: "Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_InstanceId",
                table: "Organisations",
                column: "InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Organisations_PlanId",
                table: "Organisations",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Plans_InstanceId",
                table: "Plans",
                column: "InstanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_Instances_InstanceId",
                table: "Organisations",
                column: "InstanceId",
                principalTable: "Instances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organisations_Plans_PlanId",
                table: "Organisations",
                column: "PlanId",
                principalTable: "Plans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_Instances_InstanceId",
                table: "Organisations");

            migrationBuilder.DropForeignKey(
                name: "FK_Organisations_Plans_PlanId",
                table: "Organisations");

            migrationBuilder.DropTable(
                name: "Plans");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_InstanceId",
                table: "Organisations");

            migrationBuilder.DropIndex(
                name: "IX_Organisations_PlanId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "InstanceId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "PlanId",
                table: "Organisations");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Organisations");
        }
    }
}
