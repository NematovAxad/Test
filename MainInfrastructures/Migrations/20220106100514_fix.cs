using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "category",
                schema: "ranking",
                table: "sub_field",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "category",
                schema: "ranking",
                table: "sphere",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "category",
                schema: "ranking",
                table: "field",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                schema: "ranking",
                table: "sub_field");

            migrationBuilder.DropColumn(
                name: "category",
                schema: "ranking",
                table: "sphere");

            migrationBuilder.DropColumn(
                name: "category",
                schema: "ranking",
                table: "field");
        }
    }
}
