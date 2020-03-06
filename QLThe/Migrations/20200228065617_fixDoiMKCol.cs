using Microsoft.EntityFrameworkCore.Migrations;

namespace QLThe.Migrations
{
    public partial class fixDoiMKCol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doimatkhau",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Ngaydoimk",
                table: "Users",
                newName: "NgayDoiMK");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "DoiMK",
                table: "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoiMK",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "NgayDoiMK",
                table: "Users",
                newName: "Ngaydoimk");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<bool>(
                name: "Doimatkhau",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
