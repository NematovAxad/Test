using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class updatetimeforlasttables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_servers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_servers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_indicators",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_indicators",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_finance_report",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_finance_report",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_finance",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_finance",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_digital_economy_projects_report",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_digital_economy_projects_report",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_digital_economy_projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_digital_economy_projects",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_computers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_computers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_budget",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_budget",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "indicator_rating",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "indicator_rating",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_servers");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_servers");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_indicators");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_indicators");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_ict_special_forces");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_ict_special_forces");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_finance_report");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_finance_report");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_finance");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_finance");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_digital_economy_projects_report");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_digital_economy_projects_report");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_digital_economy_projects");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_digital_economy_projects");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_computers");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_budget");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_budget");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "indicator_rating");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "indicator_rating");
        }
    }
}
