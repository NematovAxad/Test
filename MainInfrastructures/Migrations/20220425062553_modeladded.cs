using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class modeladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "website_requirements",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    requirement1 = table.Column<int>(nullable: false),
                    requirement2 = table.Column<int>(nullable: false),
                    requirement3 = table.Column<int>(nullable: false),
                    requirement4 = table.Column<int>(nullable: false),
                    requirement5 = table.Column<int>(nullable: false),
                    requirement6 = table.Column<int>(nullable: false),
                    requirement7 = table.Column<int>(nullable: false),
                    requirement8 = table.Column<int>(nullable: false),
                    requirement9 = table.Column<int>(nullable: false),
                    requirement10 = table.Column<int>(nullable: false),
                    requirement11 = table.Column<int>(nullable: false),
                    requirement12 = table.Column<int>(nullable: false),
                    requirement13 = table.Column<int>(nullable: false),
                    requirement14 = table.Column<int>(nullable: false),
                    requirement15 = table.Column<int>(nullable: false),
                    requirement16 = table.Column<int>(nullable: false),
                    requirement17 = table.Column<int>(nullable: false),
                    requirement18 = table.Column<int>(nullable: false),
                    requirement19 = table.Column<int>(nullable: false),
                    requirement20 = table.Column<int>(nullable: false),
                    requirement21 = table.Column<int>(nullable: false),
                    requirement22 = table.Column<int>(nullable: false),
                    requirement23 = table.Column<int>(nullable: false),
                    requirement24 = table.Column<int>(nullable: false),
                    requirement25 = table.Column<int>(nullable: false),
                    requirement26 = table.Column<int>(nullable: false),
                    requirement27 = table.Column<int>(nullable: false),
                    requirement28 = table.Column<int>(nullable: false),
                    requirement29 = table.Column<int>(nullable: false),
                    requirement30 = table.Column<int>(nullable: false),
                    requirement31 = table.Column<int>(nullable: false),
                    requirement32 = table.Column<int>(nullable: false),
                    requirement33 = table.Column<int>(nullable: false),
                    requirement34 = table.Column<int>(nullable: false),
                    requirement35 = table.Column<int>(nullable: false),
                    requirement36 = table.Column<int>(nullable: false),
                    requirement37 = table.Column<int>(nullable: false),
                    requirement38 = table.Column<int>(nullable: false),
                    requirement39 = table.Column<int>(nullable: false),
                    requirement40 = table.Column<int>(nullable: false),
                    requirement41 = table.Column<int>(nullable: false),
                    requirement42 = table.Column<int>(nullable: false),
                    requirement43 = table.Column<int>(nullable: false),
                    requirement44 = table.Column<int>(nullable: false),
                    requirement45 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_website_requirements", x => x.id);
                    table.ForeignKey(
                        name: "FK_website_requirements_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_website_requirements_organization_id",
                schema: "organizations",
                table: "website_requirements",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "website_requirements",
                schema: "organizations");
        }
    }
}
