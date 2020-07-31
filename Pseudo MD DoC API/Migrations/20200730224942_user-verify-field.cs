using Microsoft.EntityFrameworkCore.Migrations;

namespace Pseudo_MD_DoC_API.Migrations
{
    public partial class userverifyfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AccountVerified",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountVerified",
                table: "Users");
        }
    }
}
