using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class projectefficiency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reestr_project_efficiency",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    reestr_project_id = table.Column<int>(nullable: false),
                    efficiency_type = table.Column<int>(nullable: false),
                    file_path = table.Column<string>(nullable: true),
                    org_comment = table.Column<string>(nullable: true),
                    all_items = table.Column<int>(nullable: false),
                    excepted_items = table.Column<int>(nullable: false),
                    expert_comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reestr_project_efficiency", x => x.id);
                    table.ForeignKey(
                        name: "FK_reestr_project_efficiency_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reestr_project_efficiency_organization_id",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reestr_project_efficiency",
                schema: "reestrprojects");
        }
    }
}
