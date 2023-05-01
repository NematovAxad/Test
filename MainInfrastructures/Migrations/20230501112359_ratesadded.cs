using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class ratesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "data_availiability_rate",
                schema: "organizations",
                table: "organization_data_availability",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "data_relevance_rate",
                schema: "organizations",
                table: "organization_data_availability",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_availiability_rate",
                schema: "organizations",
                table: "organization_data_availability");

            migrationBuilder.DropColumn(
                name: "data_relevance_rate",
                schema: "organizations",
                table: "organization_data_availability");
        }
    }
}
