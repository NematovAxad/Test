using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class suborgs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "organizations",
                table: "organization",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "user_service_id",
                schema: "organizations",
                table: "organization",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "sub_organization",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    parent_id = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    director_first_name = table.Column<string>(nullable: true),
                    director_last_name = table.Column<string>(nullable: true),
                    director_mid_name = table.Column<string>(nullable: true),
                    owner_type = table.Column<string>(nullable: true),
                    official_site = table.Column<string>(nullable: true),
                    contacts = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_organization", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_organization_organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sub_organization_OrganizationId",
                schema: "organizations",
                table: "sub_organization",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "sub_organization",
                schema: "organizations");

            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "organizations",
                table: "organization");

            migrationBuilder.DropColumn(
                name: "user_service_id",
                schema: "organizations",
                table: "organization");
        }
    }
}
