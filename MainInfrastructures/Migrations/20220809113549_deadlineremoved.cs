using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class deadlineremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_helpline_info_deadline_deadline_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropIndex(
                name: "IX_helpline_info_deadline_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "deadline_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment10",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment11",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment12",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment13",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment14",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment2",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment3",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment4",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment5",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment6",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment7",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment8",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment9",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot10_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot11_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot12_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot13_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot14_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot2_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot3_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot4_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot5_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot6_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot7_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot8_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot9_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "screenshot_link",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment10",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment11",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment12",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment13",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment14",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment2",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment3",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment4",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment5",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment6",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment7",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment8",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "Comment9",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot10_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot11_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot12_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot13_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot14_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot2_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot3_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot4_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot5_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot6_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot7_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot8_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot9_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "screenshot_link",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.AddColumn<int>(
                name: "deadline_id",
                schema: "organizations",
                table: "helpline_info",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_helpline_info_deadline_id",
                schema: "organizations",
                table: "helpline_info",
                column: "deadline_id");

            migrationBuilder.AddForeignKey(
                name: "FK_helpline_info_deadline_deadline_id",
                schema: "organizations",
                table: "helpline_info",
                column: "deadline_id",
                principalSchema: "ranking",
                principalTable: "deadline",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
