using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class efficiencyitems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "efficiency_type",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.DropColumn(
                name: "file_path",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.AddColumn<bool>(
                name: "exist",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ReestrProjectEfficiencyId",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "project_efficiency",
                schema: "reestrprojects",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(nullable: false),
                    efficiency_type = table.Column<int>(nullable: false),
                    file_path = table.Column<string>(nullable: true),
                    org_comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_project_efficiency", x => x.id);
                    table.ForeignKey(
                        name: "FK_project_efficiency_reestr_project_efficiency_parent_id",
                        column: x => x.parent_id,
                        principalSchema: "reestrprojects",
                        principalTable: "reestr_project_efficiency",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reestr_project_efficiency_ReestrProjectEfficiencyId",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                column: "ReestrProjectEfficiencyId");

            migrationBuilder.CreateIndex(
                name: "IX_project_efficiency_parent_id",
                schema: "reestrprojects",
                table: "project_efficiency",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_reestr_project_efficiency_reestr_project_efficiency_ReestrP~",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                column: "ReestrProjectEfficiencyId",
                principalSchema: "reestrprojects",
                principalTable: "reestr_project_efficiency",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reestr_project_efficiency_reestr_project_efficiency_ReestrP~",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.DropTable(
                name: "project_efficiency",
                schema: "reestrprojects");

            migrationBuilder.DropIndex(
                name: "IX_reestr_project_efficiency_ReestrProjectEfficiencyId",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.DropColumn(
                name: "exist",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.DropColumn(
                name: "ReestrProjectEfficiencyId",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.AddColumn<int>(
                name: "efficiency_type",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "file_path",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                type: "text",
                nullable: true);
        }
    }
}
