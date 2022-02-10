using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class financiers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "project_financiers",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<int>(nullable: false),
                    financier_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_financiers", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_financiers_financier_financier_id",
                        column: x => x.financier_id,
                        principalSchema: "module_regions",
                        principalTable: "financier",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_financiers_project_project_id",
                        column: x => x.project_id,
                        principalSchema: "module_regions",
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_financiers_financier_id",
                schema: "module_regions",
                table: "project_financiers",
                column: "financier_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_financiers_project_id",
                schema: "module_regions",
                table: "project_financiers",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_financiers",
                schema: "module_regions");
        }
    }
}
