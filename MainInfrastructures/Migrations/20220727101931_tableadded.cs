using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class tableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "site_fails",
                schema: "organizations");

            migrationBuilder.CreateTable(
                name: "site_fails_table",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    deadline_id = table.Column<int>(nullable: false),
                    website = table.Column<string>(nullable: true),
                    failed_time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_fails_table", x => x.id);
                    table.ForeignKey(
                        name: "FK_site_fails_table_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_site_fails_table_organization_id",
                schema: "organizations",
                table: "site_fails_table",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "site_fails_table",
                schema: "organizations");

            migrationBuilder.CreateTable(
                name: "site_fails",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    deadline_id = table.Column<int>(type: "integer", nullable: false),
                    failed_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    website = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_site_fails", x => x.id);
                    table.ForeignKey(
                        name: "FK_site_fails_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_site_fails_organization_id",
                schema: "organizations",
                table: "site_fails",
                column: "organization_id");
        }
    }
}
