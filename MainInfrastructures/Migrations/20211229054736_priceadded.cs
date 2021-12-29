using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class priceadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "project_price",
                schema: "organizations",
                table: "delays_on_projects",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "provided_fund",
                schema: "organizations",
                table: "delays_on_projects",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "project_price",
                schema: "organizations",
                table: "delays_on_projects");

            migrationBuilder.DropColumn(
                name: "provided_fund",
                schema: "organizations",
                table: "delays_on_projects");
        }
    }
}
