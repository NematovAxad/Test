using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class reestrprojectposition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "org_comment",
                schema: "organizations",
                table: "reestr_project_position");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "org_comment",
                schema: "organizations",
                table: "reestr_project_position",
                type: "text",
                nullable: true);
        }
    }
}
