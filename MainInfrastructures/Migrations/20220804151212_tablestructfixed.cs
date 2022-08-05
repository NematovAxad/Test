using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class tablestructfixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScreenLink",
                schema: "organizations",
                table: "site_requirements_sample");

            migrationBuilder.DropColumn(
                name: "site_link",
                schema: "organizations",
                table: "site_requirements_sample");

            migrationBuilder.AddColumn<string>(
                name: "screen_link_1",
                schema: "organizations",
                table: "site_requirements_sample",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screen_link_2",
                schema: "organizations",
                table: "site_requirements_sample",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screen_link_3",
                schema: "organizations",
                table: "site_requirements_sample",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "site_link_1",
                schema: "organizations",
                table: "site_requirements_sample",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "site_link_2",
                schema: "organizations",
                table: "site_requirements_sample",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "site_link_3",
                schema: "organizations",
                table: "site_requirements_sample",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "screen_link_1",
                schema: "organizations",
                table: "site_requirements_sample");

            migrationBuilder.DropColumn(
                name: "screen_link_2",
                schema: "organizations",
                table: "site_requirements_sample");

            migrationBuilder.DropColumn(
                name: "screen_link_3",
                schema: "organizations",
                table: "site_requirements_sample");

            migrationBuilder.DropColumn(
                name: "site_link_1",
                schema: "organizations",
                table: "site_requirements_sample");

            migrationBuilder.DropColumn(
                name: "site_link_2",
                schema: "organizations",
                table: "site_requirements_sample");

            migrationBuilder.DropColumn(
                name: "site_link_3",
                schema: "organizations",
                table: "site_requirements_sample");

            migrationBuilder.AddColumn<string>(
                name: "ScreenLink",
                schema: "organizations",
                table: "site_requirements_sample",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "site_link",
                schema: "organizations",
                table: "site_requirements_sample",
                type: "text",
                nullable: true);
        }
    }
}
