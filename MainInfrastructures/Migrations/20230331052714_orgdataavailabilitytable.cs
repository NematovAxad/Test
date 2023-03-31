using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class orgdataavailabilitytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organization_data_availability",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    section = table.Column<string>(nullable: true),
                    data_availiability = table.Column<int>(nullable: false),
                    data_relevance = table.Column<bool>(nullable: false),
                    set_date = table.Column<DateTime>(nullable: false),
                    update_date = table.Column<DateTime>(nullable: false),
                    expert_pinfl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_data_availability", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_data_availability_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_data_availability_organization_id",
                schema: "organizations",
                table: "organization_data_availability",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_data_availability",
                schema: "organizations");
        }
    }
}
