using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class newmigrationn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_connections_reestr_project_connection_reestr_projec~",
                schema: "reestrprojects",
                table: "project_connections");

            migrationBuilder.DropIndex(
                name: "IX_project_connections_reestr_project_connection_id",
                schema: "reestrprojects",
                table: "project_connections");

            migrationBuilder.DropColumn(
                name: "reestr_project_connection_id",
                schema: "reestrprojects",
                table: "project_connections");

            migrationBuilder.AddColumn<int>(
                name: "parent_id",
                schema: "reestrprojects",
                table: "project_connections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_project_connections_parent_id",
                schema: "reestrprojects",
                table: "project_connections",
                column: "parent_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_connections_reestr_project_connection_parent_id",
                schema: "reestrprojects",
                table: "project_connections",
                column: "parent_id",
                principalSchema: "reestrprojects",
                principalTable: "reestr_project_connection",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_connections_reestr_project_connection_parent_id",
                schema: "reestrprojects",
                table: "project_connections");

            migrationBuilder.DropIndex(
                name: "IX_project_connections_parent_id",
                schema: "reestrprojects",
                table: "project_connections");

            migrationBuilder.DropColumn(
                name: "parent_id",
                schema: "reestrprojects",
                table: "project_connections");

            migrationBuilder.AddColumn<int>(
                name: "reestr_project_connection_id",
                schema: "reestrprojects",
                table: "project_connections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_project_connections_reestr_project_connection_id",
                schema: "reestrprojects",
                table: "project_connections",
                column: "reestr_project_connection_id");

            migrationBuilder.AddForeignKey(
                name: "FK_project_connections_reestr_project_connection_reestr_projec~",
                schema: "reestrprojects",
                table: "project_connections",
                column: "reestr_project_connection_id",
                principalSchema: "reestrprojects",
                principalTable: "reestr_project_connection",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
