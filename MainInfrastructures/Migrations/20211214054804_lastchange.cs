using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class lastchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_parent",
                schema: "organizations",
                table: "regions");

            migrationBuilder.CreateTable(
                name: "content_manager",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    full_name = table.Column<string>(nullable: true),
                    position = table.Column<string>(nullable: true),
                    phone = table.Column<string>(nullable: true),
                    file_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_content_manager", x => x.id);
                    table.ForeignKey(
                        name: "FK_content_manager_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_apps",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    has_android_app = table.Column<bool>(nullable: false),
                    android_app_link = table.Column<string>(nullable: true),
                    has_ios_app = table.Column<bool>(nullable: false),
                    ios_app_link = table.Column<string>(nullable: true),
                    has_other_apps = table.Column<bool>(nullable: false),
                    other_app_link = table.Column<string>(nullable: true),
                    has_responsive_website = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_apps", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_apps_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_content_manager_organization_id",
                schema: "organizations",
                table: "content_manager",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_apps_organization_id",
                schema: "organizations",
                table: "organization_apps",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "content_manager",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "organization_apps",
                schema: "organizations");

            migrationBuilder.AddColumn<bool>(
                name: "is_parent",
                schema: "organizations",
                table: "regions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
