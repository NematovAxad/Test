using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class fieldexceptiontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exception_fields",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    year = table.Column<int>(nullable: false),
                    quarter = table.Column<int>(nullable: false),
                    sphere_id = table.Column<int>(nullable: false),
                    field_id = table.Column<int>(nullable: false),
                    comment = table.Column<string>(nullable: true),
                    expert_id = table.Column<int>(nullable: false),
                    espert_pinfl = table.Column<string>(nullable: true),
                    created_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exception_fields", x => x.id);
                    table.ForeignKey(
                        name: "FK_exception_fields_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_exception_fields_organization_id",
                schema: "ranking",
                table: "exception_fields",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exception_fields",
                schema: "ranking");
        }
    }
}
