using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class reestrprojectconnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reestrprojects");

            migrationBuilder.RenameTable(
                name: "reestr_project_position",
                schema: "organizations",
                newName: "reestr_project_position",
                newSchema: "reestrprojects");

            migrationBuilder.RenameTable(
                name: "reestr_project_identities",
                schema: "organizations",
                newName: "reestr_project_identities",
                newSchema: "reestrprojects");

            migrationBuilder.CreateTable(
                name: "reestr_project_connection",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    reestr_project_id = table.Column<int>(nullable: false),
                    org_comment = table.Column<string>(nullable: true),
                    expert_except = table.Column<bool>(nullable: false),
                    expert_comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reestr_project_connection", x => x.id);
                    table.ForeignKey(
                        name: "FK_reestr_project_connection_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reestr_project_connection_organization_id",
                schema: "reestrprojects",
                table: "reestr_project_connection",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reestr_project_connection",
                schema: "reestrprojects");

            migrationBuilder.RenameTable(
                name: "reestr_project_position",
                schema: "reestrprojects",
                newName: "reestr_project_position",
                newSchema: "organizations");

            migrationBuilder.RenameTable(
                name: "reestr_project_identities",
                schema: "reestrprojects",
                newName: "reestr_project_identities",
                newSchema: "organizations");
        }
    }
}
