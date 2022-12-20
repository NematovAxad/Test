using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class tableaddednew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reestr_project_authorizations",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    reestr_project_id = table.Column<int>(nullable: false),
                    exist = table.Column<bool>(nullable: false),
                    org_comment = table.Column<string>(nullable: true),
                    all_items = table.Column<int>(nullable: false),
                    excepted_items = table.Column<int>(nullable: false),
                    expert_comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reestr_project_authorizations", x => x.id);
                    table.ForeignKey(
                        name: "FK_reestr_project_authorizations_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_authorizations",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(nullable: false),
                    authorization_type = table.Column<int>(nullable: false),
                    authorization_uri = table.Column<string>(nullable: true),
                    file_path = table.Column<string>(nullable: true),
                    ReestrProjectAuthorizationsId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_authorizations", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_authorizations_reestr_project_classifications_paren~",
                        column: x => x.parent_id,
                        principalSchema: "reestrprojects",
                        principalTable: "reestr_project_classifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_authorizations_reestr_project_authorizations_Reestr~",
                        column: x => x.ReestrProjectAuthorizationsId,
                        principalSchema: "reestrprojects",
                        principalTable: "reestr_project_authorizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_authorizations_parent_id",
                schema: "reestrprojects",
                table: "project_authorizations",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_authorizations_ReestrProjectAuthorizationsId",
                schema: "reestrprojects",
                table: "project_authorizations",
                column: "ReestrProjectAuthorizationsId");

            migrationBuilder.CreateIndex(
                name: "IX_reestr_project_authorizations_organization_id",
                schema: "reestrprojects",
                table: "reestr_project_authorizations",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_authorizations",
                schema: "reestrprojects");

            migrationBuilder.DropTable(
                name: "reestr_project_authorizations",
                schema: "reestrprojects");
        }
    }
}
