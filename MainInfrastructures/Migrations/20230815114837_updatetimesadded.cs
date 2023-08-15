using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class updatetimesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "website_requirements",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "website_requirements",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "site_fail_comment",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "site_fail_comment",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_apps",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_apps",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "content_manager",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "content_manager",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "site_fail_comment");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "site_fail_comment");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_apps");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_apps");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "content_manager");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "content_manager");
        }
    }
}
