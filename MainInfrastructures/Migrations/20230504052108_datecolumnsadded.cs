using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class datecolumnsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "rate_add_date",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "rate_update_date",
                schema: "organizations",
                table: "organization_services_rate",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rate_add_date",
                schema: "organizations",
                table: "organization_services_rate");

            migrationBuilder.DropColumn(
                name: "rate_update_date",
                schema: "organizations",
                table: "organization_services_rate");
        }
    }
}
