using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class newtablesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization_services",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    service_name_uz = table.Column<string>(nullable: true),
                    service_name_ru = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_services", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_services_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_services_rate",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    service_id = table.Column<int>(nullable: false),
                    application_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_services_rate", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_services_rate_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organization_services_rate_organization_services_service_id",
                        column: x => x.service_id,
                        principalSchema: "organizations",
                        principalTable: "organization_services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_services_organization_id",
                schema: "organizations",
                table: "organization_services",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_services_rate_organization_id",
                schema: "organizations",
                table: "organization_services_rate",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_services_rate_service_id",
                schema: "organizations",
                table: "organization_services_rate",
                column: "service_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_services_rate",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "organization_services",
                schema: "organizations");
        }
    }
}
