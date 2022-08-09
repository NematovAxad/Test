using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class tablechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_main",
                schema: "organizations",
                table: "organization_socials",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "links_to_other_socials",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "org_email",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "org_full_name",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "org_legal_address",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "org_legal_site",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "org_phone",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "pool",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "post1",
                schema: "organizations",
                table: "organization_socials",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "post1_link",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "post2",
                schema: "organizations",
                table: "organization_socials",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "post2_link",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "post3",
                schema: "organizations",
                table: "organization_socials",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "post3_link",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "post4",
                schema: "organizations",
                table: "organization_socials",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "post4_link",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "post5",
                schema: "organizations",
                table: "organization_socials",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "post5_link",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "syncronized_posts",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_main",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "links_to_other_socials",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "org_email",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "org_full_name",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "org_legal_address",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "org_legal_site",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "org_phone",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "pool",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "post1",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "post1_link",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "post2",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "post2_link",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "post3",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "post3_link",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "post4",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "post4_link",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "post5",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "post5_link",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "syncronized_posts",
                schema: "organizations",
                table: "organization_socials");
        }
    }
}
