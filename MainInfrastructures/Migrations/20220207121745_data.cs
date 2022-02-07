using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "performance_year",
                schema: "module_regions",
                table: "application");

            migrationBuilder.AddColumn<DateTime>(
                name: "performance_year_end",
                schema: "module_regions",
                table: "application",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "performance_year_start",
                schema: "module_regions",
                table: "application",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "performance_year_end",
                schema: "module_regions",
                table: "application");

            migrationBuilder.DropColumn(
                name: "performance_year_start",
                schema: "module_regions",
                table: "application");

            migrationBuilder.AddColumn<DateTime>(
                name: "performance_year",
                schema: "module_regions",
                table: "application",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
