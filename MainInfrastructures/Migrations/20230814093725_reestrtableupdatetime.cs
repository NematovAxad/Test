using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class reestrtableupdatetime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "update_time",
                schema: "reestrprojects",
                table: "reestr_project_passport_details",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "update_time",
                schema: "reestrprojects",
                table: "reestr_project_passport",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "update_time",
                schema: "reestrprojects",
                table: "reestr_project_passport_details");

            migrationBuilder.DropColumn(
                name: "update_time",
                schema: "reestrprojects",
                table: "reestr_project_passport");
        }
    }
}
