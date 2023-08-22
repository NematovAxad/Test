using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class columnnaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pool_expert",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.AddColumn<bool>(
                name: "pool_except_expert",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pool_except_expert",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.AddColumn<bool>(
                name: "pool_expert",
                schema: "organizations",
                table: "organization_socials",
                type: "boolean",
                nullable: true);
        }
    }
}
