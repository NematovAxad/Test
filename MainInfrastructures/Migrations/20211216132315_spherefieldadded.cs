using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class spherefieldadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rank_field_id",
                schema: "ranking",
                table: "rank_table");

            migrationBuilder.AddColumn<int>(
                name: "field_id",
                schema: "ranking",
                table: "rank_table",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "sphere_id",
                schema: "ranking",
                table: "rank_table",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "sphere",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(nullable: true)
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
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sphere_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    max_rate = table.Column<double>(nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_rank_table_field_id",
                schema: "ranking",
                table: "rank_table",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_rank_table_sphere_id",
                schema: "ranking",
                table: "rank_table",
                column: "sphere_id");

            migrationBuilder.CreateIndex(
                name: "IX_field_sphere_id",
                schema: "ranking",
                table: "field",
                column: "sphere_id");

            migrationBuilder.AddForeignKey(
                name: "FK_rank_table_field_field_id",
                schema: "ranking",
                table: "rank_table",
                column: "field_id",
                principalSchema: "ranking",
                principalTable: "field",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rank_table_sphere_sphere_id",
                schema: "ranking",
                table: "rank_table",
                column: "sphere_id",
                principalSchema: "ranking",
                principalTable: "sphere",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rank_table_field_field_id",
                schema: "ranking",
                table: "rank_table");

            migrationBuilder.DropForeignKey(
                name: "FK_rank_table_sphere_sphere_id",
                schema: "ranking",
                table: "rank_table");

            migrationBuilder.DropTable(
                name: "field",
                schema: "ranking");

            migrationBuilder.DropTable(
                name: "sphere",
                schema: "ranking");

            migrationBuilder.DropIndex(
                name: "IX_rank_table_field_id",
                schema: "ranking",
                table: "rank_table");

            migrationBuilder.DropIndex(
                name: "IX_rank_table_sphere_id",
                schema: "ranking",
                table: "rank_table");

            migrationBuilder.DropColumn(
                name: "field_id",
                schema: "ranking",
                table: "rank_table");

            migrationBuilder.DropColumn(
                name: "sphere_id",
                schema: "ranking",
                table: "rank_table");

            migrationBuilder.AddColumn<int>(
                name: "rank_field_id",
                schema: "ranking",
                table: "rank_table",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
