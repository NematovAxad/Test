using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class newtabadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "reestr_project_automated_services",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    reestr_project_id = table.Column<int>(nullable: false),
                    project_service_exist = table.Column<bool>(nullable: false),
                    all_items = table.Column<int>(nullable: false),
                    excepted_items = table.Column<int>(nullable: false),
                    expert_comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reestr_project_automated_services", x => x.id);
                    table.ForeignKey(
                        name: "FK_reestr_project_automated_services_organization_organization~",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "automated_functions",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(nullable: false),
                    function_name = table.Column<string>(nullable: true),
                    file_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_automated_functions", x => x.id);
                    table.ForeignKey(
                        name: "FK_automated_functions_reestr_project_automated_services_paren~",
                        column: x => x.parent_id,
                        principalSchema: "reestrprojects",
                        principalTable: "reestr_project_automated_services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "automated_services",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(nullable: false),
                    service_name = table.Column<string>(nullable: true),
                    file_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_automated_services", x => x.id);
                    table.ForeignKey(
                        name: "FK_automated_services_reestr_project_automated_services_parent~",
                        column: x => x.parent_id,
                        principalSchema: "reestrprojects",
                        principalTable: "reestr_project_automated_services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_automated_functions_parent_id",
                schema: "reestrprojects",
                table: "automated_functions",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_automated_services_parent_id",
                schema: "reestrprojects",
                table: "automated_services",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_reestr_project_automated_services_organization_id",
                schema: "reestrprojects",
                table: "reestr_project_automated_services",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "automated_functions",
                schema: "reestrprojects");

            migrationBuilder.DropTable(
                name: "automated_services",
                schema: "reestrprojects");

            migrationBuilder.DropTable(
                name: "reestr_project_automated_services",
                schema: "reestrprojects");
        }
    }
}
