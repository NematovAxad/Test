using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class tablesaddedd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "g_sphere",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_g_sphere", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "x_sphere",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_x_sphere", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "g_field",
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
                    table.PrimaryKey("PK_g_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_g_field_g_sphere_sphere_id",
                        column: x => x.sphere_id,
                        principalSchema: "ranking",
                        principalTable: "g_sphere",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "x_field",
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
                    table.PrimaryKey("PK_x_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_x_field_x_sphere_sphere_id",
                        column: x => x.sphere_id,
                        principalSchema: "ranking",
                        principalTable: "x_sphere",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "g_sub_field",
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
                    table.PrimaryKey("PK_g_sub_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_g_sub_field_g_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "g_field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "x_sub_field",
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
                    table.PrimaryKey("PK_x_sub_field", x => x.id);
                    table.ForeignKey(
                        name: "FK_x_sub_field_x_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "x_field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_g_field_sphere_id",
                schema: "ranking",
                table: "g_field",
                column: "sphere_id");

            migrationBuilder.CreateIndex(
                name: "IX_g_sub_field_field_id",
                schema: "ranking",
                table: "g_sub_field",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_x_field_sphere_id",
                schema: "ranking",
                table: "x_field",
                column: "sphere_id");

            migrationBuilder.CreateIndex(
                name: "IX_x_sub_field_field_id",
                schema: "ranking",
                table: "x_sub_field",
                column: "field_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "g_sub_field",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "x_sub_field",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "g_field",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "x_field",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "g_sphere",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "x_sphere",
                schema: "ranking");
        }
    }
}
