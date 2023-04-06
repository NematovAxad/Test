using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class digitaleconomyprojects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "project_stage",
                schema: "organizations",
                table: "organization_digital_economy_projects");

            migrationBuilder.CreateTable(
                name: "organization_digital_economy_projects_report",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    projects_count = table.Column<int>(nullable: false),
                    completed_projects = table.Column<int>(nullable: false),
                    ongoing_plojects = table.Column<int>(nullable: false),
                    not_finished_projects = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_digital_economy_projects_report", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_digital_economy_projects_report_organization_o~",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_digital_economy_projects_report_organization_id",
                schema: "organizations",
                table: "organization_digital_economy_projects_report",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_digital_economy_projects_report",
                schema: "organizations");

            migrationBuilder.AddColumn<int>(
                name: "project_stage",
                schema: "organizations",
                table: "organization_digital_economy_projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
