using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class orgdigitaldetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization_digital_economy_projects_detail",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    normative_document_number = table.Column<string>(nullable: true),
                    application_number = table.Column<string>(nullable: true),
                    project_index = table.Column<string>(nullable: true),
                    responsibles = table.Column<string>(nullable: true),
                    actions = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_digital_economy_projects_detail", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_digital_economy_projects_detail_organization_o~",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_digital_economy_projects_detail_organization_id",
                schema: "organizations",
                table: "organization_digital_economy_projects_detail",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_digital_economy_projects_detail",
                schema: "organizations");
        }
    }
}
