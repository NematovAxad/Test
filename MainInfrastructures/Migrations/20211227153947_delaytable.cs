using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class delaytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "delays_on_projects",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    project_name = table.Column<string>(nullable: true),
                    project_document_number = table.Column<string>(nullable: true),
                    project_document_date = table.Column<DateTime>(nullable: false),
                    project_applying_mechanism = table.Column<string>(nullable: true),
                    project_applying_date = table.Column<DateTime>(nullable: false),
                    project_financing_source = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_delays_on_projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_delays_on_projects_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_delays_on_projects_organization_id",
                schema: "organizations",
                table: "delays_on_projects",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "delays_on_projects",
                schema: "organizations");
        }
    }
}
