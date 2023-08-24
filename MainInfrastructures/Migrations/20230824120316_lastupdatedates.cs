using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class lastupdatedates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_position",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_position",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_identities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_expert_decsion",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_expert_decsion",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_efficiency",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_cyber_security_expert_decsion",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_cyber_security_expert_decsion",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_connection",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_connection",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_classifications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_classifications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_automated_services",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_automated_services",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_authorizations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_authorizations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "project_identities",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "project_identities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "project_efficiency",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "project_efficiency",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "project_connections",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "project_connections",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "project_classifications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "project_classifications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "project_authorizations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "project_authorizations",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "automated_services",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "automated_services",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_update",
                schema: "reestrprojects",
                table: "automated_functions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "automated_functions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_position");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_position");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_identities");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_identities");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_expert_decsion");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_expert_decsion");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_efficiency");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_cyber_security_expert_decsion");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_cyber_security_expert_decsion");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_connection");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_connection");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_classifications");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_classifications");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_automated_services");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_automated_services");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "reestr_project_authorizations");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "reestr_project_authorizations");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "project_identities");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "project_identities");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "project_efficiency");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "project_efficiency");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "project_connections");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "project_connections");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "project_classifications");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "project_classifications");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "project_authorizations");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "project_authorizations");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "automated_services");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "automated_services");

            migrationBuilder.DropColumn(
                name: "last_update",
                schema: "reestrprojects",
                table: "automated_functions");

            migrationBuilder.DropColumn(
                name: "user_pinfl",
                schema: "reestrprojects",
                table: "automated_functions");
        }
    }
}
