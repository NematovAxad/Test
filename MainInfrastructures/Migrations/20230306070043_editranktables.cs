using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class editranktables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "expert_id",
                schema: "ranking",
                table: "x_rank_table",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "espert_pinfl",
                schema: "ranking",
                table: "x_rank_table",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "expert_id",
                schema: "ranking",
                table: "g_rank_table",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "espert_pinfl",
                schema: "ranking",
                table: "g_rank_table",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "expert_id",
                schema: "ranking",
                table: "a_rank_table",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "espert_pinfl",
                schema: "ranking",
                table: "a_rank_table",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expert_id",
                schema: "ranking",
                table: "x_rank_table");

            migrationBuilder.DropColumn(
                name: "espert_pinfl",
                schema: "ranking",
                table: "x_rank_table");

            migrationBuilder.DropColumn(
                name: "expert_id",
                schema: "ranking",
                table: "g_rank_table");

            migrationBuilder.DropColumn(
                name: "espert_pinfl",
                schema: "ranking",
                table: "g_rank_table");

            migrationBuilder.DropColumn(
                name: "expert_id",
                schema: "ranking",
                table: "a_rank_table");

            migrationBuilder.DropColumn(
                name: "espert_pinfl",
                schema: "ranking",
                table: "a_rank_table");
        }
    }
}
