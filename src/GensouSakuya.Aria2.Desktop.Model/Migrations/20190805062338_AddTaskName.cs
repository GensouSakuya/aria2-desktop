using Microsoft.EntityFrameworkCore.Migrations;

namespace GensouSakuya.Aria2.Desktop.Model.Migrations
{
    public partial class AddTaskName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskName",
                table: "DownloadTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskName",
                table: "DownloadTasks");
        }
    }
}
