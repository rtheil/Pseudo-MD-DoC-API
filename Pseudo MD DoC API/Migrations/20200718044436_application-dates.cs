using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pseudo_MD_DoC_API.Migrations
{
    public partial class applicationdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone",
                table: "Employment",
                newName: "Phone");

            migrationBuilder.AlterColumn<string>(
                name: "SocialSecurityNumber",
                table: "Applications",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReceived",
                table: "Applications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateReceived",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Employment",
                newName: "phone");

            migrationBuilder.AlterColumn<string>(
                name: "SocialSecurityNumber",
                table: "Applications",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 9);
        }
    }
}
