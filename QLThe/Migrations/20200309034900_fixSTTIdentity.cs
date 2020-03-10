using Microsoft.EntityFrameworkCore.Migrations;

namespace QLThe.Migrations
{
    public partial class fixSTTIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChiTietCapThes",
                table: "ChiTietCapThes");

            migrationBuilder.AddColumn<decimal>(
                name: "STT",
                table: "ChiTietCapThes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChiTietCapThes",
                table: "ChiTietCapThes",
                columns: new[] { "STT", "MaCapThe" });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietCapThes_MaCapThe",
                table: "ChiTietCapThes",
                column: "MaCapThe");
        }
    }
}
