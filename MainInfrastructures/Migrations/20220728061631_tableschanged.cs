using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class tableschanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "org_social_sites",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "organization_messengers",
                schema: "organizations");

            migrationBuilder.CreateTable(
                name: "organization_social_parameters",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    deadline_id = table.Column<int>(nullable: false),
                    org_full_name = table.Column<bool>(nullable: true),
                    org_legal_site = table.Column<bool>(nullable: true),
                    org_phone = table.Column<bool>(nullable: true),
                    org_legal_address = table.Column<bool>(nullable: true),
                    org_email = table.Column<bool>(nullable: true),
                    links_to_other_socials = table.Column<bool>(nullable: true),
                    syncronized_posts = table.Column<bool>(nullable: true),
                    pool = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_social_parameters", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_social_parameters_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_socials",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    messenger_link = table.Column<string>(nullable: true),
                    verified = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_socials", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_socials_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_social_parameters_organization_id",
                schema: "organizations",
                table: "organization_social_parameters",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_socials_organization_id",
                schema: "organizations",
                table: "organization_socials",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_social_parameters",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "organization_socials",
                schema: "organizations");

            migrationBuilder.CreateTable(
                name: "org_social_sites",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    social_site_link = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_social_sites", x => x.id);
                    table.ForeignKey(
                        name: "FK_org_social_sites_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_messengers",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    messenger_link = table.Column<string>(type: "text", nullable: true),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    reason_not_filling = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_messengers", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_messengers_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_org_social_sites_organization_id",
                schema: "organizations",
                table: "org_social_sites",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_messengers_organization_id",
                schema: "organizations",
                table: "organization_messengers",
                column: "organization_id");
        }
    }
}
