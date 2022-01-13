using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class rulesorgremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "organization_id",
                schema: "ranking",
                table: "x_rank_rules");

            migrationBuilder.DropColumn(
                name: "organization_id",
                schema: "ranking",
                table: "g_rank_rules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "organization_id",
                schema: "ranking",
                table: "x_rank_rules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "organization_id",
                schema: "ranking",
                table: "g_rank_rules",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
