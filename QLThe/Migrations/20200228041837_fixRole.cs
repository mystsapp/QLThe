using Microsoft.EntityFrameworkCore.Migrations;

namespace QLThe.Migrations
{
    public partial class fixRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KhuVuc",
                table: "VanPhongs");

            migrationBuilder.DropColumn(
                name: "Nhom",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "VanPhongs",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LoginViewModels",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    Mact = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: false),
                    Trangthai = table.Column<bool>(nullable: false),
                    Doimk = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginViewModels", x => x.Username);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "LoginViewModels");

            migrationBuilder.DropIndex(
                name: "IX_Users_RoleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "VanPhongs");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "KhuVuc",
                table: "VanPhongs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nhom",
                table: "Users",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
