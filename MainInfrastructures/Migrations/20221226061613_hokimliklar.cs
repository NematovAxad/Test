using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class hokimliklar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "a_sphere",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_a_sphere", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "a_field",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sphere_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    max_rate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_a_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_a_field_a_sphere_sphere_id",
                        column: x => x.sphere_id,
                        principalSchema: "ranking",
                        principalTable: "a_sphere",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "a_rank_table",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    year = table.Column<int>(nullable: false),
                    quarter = table.Column<int>(nullable: false),
                    element_id = table.Column<int>(nullable: false),
                    rank = table.Column<double>(nullable: false),
                    is_exception = table.Column<bool>(nullable: false),
                    sphere_id = table.Column<int>(nullable: false),
                    field_id = table.Column<int>(nullable: false),
                    sub_field_id = table.Column<int>(nullable: false),
                    comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_a_rank_table", x => x.id);
                    table.ForeignKey(
                        name: "FK_a_rank_table_a_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "a_field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_a_rank_table_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_a_rank_table_a_sphere_sphere_id",
                        column: x => x.sphere_id,
                        principalSchema: "ranking",
                        principalTable: "a_sphere",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "a_sub_field",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    field_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    max_rate = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_a_sub_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_a_sub_field_a_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "a_field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_a_field_sphere_id",
                schema: "ranking",
                table: "a_field",
                column: "sphere_id");

            migrationBuilder.CreateIndex(
                name: "IX_a_rank_table_field_id",
                schema: "ranking",
                table: "a_rank_table",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_a_rank_table_organization_id",
                schema: "ranking",
                table: "a_rank_table",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_a_rank_table_sphere_id",
                schema: "ranking",
                table: "a_rank_table",
                column: "sphere_id");

            migrationBuilder.CreateIndex(
                name: "IX_a_sub_field_field_id",
                schema: "ranking",
                table: "a_sub_field",
                column: "field_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "a_rank_table",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "a_sub_field",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "a_field",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "a_sphere",
                schema: "ranking");
        }
    }
}
