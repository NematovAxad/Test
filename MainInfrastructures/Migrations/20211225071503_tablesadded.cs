using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class tablesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "org_information_systems",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    system_name = table.Column<string>(nullable: true),
                    system_appointment = table.Column<string>(nullable: true),
                    system_reestr_number = table.Column<string>(nullable: true),
                    system_condition = table.Column<string>(nullable: true),
                    commissioning_date = table.Column<DateTime>(nullable: false),
                    expert_opinion_date = table.Column<DateTime>(nullable: false),
                    expert_opinion_number = table.Column<string>(nullable: true),
                    list_of_services = table.Column<string>(nullable: true),
                    users_count = table.Column<int>(nullable: false),
                    uses_classifiers = table.Column<bool>(nullable: false),
                    used_clasifiers = table.Column<string>(nullable: true),
                    other_clasifiers = table.Column<string>(nullable: true),
                    has_integration_with_egovernment = table.Column<bool>(nullable: false),
                    integration_interdepartmental_platform = table.Column<bool>(nullable: false),
                    transmitting_informations_first = table.Column<string>(nullable: true),
                    integrated_register_classifiers = table.Column<bool>(nullable: false),
                    transmitting_informations_second = table.Column<string>(nullable: true),
                    integrated_central_database = table.Column<bool>(nullable: false),
                    transmitting_informations_third = table.Column<string>(nullable: true),
                    integrated_complexes_of_systems = table.Column<bool>(nullable: false),
                    transmitting_informations_fourth = table.Column<string>(nullable: true),
                    integrated_payment_system = table.Column<bool>(nullable: false),
                    payment_system_name = table.Column<string>(nullable: true),
                    availability_automation_of_service = table.Column<bool>(nullable: false),
                    automated_services = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_information_systems", x => x.id);
                    table.ForeignKey(
                        name: "FK_org_information_systems_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_public_services",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    service_name = table.Column<string>(nullable: true),
                    user_types = table.Column<string>(nullable: true),
                    rendering_form = table.Column<string>(nullable: true),
                    portal_link = table.Column<string>(nullable: true),
                    service_link = table.Column<string>(nullable: true),
                    mobile_app = table.Column<string>(nullable: true),
                    other_resources = table.Column<string>(nullable: true),
                    is_paid = table.Column<bool>(nullable: false),
                    service_result = table.Column<string>(nullable: true),
                    service_other_result = table.Column<string>(nullable: true),
                    mechanizm_for_tracking_progress = table.Column<bool>(nullable: false),
                    tracking_progress_by = table.Column<string>(nullable: true),
                    reglament_path = table.Column<string>(nullable: true),
                    reglament_updated = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_public_services", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_public_services_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_org_information_systems_organization_id",
                schema: "organizations",
                table: "org_information_systems",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_public_services_organization_id",
                schema: "organizations",
                table: "organization_public_services",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "org_information_systems",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "organization_public_services",
                schema: "organizations");
        }
    }
}
