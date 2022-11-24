using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "exist",
                schema: "reestrprojects",
                table: "reestr_project_connection",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "project_connections",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reestr_project_connection_id = table.Column<int>(nullable: false),
                    connection_type = table.Column<int>(nullable: false),
                    platform_reestr_id = table.Column<string>(nullable: true),
                    file_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_connections", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_connections_reestr_project_connection_reestr_projec~",
                        column: x => x.reestr_project_connection_id,
                        principalSchema: "reestrprojects",
                        principalTable: "reestr_project_connection",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_project_connections_reestr_project_connection_id",
                schema: "reestrprojects",
                table: "project_connections",
                column: "reestr_project_connection_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "project_connections",
                schema: "reestrprojects");

            migrationBuilder.DropColumn(
                name: "exist",
                schema: "reestrprojects",
                table: "reestr_project_connection");
        }
    }
}
