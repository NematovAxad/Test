using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class fkdeadline : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "deadline_id",
                schema: "organizations",
                table: "organization_data_availability",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_organization_data_availability_deadline_id",
                schema: "organizations",
                table: "organization_data_availability",
                column: "deadline_id");

            migrationBuilder.AddForeignKey(
                name: "FK_organization_data_availability_deadline_deadline_id",
                schema: "organizations",
                table: "organization_data_availability",
                column: "deadline_id",
                principalSchema: "ranking",
                principalTable: "deadline",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_organization_data_availability_deadline_deadline_id",
                schema: "organizations",
                table: "organization_data_availability");

            migrationBuilder.DropIndex(
                name: "IX_organization_data_availability_deadline_id",
                schema: "organizations",
                table: "organization_data_availability");

            migrationBuilder.DropColumn(
                name: "deadline_id",
                schema: "organizations",
                table: "organization_data_availability");
        }
    }
}
