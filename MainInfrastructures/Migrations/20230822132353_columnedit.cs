using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class columnedit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "pool_except_expert",
                schema: "organizations",
                table: "organization_socials",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "pool_except_expert",
                schema: "organizations",
                table: "organization_socials",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
