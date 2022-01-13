using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class rulesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "g_rank_rules",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    sphere_id = table.Column<int>(nullable: false),
                    field_id = table.Column<int>(nullable: false),
                    sub_field_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    rank = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_g_rank_rules", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "x_rank_rules",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    sphere_id = table.Column<int>(nullable: false),
                    field_id = table.Column<int>(nullable: false),
                    sub_field_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    rank = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_x_rank_rules", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "g_rank_rules",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "x_rank_rules",
                schema: "ranking");
        }
    }
}
