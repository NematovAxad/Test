using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name_ru",
                schema: "module_regions",
                table: "stage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_uz",
                schema: "module_regions",
                table: "stage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "action",
                schema: "module_regions",
                table: "comment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_role",
                schema: "module_regions",
                table: "comment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name_ru",
                schema: "module_regions",
                table: "stage");

            migrationBuilder.DropColumn(
                name: "name_uz",
                schema: "module_regions",
                table: "stage");

            migrationBuilder.DropColumn(
                name: "action",
                schema: "module_regions",
                table: "comment");

            migrationBuilder.DropColumn(
                name: "user_role",
                schema: "module_regions",
                table: "comment");
        }
    }
}
