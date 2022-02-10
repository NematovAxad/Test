using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class financiersfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financier_project_ProjectId",
                schema: "module_regions",
                table: "financier");

            migrationBuilder.DropIndex(
                name: "IX_financier_ProjectId",
                schema: "module_regions",
                table: "financier");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                schema: "module_regions",
                table: "financier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                schema: "module_regions",
                table: "financier",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_financier_ProjectId",
                schema: "module_regions",
                table: "financier",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_financier_project_ProjectId",
                schema: "module_regions",
                table: "financier",
                column: "ProjectId",
                principalSchema: "module_regions",
                principalTable: "project",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
