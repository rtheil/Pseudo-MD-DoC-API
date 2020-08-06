using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pseudo_MD_DoC_API.Migrations
{
    public partial class changebgchecktodate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundCheckComplete",
                table: "Applications");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateBackgroundCheckComplete",
                table: "Applications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBackgroundCheckComplete",
                table: "Applications");

            migrationBuilder.AddColumn<bool>(
                name: "BackgroundCheckComplete",
                table: "Applications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
