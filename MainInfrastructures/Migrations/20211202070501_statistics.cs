using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class statistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sub_organization_organization_OrganizationId",
                schema: "organizations",
                table: "sub_organization");

            migrationBuilder.DropIndex(
                name: "IX_sub_organization_OrganizationId",
                schema: "organizations",
                table: "sub_organization");

            migrationBuilder.DropColumn(
                name: "OrganizationId",
                schema: "organizations",
                table: "sub_organization");

            migrationBuilder.CreateTable(
                name: "employee_statistics",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    central_management_positions = table.Column<int>(nullable: false),
                    central_management_employees = table.Column<int>(nullable: false),
                    territorial_management_positions = table.Column<int>(nullable: false),
                    territorial_management_employees = table.Column<int>(nullable: false),
                    subordination_positions = table.Column<int>(nullable: false),
                    subordination_employees = table.Column<int>(nullable: false),
                    other_positions = table.Column<int>(nullable: false),
                    other_employees = table.Column<int>(nullable: false),
                    head_positions = table.Column<int>(nullable: false),
                    head_employees = table.Column<int>(nullable: false),
                    department_head_positions = table.Column<int>(nullable: false),
                    department_head_employees = table.Column<int>(nullable: false),
                    specialists_position = table.Column<int>(nullable: false),
                    specialists_employee = table.Column<int>(nullable: false),
                    production_personnels_position = table.Column<int>(nullable: false),
                    production_personnels_employee = table.Column<int>(nullable: false),
                    technical_stuff_positions = table.Column<int>(nullable: false),
                    technical_stuff_employee = table.Column<int>(nullable: false),
                    service_stuff_positions = table.Column<int>(nullable: false),
                    service_stuff_employee = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee_statistics", x => x.id);
                    table.ForeignKey(
                        name: "FK_employee_statistics_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_org_statistics",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    central_managements = table.Column<int>(nullable: false),
                    territorial_managements = table.Column<int>(nullable: false),
                    subordinations = table.Column<int>(nullable: false),
                    others = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_org_statistics", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_org_statistics_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sub_organization_parent_id",
                schema: "organizations",
                table: "sub_organization",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_statistics_organization_id",
                schema: "organizations",
                table: "employee_statistics",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_org_statistics_organization_id",
                schema: "organizations",
                table: "sub_org_statistics",
                column: "organization_id");

            migrationBuilder.AddForeignKey(
                name: "FK_sub_organization_organization_parent_id",
                schema: "organizations",
                table: "sub_organization",
                column: "parent_id",
                principalSchema: "organizations",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sub_organization_organization_parent_id",
                schema: "organizations",
                table: "sub_organization");

            migrationBuilder.DropTable(
                name: "employee_statistics",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "sub_org_statistics",
                schema: "organizations");

            migrationBuilder.DropIndex(
                name: "IX_sub_organization_parent_id",
                schema: "organizations",
                table: "sub_organization");

            migrationBuilder.AddColumn<int>(
                name: "OrganizationId",
                schema: "organizations",
                table: "sub_organization",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_sub_organization_OrganizationId",
                schema: "organizations",
                table: "sub_organization",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_sub_organization_organization_OrganizationId",
                schema: "organizations",
                table: "sub_organization",
                column: "OrganizationId",
                principalSchema: "organizations",
                principalTable: "organization",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
