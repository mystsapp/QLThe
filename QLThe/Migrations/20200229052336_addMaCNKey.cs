using Microsoft.EntityFrameworkCore.Migrations;

namespace QLThe.Migrations
{
    public partial class addMaCNKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaChiNhanh",
                table: "ChiNhanhs",
                newName: "MaCN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MaCN",
                table: "ChiNhanhs",
                newName: "MaChiNhanh");
        }
    }
}
