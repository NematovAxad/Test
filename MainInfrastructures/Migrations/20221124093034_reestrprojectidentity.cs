using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class reestrprojectidentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_link",
                schema: "reestrprojects",
                table: "reestr_project_identities");

            migrationBuilder.DropColumn(
                name: "identity_id",
                schema: "reestrprojects",
                table: "reestr_project_identities");

            migrationBuilder.DropColumn(
                name: "project_name",
                schema: "reestrprojects",
                table: "reestr_project_identities");

            migrationBuilder.DropColumn(
                name: "project_org",
                schema: "reestrprojects",
                table: "reestr_project_identities");

            migrationBuilder.DropColumn(
                name: "screenshot_link",
                schema: "reestrprojects",
                table: "reestr_project_identities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "file_link",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "identity_id",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "project_name",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "project_org",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot_link",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                type: "text",
                nullable: true);
        }
    }
}
