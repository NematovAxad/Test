using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class lastmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "project_status",
                schema: "organizations",
                table: "delays_on_projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "organization_computers",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    all_cmputers = table.Column<int>(nullable: false),
                    central_all_cmputers = table.Column<int>(nullable: false),
                    territorial_all_cmputers = table.Column<int>(nullable: false),
                    subordinate_all_cmputers = table.Column<int>(nullable: false),
                    working_cmputers = table.Column<int>(nullable: false),
                    central_working_cmputers = table.Column<int>(nullable: false),
                    territorial_working_cmputers = table.Column<int>(nullable: false),
                    subordinate_working_cmputers = table.Column<int>(nullable: false),
                    connected_local_set = table.Column<int>(nullable: false),
                    central_connected_local_set = table.Column<int>(nullable: false),
                    territorial_connected_local_set = table.Column<int>(nullable: false),
                    subordinate_connected_local_set = table.Column<int>(nullable: false),
                    connected_network = table.Column<int>(nullable: false),
                    central_connected_network = table.Column<int>(nullable: false),
                    territorial_connected_network = table.Column<int>(nullable: false),
                    subordinate_connected_network = table.Column<int>(nullable: false),
                    connected_corporate_network = table.Column<int>(nullable: false),
                    central_connected_corporate_network = table.Column<int>(nullable: false),
                    territorial_connected_corporate_network = table.Column<int>(nullable: false),
                    subordinate_connected_corporate_network = table.Column<int>(nullable: false),
                    connected_exat = table.Column<int>(nullable: false),
                    central_connected_exat = table.Column<int>(nullable: false),
                    territorial_connected_exat = table.Column<int>(nullable: false),
                    subordinate_connected_exat = table.Column<int>(nullable: false),
                    connected_eijro = table.Column<int>(nullable: false),
                    central_connected_eijro = table.Column<int>(nullable: false),
                    territorial_connected_eijro = table.Column<int>(nullable: false),
                    subordinate_connected_eijro = table.Column<int>(nullable: false),
                    connected_project_gov = table.Column<int>(nullable: false),
                    central_connected_project_gov = table.Column<int>(nullable: false),
                    territorial_connected_project_gov = table.Column<int>(nullable: false),
                    subordinateconnected_project_gov = table.Column<int>(nullable: false),
                    connected_project_my_work = table.Column<int>(nullable: false),
                    central_connected_project_my_work = table.Column<int>(nullable: false),
                    territorial_connected_project_my_work = table.Column<int>(nullable: false),
                    subordinate_connected_project_my_work = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_computers", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_computers_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_servers",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    server_type = table.Column<string>(nullable: true),
                    server_brand = table.Column<string>(nullable: true),
                    server_config = table.Column<string>(nullable: true),
                    server_automatic_tasks = table.Column<string>(nullable: true),
                    number_of_servers = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_servers", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_servers_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organization_computers_organization_id",
                schema: "organizations",
                table: "organization_computers",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_servers_organization_id",
                schema: "organizations",
                table: "organization_servers",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "organization_computers",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "organization_servers",
                schema: "organizations");

            migrationBuilder.DropColumn(
                name: "project_status",
                schema: "organizations",
                table: "delays_on_projects");
        }
    }
}
