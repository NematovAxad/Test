using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class sectionnameadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "section",
                schema: "ranking",
                table: "x_sub_field",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "section",
                schema: "ranking",
                table: "x_sphere",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "section",
                schema: "ranking",
                table: "x_field",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "section",
                schema: "ranking",
                table: "g_sub_field",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "section",
                schema: "ranking",
                table: "g_sphere",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "section",
                schema: "ranking",
                table: "g_field",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "section",
                schema: "ranking",
                table: "a_sub_field",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "section",
                schema: "ranking",
                table: "a_sphere",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "section",
                schema: "ranking",
                table: "a_field",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "section",
                schema: "ranking",
                table: "x_sub_field");

            migrationBuilder.DropColumn(
                name: "section",
                schema: "ranking",
                table: "x_sphere");

            migrationBuilder.DropColumn(
                name: "section",
                schema: "ranking",
                table: "x_field");

            migrationBuilder.DropColumn(
                name: "section",
                schema: "ranking",
                table: "g_sub_field");

            migrationBuilder.DropColumn(
                name: "section",
                schema: "ranking",
                table: "g_sphere");

            migrationBuilder.DropColumn(
                name: "section",
                schema: "ranking",
                table: "g_field");

            migrationBuilder.DropColumn(
                name: "section",
                schema: "ranking",
                table: "a_sub_field");

            migrationBuilder.DropColumn(
                name: "section",
                schema: "ranking",
                table: "a_sphere");

            migrationBuilder.DropColumn(
                name: "section",
                schema: "ranking",
                table: "a_field");
        }
    }
}
