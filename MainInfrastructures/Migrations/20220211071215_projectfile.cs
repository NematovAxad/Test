using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class projectfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_file_stage_stages_id",
                schema: "module_regions",
                table: "file");

            migrationBuilder.DropPrimaryKey(
                name: "PK_file",
                schema: "module_regions",
                table: "file");

            migrationBuilder.DropColumn(
                name: "user_name",
                schema: "module_regions",
                table: "file");

            migrationBuilder.RenameTable(
                name: "file",
                schema: "module_regions",
                newName: "file_stage",
                newSchema: "module_regions");

            migrationBuilder.RenameIndex(
                name: "IX_file_stages_id",
                schema: "module_regions",
                table: "file_stage",
                newName: "IX_file_stage_stages_id");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                schema: "module_regions",
                table: "file_stage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_file_stage",
                schema: "module_regions",
                table: "file_stage",
                column: "id");

            migrationBuilder.CreateTable(
                name: "file_project",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    path = table.Column<string>(nullable: true),
                    user_id = table.Column<int>(nullable: false),
                    file_save_date = table.Column<DateTime>(nullable: false),
                    project_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file_project", x => x.id);
                    table.ForeignKey(
                        name: "FK_file_project_project_project_id",
                        column: x => x.project_id,
                        principalSchema: "module_regions",
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_file_project_project_id",
                schema: "module_regions",
                table: "file_project",
                column: "project_id");

            migrationBuilder.AddForeignKey(
                name: "FK_file_stage_stage_stages_id",
                schema: "module_regions",
                table: "file_stage",
                column: "stages_id",
                principalSchema: "module_regions",
                principalTable: "stage",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_file_stage_stage_stages_id",
                schema: "module_regions",
                table: "file_stage");

            migrationBuilder.DropTable(
                name: "file_project",
                schema: "module_regions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_file_stage",
                schema: "module_regions",
                table: "file_stage");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "module_regions",
                table: "file_stage");

            migrationBuilder.RenameTable(
                name: "file_stage",
                schema: "module_regions",
                newName: "file",
                newSchema: "module_regions");

            migrationBuilder.RenameIndex(
                name: "IX_file_stage_stages_id",
                schema: "module_regions",
                table: "file",
                newName: "IX_file_stages_id");

            migrationBuilder.AddColumn<string>(
                name: "user_name",
                schema: "module_regions",
                table: "file",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_file",
                schema: "module_regions",
                table: "file",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_file_stage_stages_id",
                schema: "module_regions",
                table: "file",
                column: "stages_id",
                principalSchema: "module_regions",
                principalTable: "stage",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
