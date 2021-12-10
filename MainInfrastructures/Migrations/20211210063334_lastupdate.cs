using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class lastupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization_documents",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    document_no = table.Column<string>(nullable: true),
                    document_date = table.Column<DateTime>(nullable: false),
                    document_type = table.Column<int>(nullable: false),
                    document_name = table.Column<string>(nullable: true),
                    main_purpose = table.Column<string>(nullable: true),
                    path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_documents_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "replacer_org_head",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    mid_name = table.Column<string>(nullable: true),
                    position = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    fax = table.Column<string>(nullable: true),
                    file_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_replacer_org_head", x => x.id);
                    table.ForeignKey(
                        name: "FK_replacer_org_head_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_documents_organization_id",
                schema: "organizations",
                table: "organization_documents",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_replacer_org_head_organization_id",
                schema: "organizations",
                table: "replacer_org_head",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_documents",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "replacer_org_head",
                schema: "organizations");
        }
    }
}
