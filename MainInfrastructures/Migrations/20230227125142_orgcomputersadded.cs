using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class orgcomputersadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "central_connected_project_appeal",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "central_connected_project_resolution",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "connected_project_appeal",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "connected_project_resolution",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_all_cmputers",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_connected_corporate_network",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_connected_eijro",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_connected_exat",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_connected_local_set",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_connected_network",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_connected_project_appeal",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_connected_project_gov",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_connected_project_my_work",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_connected_project_resolution",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "devicions_working_cmputers",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "subordinate_connected_project_appeal",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "subordinate_connected_project_resolution",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "territorial_connected_project_appeal",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "territorial_connected_project_resolution",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "central_connected_project_appeal",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "central_connected_project_resolution",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "connected_project_appeal",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "connected_project_resolution",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_all_cmputers",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_connected_corporate_network",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_connected_eijro",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_connected_exat",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_connected_local_set",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_connected_network",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_connected_project_appeal",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_connected_project_gov",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_connected_project_my_work",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_connected_project_resolution",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "devicions_working_cmputers",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "subordinate_connected_project_appeal",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "subordinate_connected_project_resolution",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "territorial_connected_project_appeal",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "territorial_connected_project_resolution",
                schema: "organizations",
                table: "organization_computers");
        }
    }
}
