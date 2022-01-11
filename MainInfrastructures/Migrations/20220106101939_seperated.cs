using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class seperated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                schema: "ranking",
                table: "sphere");

            migrationBuilder.DropColumn(
                name: "category",
                schema: "ranking",
                table: "field");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "category",
                schema: "ranking",
                table: "sphere",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "category",
                schema: "ranking",
                table: "field",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
