using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class sixthsection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "org_future_years_strategies",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    document_name = table.Column<string>(nullable: true),
                    document_number = table.Column<string>(nullable: true),
                    approval_date = table.Column<DateTime>(nullable: false),
                    document_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_future_years_strategies", x => x.id);
                    table.ForeignKey(
                        name: "FK_org_future_years_strategies_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_events",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    event_name = table.Column<string>(nullable: true),
                    event_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_events", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_events_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_org_future_years_strategies_organization_id",
                schema: "organizations",
                table: "org_future_years_strategies",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_events_organization_id",
                schema: "organizations",
                table: "organization_events",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "org_future_years_strategies",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "organization_events",
                schema: "organizations");
        }
    }
}
