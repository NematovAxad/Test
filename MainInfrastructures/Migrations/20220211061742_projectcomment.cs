using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class projectcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "project_comment",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    text = table.Column<string>(nullable: true),
                    user_id = table.Column<int>(nullable: false),
                    user_role = table.Column<string>(nullable: true),
                    action = table.Column<string>(nullable: true),
                    user_name = table.Column<string>(nullable: true),
                    date_comment = table.Column<DateTime>(nullable: false),
                    project_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_comment_project_project_id",
                        column: x => x.project_id,
                        principalSchema: "module_regions",
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_comment_project_id",
                schema: "module_regions",
                table: "project_comment",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_comment",
                schema: "module_regions");
        }
    }
}
