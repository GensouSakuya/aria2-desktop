using Microsoft.EntityFrameworkCore.Migrations;

namespace GensouSakuya.Aria2.Desktop.Model.Migrations
{
    public partial class AddLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "DownloadTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Link",
                table: "DownloadTasks");
        }
    }
}
