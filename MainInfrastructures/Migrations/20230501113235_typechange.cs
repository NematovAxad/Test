using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class typechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "data_relevance_rate",
                schema: "organizations",
                table: "organization_data_availability",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<double>(
                name: "data_availiability_rate",
                schema: "organizations",
                table: "organization_data_availability",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "data_relevance_rate",
                schema: "organizations",
                table: "organization_data_availability",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<decimal>(
                name: "data_availiability_rate",
                schema: "organizations",
                table: "organization_data_availability",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
