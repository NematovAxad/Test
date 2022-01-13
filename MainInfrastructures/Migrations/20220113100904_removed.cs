using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class removed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "g_rank_rules",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "x_rank_rules",
                schema: "ranking");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "g_rank_rules",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    field_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    rank = table.Column<double>(type: "double precision", nullable: false),
                    sphere_id = table.Column<int>(type: "integer", nullable: false),
                    sub_field_id = table.Column<int>(type: "integer", nullable: false)
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
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    field_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    rank = table.Column<double>(type: "double precision", nullable: false),
                    sphere_id = table.Column<int>(type: "integer", nullable: false),
                    sub_field_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_x_rank_rules", x => x.id);
                });
        }
    }
}
