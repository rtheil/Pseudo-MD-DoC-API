using Microsoft.EntityFrameworkCore.Migrations;

namespace Pseudo_MD_DoC_API.Migrations
{
    public partial class changeappStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationStatus",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationStatusId",
                table: "Applications",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "ApplicationStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationStatus", x => x.Id);
                });
            
            //ADD DEFAULT VALUES FOR APPLICATIONSTATUS TABLE
            migrationBuilder.Sql("insert into ApplicationStatus (Status) values ('New')");
            migrationBuilder.Sql("insert into ApplicationStatus (Status) values ('Background Check Pending')");
            migrationBuilder.Sql("insert into ApplicationStatus (Status) values ('Test Pending')");
            migrationBuilder.Sql("insert into ApplicationStatus (Status) values ('Failed Test')");
            migrationBuilder.Sql("insert into ApplicationStatus (Status) values ('Offer')");
            migrationBuilder.Sql("insert into ApplicationStatus (Status) values ('Accepted')");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationStatusId",
                table: "Applications",
                column: "ApplicationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_ApplicationStatus_ApplicationStatusId",
                table: "Applications",
                column: "ApplicationStatusId",
                principalTable: "ApplicationStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_ApplicationStatus_ApplicationStatusId",
                table: "Applications");

            migrationBuilder.DropTable(
                name: "ApplicationStatus");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ApplicationStatusId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "ApplicationStatusId",
                table: "Applications");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationStatus",
                table: "Applications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
