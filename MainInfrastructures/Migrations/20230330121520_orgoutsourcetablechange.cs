using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class orgoutsourcetablechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "expert_comment",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "expert_except",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "outsourcing_company_sertificate",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "outsourcing_work_plan_file",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "quarterly_report_outsourcing_file",
                schema: "organizations",
                table: "organization_ict_special_forces",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "expert_comment",
                schema: "organizations",
                table: "organization_ict_special_forces");

            migrationBuilder.DropColumn(
                name: "expert_except",
                schema: "organizations",
                table: "organization_ict_special_forces");

            migrationBuilder.DropColumn(
                name: "outsourcing_company_sertificate",
                schema: "organizations",
                table: "organization_ict_special_forces");

            migrationBuilder.DropColumn(
                name: "outsourcing_work_plan_file",
                schema: "organizations",
                table: "organization_ict_special_forces");

            migrationBuilder.DropColumn(
                name: "quarterly_report_outsourcing_file",
                schema: "organizations",
                table: "organization_ict_special_forces");
        }
    }
}
