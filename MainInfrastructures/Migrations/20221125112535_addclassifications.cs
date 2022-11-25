using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class addclassifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reestr_project_classifications",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    reestr_project_id = table.Column<int>(nullable: false),
                    exist = table.Column<bool>(nullable: false),
                    org_comment = table.Column<string>(nullable: true),
                    expert_except = table.Column<bool>(nullable: false),
                    expert_comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reestr_project_classifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_reestr_project_classifications_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project_classifications",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(nullable: false),
                    classification_type = table.Column<int>(nullable: false),
                    classification_uri = table.Column<string>(nullable: true),
                    file_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_classifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_classifications_reestr_project_classifications_pare~",
                        column: x => x.parent_id,
                        principalSchema: "reestrprojects",
                        principalTable: "reestr_project_classifications",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_classifications_parent_id",
                schema: "reestrprojects",
                table: "project_classifications",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_reestr_project_classifications_organization_id",
                schema: "reestrprojects",
                table: "reestr_project_classifications",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_classifications",
                schema: "reestrprojects");

            migrationBuilder.DropTable(
                name: "reestr_project_classifications",
                schema: "reestrprojects");
        }
    }
}
