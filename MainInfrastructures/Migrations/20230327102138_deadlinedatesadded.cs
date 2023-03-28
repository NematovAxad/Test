using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class deadlinedatesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deadline_date",
                schema: "ranking",
                table: "deadline");

            migrationBuilder.AddColumn<DateTime>(
                name: "fifth_section_deadline_date",
                schema: "ranking",
                table: "deadline",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "second_section_deadline_date",
                schema: "ranking",
                table: "deadline",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "sixth_section_deadline_date",
                schema: "ranking",
                table: "deadline",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "third_section_deadline_date",
                schema: "ranking",
                table: "deadline",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "fifth_section_deadline_date",
                schema: "ranking",
                table: "deadline");

            migrationBuilder.DropColumn(
                name: "second_section_deadline_date",
                schema: "ranking",
                table: "deadline");

            migrationBuilder.DropColumn(
                name: "sixth_section_deadline_date",
                schema: "ranking",
                table: "deadline");

            migrationBuilder.DropColumn(
                name: "third_section_deadline_date",
                schema: "ranking",
                table: "deadline");

            migrationBuilder.AddColumn<DateTime>(
                name: "deadline_date",
                schema: "ranking",
                table: "deadline",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
