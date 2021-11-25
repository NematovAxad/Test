using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class baseddocs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "based_documents",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    document_no = table.Column<string>(nullable: true),
                    document_date = table.Column<DateTime>(nullable: false),
                    document_type = table.Column<int>(nullable: false),
                    accepted_org = table.Column<int>(nullable: false),
                    document_name = table.Column<string>(nullable: true),
                    path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_based_documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_based_documents_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_based_documents_organization_id",
                schema: "organizations",
                table: "based_documents",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "based_documents",
                schema: "organizations");
        }
    }
}
