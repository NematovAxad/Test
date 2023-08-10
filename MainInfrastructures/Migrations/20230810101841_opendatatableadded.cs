using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class opendatatableadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "opendata");

            migrationBuilder.CreateTable(
                name: "open_data_table",
                schema: "opendata",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    table_id = table.Column<string>(nullable: true),
                    table_name = table.Column<string>(nullable: true),
                    update_date = table.Column<DateTime>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    link = table.Column<string>(nullable: true),
                    table_last_update_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_open_data_table", x => x.id);
                    table.ForeignKey(
                        name: "FK_open_data_table_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_open_data_table_organization_id",
                schema: "opendata",
                table: "open_data_table",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "open_data_table",
                schema: "opendata");
        }
    }
}
