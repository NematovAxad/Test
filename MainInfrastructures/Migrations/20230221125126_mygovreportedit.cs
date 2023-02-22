using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class mygovreportedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "part",
                schema: "organizations",
                table: "mygov_reports",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "year",
                schema: "organizations",
                table: "mygov_reports",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "part",
                schema: "organizations",
                table: "mygov_reports");

            migrationBuilder.DropColumn(
                name: "year",
                schema: "organizations",
                table: "mygov_reports");
        }
    }
}
