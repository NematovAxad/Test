using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class newtableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "requirement1",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement10",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement11",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement12",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement13",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement14",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement15",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement16",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement17",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement18",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement19",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement2",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement20",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement21",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement22",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement23",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement24",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement25",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement26",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement27",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement28",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement29",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement3",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement30",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement31",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement32",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement33",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement34",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement35",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement36",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement37",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement38",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement39",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement4",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement40",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement41",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement42",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement43",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement44",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement45",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement5",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement6",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement7",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement8",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "requirement9",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.AddColumn<string>(
                name: "comment",
                schema: "organizations",
                table: "website_requirements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "organizations",
                table: "website_requirements",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "number",
                schema: "organizations",
                table: "website_requirements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "status",
                schema: "organizations",
                table: "website_requirements",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ScreenLink",
                schema: "organizations",
                table: "website_requirements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "site_link",
                schema: "organizations",
                table: "website_requirements",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "site_requirements_sample",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    number = table.Column<int>(nullable: false),
                    site_link = table.Column<string>(nullable: true),
                    ScreenLink = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_requirements_sample", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "site_requirements_sample",
                schema: "organizations");

            migrationBuilder.DropColumn(
                name: "comment",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "number",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "status",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "ScreenLink",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.DropColumn(
                name: "site_link",
                schema: "organizations",
                table: "website_requirements");

            migrationBuilder.AddColumn<int>(
                name: "requirement1",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement10",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement11",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement12",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement13",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement14",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement15",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement16",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement17",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement18",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement19",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement2",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement20",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement21",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement22",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement23",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement24",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement25",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement26",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement27",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement28",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement29",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement3",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement30",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement31",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement32",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement33",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement34",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement35",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement36",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement37",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement38",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement39",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement4",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement40",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement41",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement42",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement43",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement44",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement45",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement5",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement6",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement7",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement8",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "requirement9",
                schema: "organizations",
                table: "website_requirements",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
