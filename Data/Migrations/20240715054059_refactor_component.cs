using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class refactor_component : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomComponentField");

            migrationBuilder.DropColumn(
                name: "ComponentType",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "Rentable_Price",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "TimePeriod",
                table: "Components");

            migrationBuilder.CreateTable(
                name: "ComponentFields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentFields_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ComponentData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ComponentFieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentData_ComponentFields_ComponentFieldId",
                        column: x => x.ComponentFieldId,
                        principalTable: "ComponentFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ComponentValidations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ComponentFieldId = table.Column<int>(type: "int", nullable: false),
                    ValidationData = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentValidations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComponentValidations_ComponentFields_ComponentFieldId",
                        column: x => x.ComponentFieldId,
                        principalTable: "ComponentFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentData_ComponentFieldId",
                table: "ComponentData",
                column: "ComponentFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentFields_ComponentId",
                table: "ComponentFields",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComponentValidations_ComponentFieldId",
                table: "ComponentValidations",
                column: "ComponentFieldId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentData");

            migrationBuilder.DropTable(
                name: "ComponentValidations");

            migrationBuilder.DropTable(
                name: "ComponentFields");

            migrationBuilder.AddColumn<string>(
                name: "ComponentType",
                table: "Components",
                type: "varchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Components",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "Components",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Components",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rentable_Price",
                table: "Components",
                type: "decimal(65,30)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TimePeriod",
                table: "Components",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomComponentField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ComponentId = table.Column<int>(type: "int", nullable: true),
                    IsRequired = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomComponentField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomComponentField_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CustomComponentField_ComponentId",
                table: "CustomComponentField",
                column: "ComponentId");
        }
    }
}
