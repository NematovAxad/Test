using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class orgbugdetadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization_budget",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    year = table.Column<int>(nullable: false),
                    personal_budget_plan1 = table.Column<double>(nullable: false),
                    personal_budget_fact1 = table.Column<double>(nullable: false),
                    local_investment_budget_plan1 = table.Column<double>(nullable: false),
                    local_investment_budget_fact1 = table.Column<double>(nullable: false),
                    foreign_budget_plan1 = table.Column<double>(nullable: false),
                    foreign_budget_fact1 = table.Column<double>(nullable: false),
                    other_budget_plan1 = table.Column<double>(nullable: false),
                    other_budget_fact1 = table.Column<double>(nullable: false),
                    all_plan1 = table.Column<double>(nullable: false),
                    all_fact1 = table.Column<double>(nullable: false),
                    personal_budget_plan2 = table.Column<double>(nullable: false),
                    personal_budget_fact2 = table.Column<double>(nullable: false),
                    local_investment_budget_plan2 = table.Column<double>(nullable: false),
                    local_investment_budget_fact2 = table.Column<double>(nullable: false),
                    foreign_budget_plan2 = table.Column<double>(nullable: false),
                    foreign_budget_fact2 = table.Column<double>(nullable: false),
                    other_budget_plan2 = table.Column<double>(nullable: false),
                    other_budget_fact2 = table.Column<double>(nullable: false),
                    all_plan2 = table.Column<double>(nullable: false),
                    all_fact2 = table.Column<double>(nullable: false),
                    personal_budget_plan3 = table.Column<double>(nullable: false),
                    personal_budget_fact3 = table.Column<double>(nullable: false),
                    local_investment_budget_plan3 = table.Column<double>(nullable: false),
                    local_investment_budget_fact3 = table.Column<double>(nullable: false),
                    foreign_budget_plan3 = table.Column<double>(nullable: false),
                    foreign_budget_fact3 = table.Column<double>(nullable: false),
                    other_budget_plan3 = table.Column<double>(nullable: false),
                    other_budget_fact3 = table.Column<double>(nullable: false),
                    all_plan3 = table.Column<double>(nullable: false),
                    all_fact3 = table.Column<double>(nullable: false),
                    personal_budget_plan4 = table.Column<double>(nullable: false),
                    personal_budget_fact4 = table.Column<double>(nullable: false),
                    local_investment_budget_plan4 = table.Column<double>(nullable: false),
                    local_investment_budget_fact4 = table.Column<double>(nullable: false),
                    foreign_budget_plan4 = table.Column<double>(nullable: false),
                    foreign_budget_fact4 = table.Column<double>(nullable: false),
                    other_budget_plan4 = table.Column<double>(nullable: false),
                    other_budget_fact4 = table.Column<double>(nullable: false),
                    all_plan4 = table.Column<double>(nullable: false),
                    all_fact4 = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_budget", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_budget_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_budget_organization_id",
                schema: "organizations",
                table: "organization_budget",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_budget",
                schema: "organizations");
        }
    }
}
