using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class spheremaxrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "max_rate",
                schema: "ranking",
                table: "x_sphere",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "max_rate",
                schema: "ranking",
                table: "g_sphere",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "max_rate",
                schema: "ranking",
                table: "a_sphere",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "max_rate",
                schema: "ranking",
                table: "x_sphere");

            migrationBuilder.DropColumn(
                name: "max_rate",
                schema: "ranking",
                table: "g_sphere");

            migrationBuilder.DropColumn(
                name: "max_rate",
                schema: "ranking",
                table: "a_sphere");
        }
    }
}
