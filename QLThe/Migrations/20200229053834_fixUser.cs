using Microsoft.EntityFrameworkCore.Migrations;

namespace QLThe.Migrations
{
    public partial class fixUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_VanPhongs_VanPhongId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_VanPhongId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VanPhongId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "ChiNhanhId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VanPhong",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChiNhanhId",
                table: "Users",
                column: "ChiNhanhId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ChiNhanhs_ChiNhanhId",
                table: "Users",
                column: "ChiNhanhId",
                principalTable: "ChiNhanhs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ChiNhanhs_ChiNhanhId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChiNhanhId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChiNhanhId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "VanPhong",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "VanPhongId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_VanPhongId",
                table: "Users",
                column: "VanPhongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_VanPhongs_VanPhongId",
                table: "Users",
                column: "VanPhongId",
                principalTable: "VanPhongs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
