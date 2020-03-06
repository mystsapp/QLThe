using Microsoft.EntityFrameworkCore.Migrations;

namespace QLThe.Migrations
{
    public partial class fixMaCN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chinhanh",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "MaCN",
                table: "Users",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaCN",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Chinhanh",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
