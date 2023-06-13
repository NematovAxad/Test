using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class extracolumnsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "application_problem_text_exspert",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_comment_confirmed_exspert",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_dissatisfaction_confirmed_exspert",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_url",
                schema: "organizations",
                table: "organization_services",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "application_problem_text_exspert",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "service_comment_confirmed_exspert",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "service_dissatisfaction_confirmed_exspert",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "service_url",
                schema: "organizations",
                table: "organization_services");
        }
    }
}
