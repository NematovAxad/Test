using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class fkchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_authorizations_reestr_project_classifications_paren~",
                schema: "reestrprojects",
                table: "project_authorizations");

            migrationBuilder.DropForeignKey(
                name: "FK_project_authorizations_reestr_project_authorizations_Reestr~",
                schema: "reestrprojects",
                table: "project_authorizations");

            migrationBuilder.DropIndex(
                name: "IX_project_authorizations_ReestrProjectAuthorizationsId",
                schema: "reestrprojects",
                table: "project_authorizations");

            migrationBuilder.DropColumn(
                name: "ReestrProjectAuthorizationsId",
                schema: "reestrprojects",
                table: "project_authorizations");

            migrationBuilder.AddForeignKey(
                name: "FK_project_authorizations_reestr_project_authorizations_parent~",
                schema: "reestrprojects",
                table: "project_authorizations",
                column: "parent_id",
                principalSchema: "reestrprojects",
                principalTable: "reestr_project_authorizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_project_authorizations_reestr_project_authorizations_parent~",
                schema: "reestrprojects",
                table: "project_authorizations");

            migrationBuilder.AddColumn<int>(
                name: "ReestrProjectAuthorizationsId",
                schema: "reestrprojects",
                table: "project_authorizations",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_project_authorizations_ReestrProjectAuthorizationsId",
                schema: "reestrprojects",
                table: "project_authorizations",
                column: "ReestrProjectAuthorizationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_project_authorizations_reestr_project_classifications_paren~",
                schema: "reestrprojects",
                table: "project_authorizations",
                column: "parent_id",
                principalSchema: "reestrprojects",
                principalTable: "reestr_project_classifications",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_authorizations_reestr_project_authorizations_Reestr~",
                schema: "reestrprojects",
                table: "project_authorizations",
                column: "ReestrProjectAuthorizationsId",
                principalSchema: "reestrprojects",
                principalTable: "reestr_project_authorizations",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
