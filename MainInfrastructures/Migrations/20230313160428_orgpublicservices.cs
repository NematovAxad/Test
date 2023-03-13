using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class orgpublicservices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_paid",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "mechanizm_for_tracking_progress",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "mobile_app",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "other_resources",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "portal_link",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "reglament_path",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "reglament_updated",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "rendering_form",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_name",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_other_result",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_result",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "tracking_progress_by",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "user_types",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.AddColumn<string>(
                name: "app_link",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "app_name",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "app_screenshot",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mygov_link",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "mygov_screenshot_link",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "mygov_service",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "mygov_service_expert",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "mygov_service_expert_commetn",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "other_apps",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "other_apps_expert",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "other_apps_expert_commetn",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "paid_for",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "service_based_document_date",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "service_based_document_name",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_based_document_number",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "service_based_document_type",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "service_complete_period",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "service_complete_period_type",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "service_has_reglament",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "service_has_reglament_expert",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "service_has_reglament_expert_comment",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "service_has_update_reglament",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "service_has_update_reglament_expert",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "service_has_update_reglament_expert_comment",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_name_ru",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_name_uz",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "service_price",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "service_price_comment",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_reglament_comment",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_reglament_path",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_screenshot_link",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "service_subject",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "service_type",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "service_type_expert",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "service_type_expert_comment",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_update_reglament_comment",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_update_reglament_path",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "app_link",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "app_name",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "app_screenshot",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "mygov_link",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "mygov_screenshot_link",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "mygov_service",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "mygov_service_expert",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "mygov_service_expert_commetn",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "other_apps",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "other_apps_expert",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "other_apps_expert_commetn",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "paid_for",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_based_document_date",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_based_document_name",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_based_document_number",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_based_document_type",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_complete_period",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_complete_period_type",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_has_reglament",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_has_reglament_expert",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_has_reglament_expert_comment",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_has_update_reglament",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_has_update_reglament_expert",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_has_update_reglament_expert_comment",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_name_ru",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_name_uz",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_price",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_price_comment",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_reglament_comment",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_reglament_path",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_screenshot_link",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_subject",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_type",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_type_expert",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_type_expert_comment",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_update_reglament_comment",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "service_update_reglament_path",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.AddColumn<bool>(
                name: "is_paid",
                schema: "organizations",
                table: "organization_public_services",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "mechanizm_for_tracking_progress",
                schema: "organizations",
                table: "organization_public_services",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "mobile_app",
                schema: "organizations",
                table: "organization_public_services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "other_resources",
                schema: "organizations",
                table: "organization_public_services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "portal_link",
                schema: "organizations",
                table: "organization_public_services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "reglament_path",
                schema: "organizations",
                table: "organization_public_services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "reglament_updated",
                schema: "organizations",
                table: "organization_public_services",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "rendering_form",
                schema: "organizations",
                table: "organization_public_services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_name",
                schema: "organizations",
                table: "organization_public_services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_other_result",
                schema: "organizations",
                table: "organization_public_services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_result",
                schema: "organizations",
                table: "organization_public_services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tracking_progress_by",
                schema: "organizations",
                table: "organization_public_services",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_types",
                schema: "organizations",
                table: "organization_public_services",
                type: "text",
                nullable: true);
        }
    }
}
