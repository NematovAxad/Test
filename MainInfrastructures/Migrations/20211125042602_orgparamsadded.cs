using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class orgparamsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address_district",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address_home_no",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address_province",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "address_street",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "department",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "director_first_name",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "director_last_name",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "director_mail",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "director_mid_name",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "director_position",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fax",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "org_mail",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "org_type",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "post_index",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "short_name",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "web_site",
                schema: "organizations",
                table: "organization",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address_district",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "address_home_no",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "address_province",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "address_street",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "department",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "director_first_name",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "director_last_name",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "director_mail",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "director_mid_name",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "director_position",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "fax",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "org_mail",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "org_type",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "phone_number",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "post_index",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "short_name",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "web_site",
                schema: "organizations",
                table: "organization");
        }
    }
}
