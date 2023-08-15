using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class lastupdatetimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "sub_organization",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "sub_organization",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "sub_org_statistics",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "sub_org_statistics",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "replacer_org_head",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "replacer_org_head",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_documents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_documents",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "employee_statistics",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "employee_statistics",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "based_documents",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "based_documents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "sub_organization");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "sub_organization");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "sub_org_statistics");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "sub_org_statistics");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "replacer_org_head");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "replacer_org_head");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_documents");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_documents");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "employee_statistics");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "employee_statistics");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "based_documents");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "based_documents");
        }
    }
}
