using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class ranktableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ranking");

            migrationBuilder.CreateTable(
                name: "deadline",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    year = table.Column<int>(nullable: false),
                    quarter = table.Column<int>(nullable: false),
                    deadline_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deadline", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "rank_table",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    year = table.Column<int>(nullable: false),
                    quarter = table.Column<int>(nullable: false),
                    rank_field_id = table.Column<int>(nullable: false),
                    rank = table.Column<double>(nullable: false),
                    is_exception = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rank_table", x => x.id);
                    table.ForeignKey(
                        name: "FK_rank_table_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rank_table_organization_id",
                schema: "ranking",
                table: "rank_table",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deadline",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "rank_table",
                schema: "ranking");
        }
    }
}
