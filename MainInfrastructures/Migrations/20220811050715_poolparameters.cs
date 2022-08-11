﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MainInfrastructures.Migrations
{
    public partial class poolparameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pool_comment",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pool_link",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pool_screenshot_link",
                schema: "organizations",
                table: "organization_socials",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pool_comment",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "pool_link",
                schema: "organizations",
                table: "organization_socials");

            migrationBuilder.DropColumn(
                name: "pool_screenshot_link",
                schema: "organizations",
                table: "organization_socials");
        }
    }
}
