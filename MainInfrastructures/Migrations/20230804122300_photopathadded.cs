using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class photopathadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "photo_path",
                schema: "organizations",
                table: "replacer_org_head",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "photo_path",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "photo_path",
                schema: "organizations",
                table: "replacer_org_head");

            migrationBuilder.DropColumn(
                name: "photo_path",
                schema: "organizations",
                table: "organization_ict_special_forces");
        }
    }
}
