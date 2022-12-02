using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class tablechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expert_except",
                schema: "reestrprojects",
                table: "reestr_project_identities");

            migrationBuilder.DropColumn(
                name: "expert_except",
                schema: "reestrprojects",
                table: "reestr_project_connection");

            migrationBuilder.DropColumn(
                name: "expert_except",
                schema: "reestrprojects",
                table: "reestr_project_classifications");

            migrationBuilder.AddColumn<int>(
                name: "all_items",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "excepted_items",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "all_items",
                schema: "reestrprojects",
                table: "reestr_project_connection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "excepted_items",
                schema: "reestrprojects",
                table: "reestr_project_connection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "all_items",
                schema: "reestrprojects",
                table: "reestr_project_classifications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "excepted_items",
                schema: "reestrprojects",
                table: "reestr_project_classifications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "all_items",
                schema: "reestrprojects",
                table: "reestr_project_identities");

            migrationBuilder.DropColumn(
                name: "excepted_items",
                schema: "reestrprojects",
                table: "reestr_project_identities");

            migrationBuilder.DropColumn(
                name: "all_items",
                schema: "reestrprojects",
                table: "reestr_project_connection");

            migrationBuilder.DropColumn(
                name: "excepted_items",
                schema: "reestrprojects",
                table: "reestr_project_connection");

            migrationBuilder.DropColumn(
                name: "all_items",
                schema: "reestrprojects",
                table: "reestr_project_classifications");

            migrationBuilder.DropColumn(
                name: "excepted_items",
                schema: "reestrprojects",
                table: "reestr_project_classifications");

            migrationBuilder.AddColumn<bool>(
                name: "expert_except",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "expert_except",
                schema: "reestrprojects",
                table: "reestr_project_connection",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "expert_except",
                schema: "reestrprojects",
                table: "reestr_project_classifications",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
