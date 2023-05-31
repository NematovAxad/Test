using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class tablenamechange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_mygov_report",
                schema: "organizations",
                table: "mygov_report");

            migrationBuilder.RenameTable(
                name: "mygov_report",
                schema: "organizations",
                newName: "mib_report",
                newSchema: "organizations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mib_report",
                schema: "organizations",
                table: "mib_report",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_mib_report",
                schema: "organizations",
                table: "mib_report");

            migrationBuilder.RenameTable(
                name: "mib_report",
                schema: "organizations",
                newName: "mygov_report",
                newSchema: "organizations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mygov_report",
                schema: "organizations",
                table: "mygov_report",
                column: "id");
        }
    }
}
