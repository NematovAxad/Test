using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class orgfinancetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reestr_project_efficiency_reestr_project_efficiency_ReestrP~",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.DropIndex(
                name: "IX_reestr_project_efficiency_ReestrProjectEfficiencyId",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.DropColumn(
                name: "ReestrProjectEfficiencyId",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.CreateTable(
                name: "organization_finance",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    year = table.Column<int>(nullable: false),
                    plan_11 = table.Column<double>(nullable: false),
                    fact_11 = table.Column<double>(nullable: false),
                    plan_21 = table.Column<double>(nullable: false),
                    fact_21 = table.Column<double>(nullable: false),
                    plan_31 = table.Column<double>(nullable: false),
                    fact_31 = table.Column<double>(nullable: false),
                    plan_41 = table.Column<double>(nullable: false),
                    fact_41 = table.Column<double>(nullable: false),
                    plan_51 = table.Column<double>(nullable: false),
                    fact_51 = table.Column<double>(nullable: false),
                    plan_61 = table.Column<double>(nullable: false),
                    fact_61 = table.Column<double>(nullable: false),
                    plan_71 = table.Column<double>(nullable: false),
                    fact_71 = table.Column<double>(nullable: false),
                    plan_81 = table.Column<double>(nullable: false),
                    fact_81 = table.Column<double>(nullable: false),
                    all_plan_1 = table.Column<double>(nullable: false),
                    all_fact_1 = table.Column<double>(nullable: false),
                    plan_12 = table.Column<double>(nullable: false),
                    fact_12 = table.Column<double>(nullable: false),
                    plan_22 = table.Column<double>(nullable: false),
                    fact_22 = table.Column<double>(nullable: false),
                    plan_32 = table.Column<double>(nullable: false),
                    fact_32 = table.Column<double>(nullable: false),
                    plan_42 = table.Column<double>(nullable: false),
                    fact_42 = table.Column<double>(nullable: false),
                    plan_52 = table.Column<double>(nullable: false),
                    fact_52 = table.Column<double>(nullable: false),
                    plan_62 = table.Column<double>(nullable: false),
                    fact_62 = table.Column<double>(nullable: false),
                    plan_72 = table.Column<double>(nullable: false),
                    fact_72 = table.Column<double>(nullable: false),
                    plan_82 = table.Column<double>(nullable: false),
                    fact_82 = table.Column<double>(nullable: false),
                    all_plan_2 = table.Column<double>(nullable: false),
                    all_fact_2 = table.Column<double>(nullable: false),
                    plan_13 = table.Column<double>(nullable: false),
                    fact_13 = table.Column<double>(nullable: false),
                    plan_23 = table.Column<double>(nullable: false),
                    fact_23 = table.Column<double>(nullable: false),
                    plan_33 = table.Column<double>(nullable: false),
                    fact_33 = table.Column<double>(nullable: false),
                    plan_43 = table.Column<double>(nullable: false),
                    fact_43 = table.Column<double>(nullable: false),
                    plan_53 = table.Column<double>(nullable: false),
                    fact_53 = table.Column<double>(nullable: false),
                    plan_63 = table.Column<double>(nullable: false),
                    fact_63 = table.Column<double>(nullable: false),
                    plan_73 = table.Column<double>(nullable: false),
                    fact_73 = table.Column<double>(nullable: false),
                    plan_83 = table.Column<double>(nullable: false),
                    fact_83 = table.Column<double>(nullable: false),
                    all_plan_3 = table.Column<double>(nullable: false),
                    all_fact_3 = table.Column<double>(nullable: false),
                    plan_14 = table.Column<double>(nullable: false),
                    fact_14 = table.Column<double>(nullable: false),
                    plan_24 = table.Column<double>(nullable: false),
                    fact_24 = table.Column<double>(nullable: false),
                    plan_34 = table.Column<double>(nullable: false),
                    fact_34 = table.Column<double>(nullable: false),
                    plan_44 = table.Column<double>(nullable: false),
                    fact_44 = table.Column<double>(nullable: false),
                    plan_54 = table.Column<double>(nullable: false),
                    fact_54 = table.Column<double>(nullable: false),
                    plan_64 = table.Column<double>(nullable: false),
                    fact_64 = table.Column<double>(nullable: false),
                    plan_74 = table.Column<double>(nullable: false),
                    fact_74 = table.Column<double>(nullable: false),
                    plan_84 = table.Column<double>(nullable: false),
                    fact_84 = table.Column<double>(nullable: false),
                    all_plan_4 = table.Column<double>(nullable: false),
                    all_fact_4 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_finance", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_finance_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_finance_organization_id",
                schema: "organizations",
                table: "organization_finance",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_finance",
                schema: "organizations");

            migrationBuilder.AddColumn<int>(
                name: "ReestrProjectEfficiencyId",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_reestr_project_efficiency_ReestrProjectEfficiencyId",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                column: "ReestrProjectEfficiencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_reestr_project_efficiency_reestr_project_efficiency_ReestrP~",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                column: "ReestrProjectEfficiencyId",
                principalSchema: "reestrprojects",
                principalTable: "reestr_project_efficiency",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
