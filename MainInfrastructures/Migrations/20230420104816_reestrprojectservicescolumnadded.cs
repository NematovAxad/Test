using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class reestrprojectservicescolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "project_functions_exist",
                schema: "reestrprojects",
                table: "reestr_project_automated_services",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "project_functions_exist",
                schema: "reestrprojects",
                table: "reestr_project_automated_services");
        }
    }
}
