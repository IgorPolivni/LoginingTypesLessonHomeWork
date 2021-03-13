using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginnigTypesLessonHW.Migrations
{
    public partial class RenameFileName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CloudPath",
                table: "Files",
                newName: "FileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Files",
                newName: "CloudPath");
        }
    }
}
