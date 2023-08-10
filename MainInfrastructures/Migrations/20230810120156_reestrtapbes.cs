using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class reestrtapbes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reestr_project_passport",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    reestr_project_id = table.Column<int>(nullable: false),
                    full_name = table.Column<string>(nullable: true),
                    short_name = table.Column<string>(nullable: true),
                    passport_status = table.Column<int>(nullable: false),
                    has_terms = table.Column<bool>(nullable: false),
                    has_expertise = table.Column<bool>(nullable: false),
                    link_for_system = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reestr_project_passport", x => x.id);
                    table.ForeignKey(
                        name: "FK_reestr_project_passport_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reestr_project_passport_details",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    reestr_project_id = table.Column<int>(nullable: false),
                    full_name = table.Column<string>(nullable: true),
                    short_name = table.Column<string>(nullable: true),
                    passport_status = table.Column<int>(nullable: false),
                    basis_name = table.Column<string>(nullable: true),
                    tasks = table.Column<string>(nullable: true),
                    is_interdepartmental_information_system = table.Column<bool>(nullable: false),
                    cybersecurity_expertise = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reestr_project_passport_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_reestr_project_passport_details_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reestr_project_passport_organization_id",
                schema: "reestrprojects",
                table: "reestr_project_passport",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_reestr_project_passport_details_organization_id",
                schema: "reestrprojects",
                table: "reestr_project_passport_details",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reestr_project_passport",
                schema: "reestrprojects");

            migrationBuilder.DropTable(
                name: "reestr_project_passport_details",
                schema: "reestrprojects");
        }
    }
}
