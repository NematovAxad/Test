using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization_ict_special_forces",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    has_special_forces = table.Column<bool>(nullable: false),
                    special_forces_name = table.Column<string>(nullable: true),
                    form_of_special_forces = table.Column<string>(nullable: true),
                    full_name_head = table.Column<string>(nullable: true),
                    head_position = table.Column<string>(nullable: true),
                    work_phone = table.Column<string>(nullable: true),
                    mobile_phone = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    ministry_agreed_head = table.Column<bool>(nullable: false),
                    has_characterizing_document = table.Column<bool>(nullable: false),
                    characterizing_document = table.Column<string>(nullable: true),
                    employees_sum = table.Column<int>(nullable: false),
                    central_office_employees = table.Column<int>(nullable: false),
                    regional_employees = table.Column<int>(nullable: false),
                    subordinate_employees = table.Column<int>(nullable: false),
                    information_security_employees = table.Column<int>(nullable: false),
                    information_system_database_employees = table.Column<int>(nullable: false),
                    organizational_structure_file = table.Column<string>(nullable: true),
                    specialists_stuffing_document = table.Column<string>(nullable: true),
                    employees_sertificates = table.Column<string>(nullable: true),
                    employees_resumes_sent_ministry = table.Column<bool>(nullable: false),
                    has_work_plan_of_special_forces = table.Column<bool>(nullable: false),
                    work_plan_of_special_forces = table.Column<string>(nullable: true),
                    finance_provision_material = table.Column<bool>(nullable: false),
                    amount_of_funds = table.Column<double>(nullable: false),
                    last_year_amount_of_funds = table.Column<double>(nullable: false),
                    fund_for_keeping_forces = table.Column<double>(nullable: false),
                    amount_of_spent_fund = table.Column<double>(nullable: false),
                    next_year_fund_for_keeping_forces = table.Column<double>(nullable: false),
                    outsourcing_spent_fund = table.Column<double>(nullable: false),
                    outsourcing_has_certificates = table.Column<bool>(nullable: false),
                    outsourcing_employees = table.Column<int>(nullable: false),
                    outsourcing_has_work_plan = table.Column<bool>(nullable: false),
                    quarterly_report_outsourcing = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_ict_special_forces", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_ict_special_forces_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_ict_special_forces_organization_id",
                schema: "organizations",
                table: "organization_ict_special_forces",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_ict_special_forces",
                schema: "organizations");
        }
    }
}
