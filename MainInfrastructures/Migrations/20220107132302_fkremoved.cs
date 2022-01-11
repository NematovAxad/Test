using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class fkremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_g_rank_table_g_sub_field_sub_field_id",
                schema: "ranking",
                table: "g_rank_table");

            migrationBuilder.DropForeignKey(
                name: "FK_x_rank_table_x_sub_field_sub_field_id",
                schema: "ranking",
                table: "x_rank_table");

            migrationBuilder.DropIndex(
                name: "IX_x_rank_table_sub_field_id",
                schema: "ranking",
                table: "x_rank_table");

            migrationBuilder.DropIndex(
                name: "IX_g_rank_table_sub_field_id",
                schema: "ranking",
                table: "g_rank_table");

            migrationBuilder.AddColumn<int>(
                name: "element_id",
                schema: "ranking",
                table: "is_filled_table",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "element_id",
                schema: "ranking",
                table: "is_filled_table");

            migrationBuilder.CreateIndex(
                name: "IX_x_rank_table_sub_field_id",
                schema: "ranking",
                table: "x_rank_table",
                column: "sub_field_id");

            migrationBuilder.CreateIndex(
                name: "IX_g_rank_table_sub_field_id",
                schema: "ranking",
                table: "g_rank_table",
                column: "sub_field_id");

            migrationBuilder.AddForeignKey(
                name: "FK_g_rank_table_g_sub_field_sub_field_id",
                schema: "ranking",
                table: "g_rank_table",
                column: "sub_field_id",
                principalSchema: "ranking",
                principalTable: "g_sub_field",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_x_rank_table_x_sub_field_sub_field_id",
                schema: "ranking",
                table: "x_rank_table",
                column: "sub_field_id",
                principalSchema: "ranking",
                principalTable: "x_sub_field",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
