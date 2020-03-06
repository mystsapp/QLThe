using Microsoft.EntityFrameworkCore.Migrations;

namespace QLThe.Migrations
{
    public partial class fixUsertbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Trangthai",
                table: "Users",
                newName: "TrangThai");

            migrationBuilder.RenameColumn(
                name: "Nguoitao",
                table: "Users",
                newName: "NguoiTao");

            migrationBuilder.RenameColumn(
                name: "Nguoicapnhat",
                table: "Users",
                newName: "NguoiCapNhat");

            migrationBuilder.RenameColumn(
                name: "Hoten",
                table: "Users",
                newName: "HoTen");

            migrationBuilder.AddColumn<string>(
                name: "PhongBan",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhongBan",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "TrangThai",
                table: "Users",
                newName: "Trangthai");

            migrationBuilder.RenameColumn(
                name: "NguoiTao",
                table: "Users",
                newName: "Nguoitao");

            migrationBuilder.RenameColumn(
                name: "NguoiCapNhat",
                table: "Users",
                newName: "Nguoicapnhat");

            migrationBuilder.RenameColumn(
                name: "HoTen",
                table: "Users",
                newName: "Hoten");
        }
    }
}
