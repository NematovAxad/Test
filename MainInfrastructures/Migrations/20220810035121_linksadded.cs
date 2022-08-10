using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class linksadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "link1",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "link2",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "link3",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "link4",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "link5",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link1",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "link2",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "link3",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "link4",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "link5",
                schema: "organizations",
                table: "organization_socials");
        }
    }
}
