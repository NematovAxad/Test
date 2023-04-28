using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class tablefilled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "application_problem_confirmed",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "application_problem_text",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expert_comment",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_application_problem",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "not_recommendation_comment",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "recommend_service",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "service_comment",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "service_comment_confirmed",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "service_comment_type",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "service_dissatisfaction_confirmed",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "service_dissatisfaction_reason",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "service_rate",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "service_satisfactive",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "application_problem_confirmed",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "application_problem_text",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "expert_comment",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "has_application_problem",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "not_recommendation_comment",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "recommend_service",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "service_comment",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "service_comment_confirmed",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "service_comment_type",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "service_dissatisfaction_confirmed",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "service_dissatisfaction_reason",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "service_rate",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "service_satisfactive",
                schema: "organizations",
                table: "organization_services_rate");
        }
    }
}
