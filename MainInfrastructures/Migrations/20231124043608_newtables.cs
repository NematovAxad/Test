using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class newtables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "news_on_dashboard",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(nullable: true),
                    body = table.Column<string>(nullable: true),
                    file_link = table.Column<string>(nullable: true),
                    add_date = table.Column<DateTime>(nullable: false),
                    update_date = table.Column<DateTime>(nullable: false),
                    author_pinfl = table.Column<string>(nullable: true),
                    first = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_news_on_dashboard", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "organization_employee_attestats",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_employee_attestats", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_employee_attestats_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_employee_attestats_organization_id",
                schema: "organizations",
                table: "organization_employee_attestats",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "news_on_dashboard",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "organization_employee_attestats",
                schema: "organizations");
        }
    }
}
