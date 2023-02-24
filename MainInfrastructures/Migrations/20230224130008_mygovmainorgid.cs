using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class mygovmainorgid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mygov_id",
                schema: "organizations",
                table: "mygov_reports");

            migrationBuilder.AddColumn<int>(
                name: "mygov_main_org_id",
                schema: "organizations",
                table: "mygov_reports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "mygov_org_id",
                schema: "organizations",
                table: "mygov_reports",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mygov_main_org_id",
                schema: "organizations",
                table: "mygov_reports");

            migrationBuilder.DropColumn(
                name: "mygov_org_id",
                schema: "organizations",
                table: "mygov_reports");

            migrationBuilder.AddColumn<int>(
                name: "mygov_id",
                schema: "organizations",
                table: "mygov_reports",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
