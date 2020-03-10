using Microsoft.EntityFrameworkCore.Migrations;

namespace QLThe.Migrations
{
    public partial class addNewSTTIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietCapThes",
                table: "ChiTietCapThes");

            migrationBuilder.AddColumn<decimal>(
                name: "STT",
                table: "ChiTietCapThes",
                type: "decimal(18, 0)",
                nullable: false,
                defaultValue: 0m)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietCapThes",
                table: "ChiTietCapThes",
                columns: new[] { "STT", "MaCapThe" });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietCapThes_MaCapThe",
                table: "ChiTietCapThes",
                column: "MaCapThe");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietCapThes",
                table: "ChiTietCapThes");

            migrationBuilder.DropIndex(
                name: "IX_ChiTietCapThes_MaCapThe",
                table: "ChiTietCapThes");

            migrationBuilder.DropColumn(
                name: "STT",
                table: "ChiTietCapThes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietCapThes",
                table: "ChiTietCapThes",
                column: "MaCapThe");
        }
    }
}
