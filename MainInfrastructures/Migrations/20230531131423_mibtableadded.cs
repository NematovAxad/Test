using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class mibtableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "org_inn",
                schema: "organizations",
                table: "organization",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mygov_report",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    api_name = table.Column<string>(nullable: true),
                    owner_inn = table.Column<string>(nullable: true),
                    api_description = table.Column<string>(nullable: true),
                    api_version = table.Column<string>(nullable: true),
                    success_count = table.Column<int>(nullable: false),
                    fail_count = table.Column<int>(nullable: false),
                    overall = table.Column<string>(nullable: true),
                    success_share = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mygov_report", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mygov_report",
                schema: "organizations");

            migrationBuilder.DropColumn(
                name: "org_inn",
                schema: "organizations",
                table: "organization");
        }
    }
}
