using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class identiutyitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "exist",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "org_comment",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "project_identities",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(nullable: false),
                    identity_type = table.Column<int>(nullable: false),
                    identity_url = table.Column<string>(nullable: true),
                    file_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_identities", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_identities_reestr_project_identities_parent_id",
                        column: x => x.parent_id,
                        principalSchema: "reestrprojects",
                        principalTable: "reestr_project_identities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_identities_parent_id",
                schema: "reestrprojects",
                table: "project_identities",
                column: "parent_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_identities",
                schema: "reestrprojects");

            migrationBuilder.DropColumn(
                name: "exist",
                schema: "reestrprojects",
                table: "reestr_project_identities");

            migrationBuilder.DropColumn(
                name: "org_comment",
                schema: "reestrprojects",
                table: "reestr_project_identities");
        }
    }
}
