using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class rankingtablesedited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "element_id",
                schema: "ranking",
                table: "x_rank_table",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "element_id",
                schema: "ranking",
                table: "g_rank_table",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "element_id",
                schema: "ranking",
                table: "x_rank_table");

            migrationBuilder.DropColumn(
                name: "element_id",
                schema: "ranking",
                table: "g_rank_table");
        }
    }
}
