using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MainInfrastructures.Migrations
{
    public partial class secondsectionmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "helpline_info",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    field_id = table.Column<int>(nullable: false),
                    deadline_id = table.Column<int>(nullable: false),
                    helpline = table.Column<string>(nullable: true),
                    official_site_has_helpline = table.Column<bool>(nullable: false),
                    can_give_feedback_to_helpline = table.Column<bool>(nullable: false),
                    official_site_has_helpline_feedback = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_helpline_info", x => x.id);
                    table.ForeignKey(
                        name: "FK_helpline_info_deadline_deadline_id",
                        column: x => x.deadline_id,
                        principalSchema: "ranking",
                        principalTable: "deadline",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_helpline_info_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_helpline_info_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "org_data_filler",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    field_id = table.Column<int>(nullable: false),
                    deadline_id = table.Column<int>(nullable: false),
                    full_name = table.Column<string>(nullable: true),
                    position = table.Column<string>(nullable: true),
                    contacts = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_data_filler", x => x.id);
                    table.ForeignKey(
                        name: "FK_org_data_filler_deadline_deadline_id",
                        column: x => x.deadline_id,
                        principalSchema: "ranking",
                        principalTable: "deadline",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_org_data_filler_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_org_data_filler_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "org_helpline",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    field_id = table.Column<int>(nullable: false),
                    deadline_id = table.Column<int>(nullable: false),
                    has_online_consultant = table.Column<bool>(nullable: false),
                    operates_in_working_day = table.Column<bool>(nullable: false),
                    acceptable_response_time = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_org_helpline", x => x.id);
                    table.ForeignKey(
                        name: "FK_org_helpline_deadline_deadline_id",
                        column: x => x.deadline_id,
                        principalSchema: "ranking",
                        principalTable: "deadline",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_org_helpline_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_org_helpline_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organization_messengers",
                schema: "organizations",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(nullable: false),
                    field_id = table.Column<int>(nullable: false),
                    deadline_id = table.Column<int>(nullable: false),
                    messenger_link = table.Column<string>(nullable: true),
                    reason_not_filling = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organization_messengers", x => x.id);
                    table.ForeignKey(
                        name: "FK_organization_messengers_deadline_deadline_id",
                        column: x => x.deadline_id,
                        principalSchema: "ranking",
                        principalTable: "deadline",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organization_messengers_field_field_id",
                        column: x => x.field_id,
                        principalSchema: "ranking",
                        principalTable: "field",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_organization_messengers_organization_organization_id",
                        column: x => x.organization_id,
                        principalSchema: "organizations",
                        principalTable: "organization",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_helpline_info_organization_id",
                schema: "organizations",
                table: "helpline_info",
                column: "organization_id");

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
                name: "IX_org_data_filler_organization_id",
                schema: "organizations",
                table: "org_data_filler",
                column: "organization_id");

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
                name: "IX_org_helpline_organization_id",
                schema: "organizations",
                table: "org_helpline",
                column: "organization_id");

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
                name: "IX_organization_messengers_organization_id",
                schema: "organizations",
                table: "organization_messengers",
                column: "organization_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "helpline_info",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "org_data_filler",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "org_helpline",
                schema: "organizations");

            migrationBuilder.DropTable(
                name: "organization_messengers",
                schema: "organizations");
        }
    }
}
