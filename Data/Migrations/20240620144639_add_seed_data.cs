using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class add_seed_data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Instances",
                columns: new[] { "Id", "Description", "Key", "Name" },
                values: new object[] { 1, "Flexi is a flexible and scalable platform for managing your organization's data", "d58250e3-8e76-4196-8884-8e7138bd6b9b", "Flexi" });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "Id", "DeletedAt", "InstanceId", "Name" },
                values: new object[] { 1, null, 1, "Flexi" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "OrganizationId", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "97d67541-c112-4127-8a2e-b7d1eec3627c", "admin@admin.com", false, false, null, "ADMIN@ADMIN.COM", "ADMIN", 1, "AQAAAAIAAYagAAAAEHLj3RvSg50kY1rRJMhgcGzS3NPvBGCejb9IzQ8LhhrVfzaXhQckPDWDLQhk8P+ZYA==", null, false, "2175b3e7-ded1-43fa-b7ec-d39e576f6189", false, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Organizations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instances",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
