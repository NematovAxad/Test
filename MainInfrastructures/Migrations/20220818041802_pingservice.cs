using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class pingservice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "automated_services",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "availability_automation_of_service",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "commissioning_date",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "expert_opinion_date",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "expert_opinion_number",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "has_integration_with_egovernment",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "integrated_central_database",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "integrated_complexes_of_systems",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "integrated_payment_system",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "integrated_register_classifiers",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "integration_interdepartmental_platform",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "list_of_services",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "other_clasifiers",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "payment_system_name",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_appointment",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_condition",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_name",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_reestr_number",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "transmitting_informations_first",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "transmitting_informations_fourth",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "transmitting_informations_second",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "transmitting_informations_third",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "used_clasifiers",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "users_count",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "uses_classifiers",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.AddColumn<bool>(
                name: "ping_service",
                schema: "ranking",
                table: "deadline",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "classifiers_used",
                schema: "organizations",
                table: "org_information_systems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "expert_decision",
                schema: "organizations",
                table: "org_information_systems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "cybersecurity_decision",
                schema: "organizations",
                table: "org_information_systems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "system_basis",
                schema: "organizations",
                table: "org_information_systems",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "system_connections",
                schema: "organizations",
                table: "org_information_systems",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "system_full_name",
                schema: "organizations",
                table: "org_information_systems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "system_id",
                schema: "organizations",
                table: "org_information_systems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "system_link",
                schema: "organizations",
                table: "org_information_systems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "system_purpose",
                schema: "organizations",
                table: "org_information_systems",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "system_short_name",
                schema: "organizations",
                table: "org_information_systems",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "system_status",
                schema: "organizations",
                table: "org_information_systems",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "system_unique_ids",
                schema: "organizations",
                table: "org_information_systems",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ping_service",
                schema: "ranking",
                table: "deadline");

            migrationBuilder.DropColumn(
                name: "classifiers_used",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "expert_decision",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "cybersecurity_decision",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_basis",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_connections",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_full_name",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_id",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_link",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_purpose",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_short_name",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_status",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.DropColumn(
                name: "system_unique_ids",
                schema: "organizations",
                table: "org_information_systems");

            migrationBuilder.AddColumn<string>(
                name: "automated_services",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "availability_automation_of_service",
                schema: "organizations",
                table: "org_information_systems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "commissioning_date",
                schema: "organizations",
                table: "org_information_systems",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "expert_opinion_date",
                schema: "organizations",
                table: "org_information_systems",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "expert_opinion_number",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "has_integration_with_egovernment",
                schema: "organizations",
                table: "org_information_systems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "integrated_central_database",
                schema: "organizations",
                table: "org_information_systems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "integrated_complexes_of_systems",
                schema: "organizations",
                table: "org_information_systems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "integrated_payment_system",
                schema: "organizations",
                table: "org_information_systems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "integrated_register_classifiers",
                schema: "organizations",
                table: "org_information_systems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "integration_interdepartmental_platform",
                schema: "organizations",
                table: "org_information_systems",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "list_of_services",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "other_clasifiers",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "payment_system_name",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "system_appointment",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "system_condition",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "system_name",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "system_reestr_number",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "transmitting_informations_first",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "transmitting_informations_fourth",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "transmitting_informations_second",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "transmitting_informations_third",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "used_clasifiers",
                schema: "organizations",
                table: "org_information_systems",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "users_count",
                schema: "organizations",
                table: "org_information_systems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "uses_classifiers",
                schema: "organizations",
                table: "org_information_systems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
