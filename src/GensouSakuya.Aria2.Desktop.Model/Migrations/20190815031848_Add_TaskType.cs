using Microsoft.EntityFrameworkCore.Migrations;

namespace GensouSakuya.Aria2.Desktop.Model.Migrations
{
    public partial class Add_TaskType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskType",
                table: "DownloadTasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskType",
                table: "DownloadTasks");
        }
    }
}
