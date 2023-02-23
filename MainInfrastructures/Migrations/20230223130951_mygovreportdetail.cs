using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class mygovreportdetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mygov_reports_detail",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    task_id = table.Column<int>(nullable: false),
                    service_id = table.Column<int>(nullable: false),
                    service_name = table.Column<string>(nullable: true),
                    deadline_from = table.Column<string>(nullable: true),
                    deadline_to = table.Column<string>(nullable: true),
                    year = table.Column<int>(nullable: false),
                    part = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mygov_reports_detail", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mygov_reports_detail",
                schema: "organizations");
        }
    }
}
