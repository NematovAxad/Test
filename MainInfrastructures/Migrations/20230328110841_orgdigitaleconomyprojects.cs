using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class orgdigitaleconomyprojects : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "is_filled_table",
                schema: "ranking");

           

            migrationBuilder.CreateTable(
                name: "organization_digital_economy_projects",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    project_name = table.Column<string>(nullable: true),
                    basis_file_path = table.Column<string>(nullable: true),
                    comment = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    project_stage = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_digital_economy_projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_digital_economy_projects_organization_organiza~",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_digital_economy_projects_organization_id",
                schema: "organizations",
                table: "organization_digital_economy_projects",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_digital_economy_projects",
                schema: "organizations");

            migrationBuilder.CreateTable(
                name: "sphere",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sphere", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "field",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    max_rate = table.Column<double>(type: "double precision", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true),
                    sphere_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_field_sphere_sphere_id",
                        column: x => x.sphere_id,
                        principalSchema: "ranking",
                        principalTable: "sphere",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "is_filled_table",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    comment = table.Column<string>(type: "text", nullable: true),
                    element_id = table.Column<int>(type: "integer", nullable: false),
                    field_id = table.Column<int>(type: "integer", nullable: false),
                    is_filled = table.Column<bool>(type: "boolean", nullable: false),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    quarter = table.Column<int>(type: "integer", nullable: false),
                    sphere_id = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_is_filled_table", x => x.id);
                    table.ForeignKey(
                        name: "FK_is_filled_table_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_is_filled_table_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_is_filled_table_sphere_sphere_id",
                        column: x => x.sphere_id,
                        principalSchema: "ranking",
                        principalTable: "sphere",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rank_table",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    comment = table.Column<string>(type: "text", nullable: true),
                    field_id = table.Column<int>(type: "integer", nullable: false),
                    is_exception = table.Column<bool>(type: "boolean", nullable: false),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    quarter = table.Column<int>(type: "integer", nullable: false),
                    rank = table.Column<double>(type: "double precision", nullable: false),
                    sphere_id = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rank_table", x => x.id);
                    table.ForeignKey(
                        name: "FK_rank_table_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rank_table_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rank_table_sphere_sphere_id",
                        column: x => x.sphere_id,
                        principalSchema: "ranking",
                        principalTable: "sphere",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_field",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    category = table.Column<int>(type: "integer", nullable: false),
                    field_id = table.Column<int>(type: "integer", nullable: false),
                    max_rate = table.Column<double>(type: "double precision", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_field_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_field_sphere_id",
                schema: "ranking",
                table: "field",
                column: "sphere_id");

            migrationBuilder.CreateIndex(
                name: "IX_is_filled_table_field_id",
                schema: "ranking",
                table: "is_filled_table",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_is_filled_table_organization_id",
                schema: "ranking",
                table: "is_filled_table",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_is_filled_table_sphere_id",
                schema: "ranking",
                table: "is_filled_table",
                column: "sphere_id");

            migrationBuilder.CreateIndex(
                name: "IX_rank_table_field_id",
                schema: "ranking",
                table: "rank_table",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_rank_table_organization_id",
                schema: "ranking",
                table: "rank_table",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_rank_table_sphere_id",
                schema: "ranking",
                table: "rank_table",
                column: "sphere_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_field_field_id",
                schema: "ranking",
                table: "sub_field",
                column: "field_id");
        }
    }
}
