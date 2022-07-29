using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class helplinechanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "acceptable_response_time",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropColumn(
                name: "has_online_consultant",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropColumn(
                name: "operates_in_working_day",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropColumn(
                name: "can_give_feedback_to_helpline",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "helpline",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "official_site_has_helpline",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "official_site_has_helpline_feedback",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.AddColumn<string>(
                name: "helpline_number",
                schema: "organizations",
                table: "org_helpline",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "deadline_id",
                schema: "organizations",
                table: "helpline_info",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "helpline_phone_rating_option",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "helpline_phone_work_status",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "helpline_statistics_archiving",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "helpline_statistics_by_rank",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "helpline_statistics_by_time",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "helpline_statistics_intime",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "regulation_shows_client_rights",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "regulation_shows_phone",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "regulation_shows_replay_deadline",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "regulation_shows_request_procedure",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "regulation_shows_services",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "regulation_shows_timetable",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "regulation_verified",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "website_has_helpline_statistics",
                schema: "organizations",
                table: "helpline_info",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_organization_social_parameters_deadline_id",
                schema: "organizations",
                table: "organization_social_parameters",
                column: "deadline_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_organization_social_parameters_deadline_deadline_id",
                schema: "organizations",
                table: "organization_social_parameters",
                column: "deadline_id",
                principalSchema: "ranking",
                principalTable: "deadline",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_helpline_info_deadline_deadline_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropForeignKey(
                name: "FK_organization_social_parameters_deadline_deadline_id",
                schema: "organizations",
                table: "organization_social_parameters");

            migrationBuilder.DropIndex(
                name: "IX_organization_social_parameters_deadline_id",
                schema: "organizations",
                table: "organization_social_parameters");

            migrationBuilder.DropIndex(
                name: "IX_helpline_info_deadline_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "helpline_number",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropColumn(
                name: "deadline_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "helpline_phone_rating_option",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "helpline_phone_work_status",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "helpline_statistics_archiving",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "helpline_statistics_by_rank",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "helpline_statistics_by_time",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "helpline_statistics_intime",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "regulation_shows_client_rights",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "regulation_shows_phone",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "regulation_shows_replay_deadline",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "regulation_shows_request_procedure",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "regulation_shows_services",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "regulation_shows_timetable",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "regulation_verified",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "website_has_helpline_statistics",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.AddColumn<bool>(
                name: "acceptable_response_time",
                schema: "organizations",
                table: "org_helpline",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "has_online_consultant",
                schema: "organizations",
                table: "org_helpline",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "operates_in_working_day",
                schema: "organizations",
                table: "org_helpline",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "can_give_feedback_to_helpline",
                schema: "organizations",
                table: "helpline_info",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "helpline",
                schema: "organizations",
                table: "helpline_info",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "official_site_has_helpline",
                schema: "organizations",
                table: "helpline_info",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "official_site_has_helpline_feedback",
                schema: "organizations",
                table: "helpline_info",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
