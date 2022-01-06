using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class isfilled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "is_filled_table",
                schema: "ranking",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    year = table.Column<int>(nullable: false),
                    quarter = table.Column<int>(nullable: false),
                    is_filled = table.Column<bool>(nullable: false),
                    sphere_id = table.Column<int>(nullable: false),
                    field_id = table.Column<int>(nullable: false),
                    comment = table.Column<string>(nullable: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "is_filled_table",
                schema: "ranking");
        }
    }
}
