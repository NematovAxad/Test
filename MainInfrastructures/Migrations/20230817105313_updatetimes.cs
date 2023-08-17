using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class updatetimes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "organization_socials",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "org_helpline",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "org_helpline",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "org_data_filler",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "org_data_filler",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "organizations",
                table: "helpline_info",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "org_data_filler");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "org_data_filler");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "organizations",
                table: "helpline_info");
        }
    }
}
