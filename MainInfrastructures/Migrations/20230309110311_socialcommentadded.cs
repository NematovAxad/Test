using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class socialcommentadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_social_parameters",
                schema: "organizations");

            migrationBuilder.AddColumn<string>(
                name: "comment_to_social_site",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comment_to_social_site",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.CreateTable(
                name: "organization_social_parameters",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    deadline_id = table.Column<int>(type: "integer", nullable: false),
                    links_to_other_socials = table.Column<bool>(type: "boolean", nullable: true),
                    org_email = table.Column<bool>(type: "boolean", nullable: true),
                    org_full_name = table.Column<bool>(type: "boolean", nullable: true),
                    org_legal_address = table.Column<bool>(type: "boolean", nullable: true),
                    org_legal_site = table.Column<bool>(type: "boolean", nullable: true),
                    org_phone = table.Column<bool>(type: "boolean", nullable: true),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    pool = table.Column<bool>(type: "boolean", nullable: true),
                    syncronized_posts = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_social_parameters", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_social_parameters_deadline_deadline_id",
                        column: x => x.deadline_id,
                        principalSchema: "ranking",
                        principalTable: "deadline",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organization_social_parameters_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_social_parameters_deadline_id",
                schema: "organizations",
                table: "organization_social_parameters",
                column: "deadline_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_social_parameters_organization_id",
                schema: "organizations",
                table: "organization_social_parameters",
                column: "organization_id");
        }
    }
}
