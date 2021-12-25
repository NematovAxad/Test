using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class lastchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_helpline_info_deadline_deadline_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropForeignKey(
                name: "FK_helpline_info_field_field_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropForeignKey(
                name: "FK_org_data_filler_deadline_deadline_id",
                schema: "organizations",
                table: "org_data_filler");

            migrationBuilder.DropForeignKey(
                name: "FK_org_data_filler_field_field_id",
                schema: "organizations",
                table: "org_data_filler");

            migrationBuilder.DropForeignKey(
                name: "FK_org_helpline_deadline_deadline_id",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropForeignKey(
                name: "FK_org_helpline_field_field_id",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropForeignKey(
                name: "FK_org_social_sites_deadline_deadline_id",
                schema: "organizations",
                table: "org_social_sites");

            migrationBuilder.DropForeignKey(
                name: "FK_org_social_sites_field_field_id",
                schema: "organizations",
                table: "org_social_sites");

            migrationBuilder.DropForeignKey(
                name: "FK_organization_messengers_deadline_deadline_id",
                schema: "organizations",
                table: "organization_messengers");

            migrationBuilder.DropForeignKey(
                name: "FK_organization_messengers_field_field_id",
                schema: "organizations",
                table: "organization_messengers");

            migrationBuilder.DropIndex(
                name: "IX_organization_messengers_deadline_id",
                schema: "organizations",
                table: "organization_messengers");

            migrationBuilder.DropIndex(
                name: "IX_organization_messengers_field_id",
                schema: "organizations",
                table: "organization_messengers");

            migrationBuilder.DropIndex(
                name: "IX_org_social_sites_deadline_id",
                schema: "organizations",
                table: "org_social_sites");

            migrationBuilder.DropIndex(
                name: "IX_org_social_sites_field_id",
                schema: "organizations",
                table: "org_social_sites");

            migrationBuilder.DropIndex(
                name: "IX_org_helpline_deadline_id",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropIndex(
                name: "IX_org_helpline_field_id",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropIndex(
                name: "IX_org_data_filler_deadline_id",
                schema: "organizations",
                table: "org_data_filler");

            migrationBuilder.DropIndex(
                name: "IX_org_data_filler_field_id",
                schema: "organizations",
                table: "org_data_filler");

            migrationBuilder.DropIndex(
                name: "IX_helpline_info_deadline_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropIndex(
                name: "IX_helpline_info_field_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "deadline_id",
                schema: "organizations",
                table: "organization_messengers");

            migrationBuilder.DropColumn(
                name: "field_id",
                schema: "organizations",
                table: "organization_messengers");

            migrationBuilder.DropColumn(
                name: "deadline_id",
                schema: "organizations",
                table: "org_social_sites");

            migrationBuilder.DropColumn(
                name: "field_id",
                schema: "organizations",
                table: "org_social_sites");

            migrationBuilder.DropColumn(
                name: "deadline_id",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropColumn(
                name: "field_id",
                schema: "organizations",
                table: "org_helpline");

            migrationBuilder.DropColumn(
                name: "deadline_id",
                schema: "organizations",
                table: "org_data_filler");

            migrationBuilder.DropColumn(
                name: "field_id",
                schema: "organizations",
                table: "org_data_filler");

            migrationBuilder.DropColumn(
                name: "deadline_id",
                schema: "organizations",
                table: "helpline_info");

            migrationBuilder.DropColumn(
                name: "field_id",
                schema: "organizations",
                table: "helpline_info");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "deadline_id",
                schema: "organizations",
                table: "organization_messengers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "field_id",
                schema: "organizations",
                table: "organization_messengers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "deadline_id",
                schema: "organizations",
                table: "org_social_sites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "field_id",
                schema: "organizations",
                table: "org_social_sites",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "deadline_id",
                schema: "organizations",
                table: "org_helpline",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "field_id",
                schema: "organizations",
                table: "org_helpline",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "deadline_id",
                schema: "organizations",
                table: "org_data_filler",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "field_id",
                schema: "organizations",
                table: "org_data_filler",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "deadline_id",
                schema: "organizations",
                table: "helpline_info",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "field_id",
                schema: "organizations",
                table: "helpline_info",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_organization_messengers_deadline_id",
                schema: "organizations",
                table: "organization_messengers",
                column: "deadline_id");

            migrationBuilder.CreateIndex(
                name: "IX_organization_messengers_field_id",
                schema: "organizations",
                table: "organization_messengers",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_org_social_sites_deadline_id",
                schema: "organizations",
                table: "org_social_sites",
                column: "deadline_id");

            migrationBuilder.CreateIndex(
                name: "IX_org_social_sites_field_id",
                schema: "organizations",
                table: "org_social_sites",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_org_helpline_deadline_id",
                schema: "organizations",
                table: "org_helpline",
                column: "deadline_id");

            migrationBuilder.CreateIndex(
                name: "IX_org_helpline_field_id",
                schema: "organizations",
                table: "org_helpline",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_org_data_filler_deadline_id",
                schema: "organizations",
                table: "org_data_filler",
                column: "deadline_id");

            migrationBuilder.CreateIndex(
                name: "IX_org_data_filler_field_id",
                schema: "organizations",
                table: "org_data_filler",
                column: "field_id");

            migrationBuilder.CreateIndex(
                name: "IX_helpline_info_deadline_id",
                schema: "organizations",
                table: "helpline_info",
                column: "deadline_id");

            migrationBuilder.CreateIndex(
                name: "IX_helpline_info_field_id",
                schema: "organizations",
                table: "helpline_info",
                column: "field_id");

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
                name: "FK_helpline_info_field_field_id",
                schema: "organizations",
                table: "helpline_info",
                column: "field_id",
                principalSchema: "ranking",
                principalTable: "field",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_org_data_filler_deadline_deadline_id",
                schema: "organizations",
                table: "org_data_filler",
                column: "deadline_id",
                principalSchema: "ranking",
                principalTable: "deadline",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_org_data_filler_field_field_id",
                schema: "organizations",
                table: "org_data_filler",
                column: "field_id",
                principalSchema: "ranking",
                principalTable: "field",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_org_helpline_deadline_deadline_id",
                schema: "organizations",
                table: "org_helpline",
                column: "deadline_id",
                principalSchema: "ranking",
                principalTable: "deadline",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_org_helpline_field_field_id",
                schema: "organizations",
                table: "org_helpline",
                column: "field_id",
                principalSchema: "ranking",
                principalTable: "field",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_org_social_sites_deadline_deadline_id",
                schema: "organizations",
                table: "org_social_sites",
                column: "deadline_id",
                principalSchema: "ranking",
                principalTable: "deadline",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_org_social_sites_field_field_id",
                schema: "organizations",
                table: "org_social_sites",
                column: "field_id",
                principalSchema: "ranking",
                principalTable: "field",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_organization_messengers_deadline_deadline_id",
                schema: "organizations",
                table: "organization_messengers",
                column: "deadline_id",
                principalSchema: "ranking",
                principalTable: "deadline",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_organization_messengers_field_field_id",
                schema: "organizations",
                table: "organization_messengers",
                column: "field_id",
                principalSchema: "ranking",
                principalTable: "field",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
