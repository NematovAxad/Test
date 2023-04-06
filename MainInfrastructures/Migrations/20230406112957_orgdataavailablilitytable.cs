using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class orgdataavailablilitytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "data_availiability",
                schema: "organizations",
                table: "organization_data_availability",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "sphere",
                schema: "organizations",
                table: "organization_data_availability",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sphere",
                schema: "organizations",
                table: "organization_data_availability");

            migrationBuilder.AlterColumn<int>(
                name: "data_availiability",
                schema: "organizations",
                table: "organization_data_availability",
                type: "integer",
                nullable: false,
                oldClrType: typeof(bool));
        }
    }
}
