using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class rankdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                schema: "ranking",
                table: "x_rank_table",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                schema: "ranking",
                table: "x_rank_table",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                schema: "ranking",
                table: "g_rank_table",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                schema: "ranking",
                table: "g_rank_table",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                schema: "ranking",
                table: "a_rank_table",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_date",
                schema: "ranking",
                table: "a_rank_table",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_date",
                schema: "ranking",
                table: "x_rank_table");

            migrationBuilder.DropColumn(
                name: "modified_date",
                schema: "ranking",
                table: "x_rank_table");

            migrationBuilder.DropColumn(
                name: "created_date",
                schema: "ranking",
                table: "g_rank_table");

            migrationBuilder.DropColumn(
                name: "modified_date",
                schema: "ranking",
                table: "g_rank_table");

            migrationBuilder.DropColumn(
                name: "created_date",
                schema: "ranking",
                table: "a_rank_table");

            migrationBuilder.DropColumn(
                name: "modified_date",
                schema: "ranking",
                table: "a_rank_table");
        }
    }
}
