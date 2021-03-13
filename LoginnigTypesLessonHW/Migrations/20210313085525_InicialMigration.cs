using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginnigTypesLessonHW.Migrations
{
    public partial class InicialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Files",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        FullPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        CloudPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        IsFolder = table.Column<bool>(type: "bit", nullable: false),
            //        Parent = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Files", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");
        }
    }
}
