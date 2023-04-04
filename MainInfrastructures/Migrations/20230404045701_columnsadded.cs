using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class columnsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "finance_provision_material_document",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ministry_agreed_head_document",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "finance_provision_material_document",
                schema: "organizations",
                table: "organization_ict_special_forces");

            migrationBuilder.DropColumn(
                name: "ministry_agreed_head_document",
                schema: "organizations",
                table: "organization_ict_special_forces");
        }
    }
}
