using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class orgdocsbool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsMonitoring",
                schema: "organizations",
                table: "organization",
                newName: "is_monitoring");

            migrationBuilder.RenameColumn(
                name: "IsIct",
                schema: "organizations",
                table: "organization",
                newName: "is_ict");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "organizations",
                table: "organization",
                newName: "is_active");

            migrationBuilder.AddColumn<bool>(
                name: "has_org_documents",
                schema: "organizations",
                table: "organization",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "has_org_documents",
                schema: "organizations",
                table: "organization");

            migrationBuilder.RenameColumn(
                name: "is_monitoring",
                schema: "organizations",
                table: "organization",
                newName: "IsMonitoring");

            migrationBuilder.RenameColumn(
                name: "is_ict",
                schema: "organizations",
                table: "organization",
                newName: "IsIct");

            migrationBuilder.RenameColumn(
                name: "is_active",
                schema: "organizations",
                table: "organization",
                newName: "IsActive");
        }
    }
}
