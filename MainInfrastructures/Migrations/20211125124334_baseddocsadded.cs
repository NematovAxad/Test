using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class baseddocsadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "org_category",
                schema: "organizations",
                table: "organization",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "org_category",
                schema: "organizations",
                table: "organization");
        }
    }
}
