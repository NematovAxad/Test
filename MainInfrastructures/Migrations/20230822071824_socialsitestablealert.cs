using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class socialsitestablealert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pool_comment_expert",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "pool_except_expert",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pool_comment_expert",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "pool_except_expert",
                schema: "organizations",
                table: "organization_socials");
        }
    }
}
