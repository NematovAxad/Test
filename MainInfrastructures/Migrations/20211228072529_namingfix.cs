using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class namingfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ministry_agreed_characterizing_document",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ministry_agreed_characterizing_document",
                schema: "organizations",
                table: "organization_ict_special_forces");
        }
    }
}
