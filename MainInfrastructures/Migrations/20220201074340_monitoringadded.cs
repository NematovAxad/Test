using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class monitoringadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "module_regions");

            migrationBuilder.CreateTable(
                name: "normative_legal_document",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    number = table.Column<int>(nullable: false),
                    name_uz = table.Column<string>(nullable: true),
                    name_ru = table.Column<string>(nullable: true),
                    approved_date = table.Column<DateTime>(nullable: false),
                    document_type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_normative_legal_document", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "performencer",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_performencer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "application",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_uz = table.Column<string>(nullable: true),
                    name_ru = table.Column<string>(nullable: true),
                    short_name = table.Column<string>(nullable: true),
                    performance_year = table.Column<DateTime>(nullable: false),
                    normative_legal_document_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application", x => x.id);
                    table.ForeignKey(
                        name: "FK_application_normative_legal_document_normative_legal_docume~",
                        column: x => x.normative_legal_document_id,
                        principalSchema: "module_regions",
                        principalTable: "normative_legal_document",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "project",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_uz = table.Column<string>(nullable: true),
                    name_ru = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false),
                    project_purpose = table.Column<string>(nullable: true),
                    cost_effective = table.Column<string>(nullable: true),
                    problem = table.Column<string>(nullable: true),
                    start_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: false),
                    volume_forecast_funds = table.Column<double>(nullable: false),
                    raised_funds = table.Column<double>(nullable: false),
                    payouts = table.Column<double>(nullable: false),
                    performencer_id = table.Column<int>(nullable: false),
                    application_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_application_application_id",
                        column: x => x.application_id,
                        principalSchema: "module_regions",
                        principalTable: "application",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_project_performencer_performencer_id",
                        column: x => x.performencer_id,
                        principalSchema: "module_regions",
                        principalTable: "performencer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cooworkers",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<int>(nullable: false),
                    performencer_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cooworkers", x => x.id);
                    table.ForeignKey(
                        name: "FK_cooworkers_performencer_performencer_id",
                        column: x => x.performencer_id,
                        principalSchema: "module_regions",
                        principalTable: "performencer",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cooworkers_project_project_id",
                        column: x => x.project_id,
                        principalSchema: "module_regions",
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "financier",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_uz = table.Column<string>(nullable: true),
                    name_ru = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financier", x => x.id);
                    table.ForeignKey(
                        name: "FK_financier_project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "module_regions",
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stage",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    start_date = table.Column<DateTime>(nullable: false),
                    end_date = table.Column<DateTime>(nullable: false),
                    stage_status = table.Column<int>(nullable: false),
                    project_id = table.Column<int>(nullable: false),
                    creation_user_id = table.Column<int>(nullable: false),
                    creation_username = table.Column<string>(nullable: true),
                    creation_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stage", x => x.id);
                    table.ForeignKey(
                        name: "FK_stage_project_project_id",
                        column: x => x.project_id,
                        principalSchema: "module_regions",
                        principalTable: "project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    text = table.Column<string>(nullable: true),
                    user_id = table.Column<int>(nullable: false),
                    user_name = table.Column<string>(nullable: true),
                    date_comment = table.Column<DateTime>(nullable: false),
                    stages_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_comment_stage_stages_id",
                        column: x => x.stages_id,
                        principalSchema: "module_regions",
                        principalTable: "stage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "file",
                schema: "module_regions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true),
                    path = table.Column<string>(nullable: true),
                    user_name = table.Column<string>(nullable: true),
                    file_save_date = table.Column<DateTime>(nullable: false),
                    stages_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_file", x => x.id);
                    table.ForeignKey(
                        name: "FK_file_stage_stages_id",
                        column: x => x.stages_id,
                        principalSchema: "module_regions",
                        principalTable: "stage",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_application_normative_legal_document_id",
                schema: "module_regions",
                table: "application",
                column: "normative_legal_document_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_stages_id",
                schema: "module_regions",
                table: "comment",
                column: "stages_id");

            migrationBuilder.CreateIndex(
                name: "IX_cooworkers_performencer_id",
                schema: "module_regions",
                table: "cooworkers",
                column: "performencer_id");

            migrationBuilder.CreateIndex(
                name: "IX_cooworkers_project_id",
                schema: "module_regions",
                table: "cooworkers",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_file_stages_id",
                schema: "module_regions",
                table: "file",
                column: "stages_id");

            migrationBuilder.CreateIndex(
                name: "IX_financier_ProjectId",
                schema: "module_regions",
                table: "financier",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_project_application_id",
                schema: "module_regions",
                table: "project",
                column: "application_id");

            migrationBuilder.CreateIndex(
                name: "IX_project_performencer_id",
                schema: "module_regions",
                table: "project",
                column: "performencer_id");

            migrationBuilder.CreateIndex(
                name: "IX_stage_project_id",
                schema: "module_regions",
                table: "stage",
                column: "project_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment",
                schema: "module_regions");

            migrationBuilder.DropTable(
                name: "cooworkers",
                schema: "module_regions");

            migrationBuilder.DropTable(
                name: "file",
                schema: "module_regions");

            migrationBuilder.DropTable(
                name: "financier",
                schema: "module_regions");

            migrationBuilder.DropTable(
                name: "stage",
                schema: "module_regions");

            migrationBuilder.DropTable(
                name: "project",
                schema: "module_regions");

            migrationBuilder.DropTable(
                name: "application",
                schema: "module_regions");

            migrationBuilder.DropTable(
                name: "performencer",
                schema: "module_regions");

            migrationBuilder.DropTable(
                name: "normative_legal_document",
                schema: "module_regions");
        }
    }
}
