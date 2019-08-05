using Microsoft.EntityFrameworkCore.Migrations;

namespace GensouSakuya.Aria2.Desktop.Model.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DownloadTasks",
                columns: table => new
                {
                    GID = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    TotalLength = table.Column<long>(nullable: false),
                    CompletedLength = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadTasks", x => x.GID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownloadTasks");
        }
    }
}
