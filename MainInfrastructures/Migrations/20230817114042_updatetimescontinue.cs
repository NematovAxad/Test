using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class updatetimescontinue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_public_services",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_public_services",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "mygov_reports",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "mib_report",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_public_services");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "mygov_reports");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "mib_report");
        }
    }
}
