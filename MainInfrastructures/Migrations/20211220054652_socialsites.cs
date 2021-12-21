using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class socialsites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "org_social_sites",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    field_id = table.Column<int>(nullable: false),
                    deadline_id = table.Column<int>(nullable: false),
                    social_site_link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_social_sites", x => x.id);
                    table.ForeignKey(
                        name: "FK_org_social_sites_deadline_deadline_id",
                        column: x => x.deadline_id,
                        principalSchema: "ranking",
                        principalTable: "deadline",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_org_social_sites_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_org_social_sites_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_org_social_sites_deadline_id",
                schema: "organizations",
                table: "org_social_sites",
                column: "deadline_id");

            migrationBuilder.CreateIndex(
                name: "IX_org_social_sites_field_id",
                schema: "organizations",
                table: "org_social_sites",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_org_social_sites_organization_id",
                schema: "organizations",
                table: "org_social_sites",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "org_social_sites",
                schema: "organizations");
        }
    }
}
