using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class bools : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cooworkers_performencer_performencer_id",
                schema: "module_regions",
                table: "cooworkers");

            migrationBuilder.DropForeignKey(
                name: "FK_project_performencer_performencer_id",
                schema: "module_regions",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_performencer_id",
                schema: "module_regions",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_cooworkers_performencer_id",
                schema: "module_regions",
                table: "cooworkers");

            migrationBuilder.DropColumn(
                name: "performencer_id",
                schema: "module_regions",
                table: "project");

            migrationBuilder.DropColumn(
                name: "performencer_id",
                schema: "module_regions",
                table: "cooworkers");

            migrationBuilder.AddColumn<bool>(
                name: "IsIct",
                schema: "organizations",
                table: "organization",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMonitoring",
                schema: "organizations",
                table: "organization",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "organization_id",
                schema: "module_regions",
                table: "project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "organization_id",
                schema: "module_regions",
                table: "cooworkers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_project_organization_id",
                schema: "module_regions",
                table: "project",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_cooworkers_organization_id",
                schema: "module_regions",
                table: "cooworkers",
                column: "organization_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cooworkers_organization_organization_id",
                schema: "module_regions",
                table: "cooworkers",
                column: "organization_id",
                principalSchema: "organizations",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_organization_organization_id",
                schema: "module_regions",
                table: "project",
                column: "organization_id",
                principalSchema: "organizations",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cooworkers_organization_organization_id",
                schema: "module_regions",
                table: "cooworkers");

            migrationBuilder.DropForeignKey(
                name: "FK_project_organization_organization_id",
                schema: "module_regions",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_project_organization_id",
                schema: "module_regions",
                table: "project");

            migrationBuilder.DropIndex(
                name: "IX_cooworkers_organization_id",
                schema: "module_regions",
                table: "cooworkers");

            migrationBuilder.DropColumn(
                name: "IsIct",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "IsMonitoring",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "organization_id",
                schema: "module_regions",
                table: "project");

            migrationBuilder.DropColumn(
                name: "organization_id",
                schema: "module_regions",
                table: "cooworkers");

            migrationBuilder.AddColumn<int>(
                name: "performencer_id",
                schema: "module_regions",
                table: "project",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "performencer_id",
                schema: "module_regions",
                table: "cooworkers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_project_performencer_id",
                schema: "module_regions",
                table: "project",
                column: "performencer_id");

            migrationBuilder.CreateIndex(
                name: "IX_cooworkers_performencer_id",
                schema: "module_regions",
                table: "cooworkers",
                column: "performencer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cooworkers_performencer_performencer_id",
                schema: "module_regions",
                table: "cooworkers",
                column: "performencer_id",
                principalSchema: "module_regions",
                principalTable: "performencer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_project_performencer_performencer_id",
                schema: "module_regions",
                table: "project",
                column: "performencer_id",
                principalSchema: "module_regions",
                principalTable: "performencer",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
