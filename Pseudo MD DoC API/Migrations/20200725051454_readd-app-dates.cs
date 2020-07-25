using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pseudo_MD_DoC_API.Migrations
{
    public partial class readdappdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateProcessed",
                table: "Applications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReceived",
                table: "Applications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateProcessed",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "DateReceived",
                table: "Applications");
        }
    }
}
