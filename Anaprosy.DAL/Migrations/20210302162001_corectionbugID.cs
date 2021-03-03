using Microsoft.EntityFrameworkCore.Migrations;

namespace Anaprosy.DAL.Migrations
{
    public partial class corectionbugID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Products",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ID");
        }
    }
}
