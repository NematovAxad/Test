using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class fixnaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "has_ministry_agreed_characterizing_document",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "has_ministry_agreed_characterizing_document",
                schema: "organizations",
                table: "organization_ict_special_forces");
        }
    }
}
