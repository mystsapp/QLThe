using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QLThe.Migrations
{
    public partial class initialDb1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChiNhanhs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaChiNhanh = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DienThoai = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiNhanhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HTTTs",
                columns: table => new
                {
                    MaHT = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    TenHT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HTTTs", x => x.MaHT);
                });

            migrationBuilder.CreateTable(
                name: "LoaiThes",
                columns: table => new
                {
                    MaLoai = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    TenLoai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ghichu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiThes", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MieuTa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrangThais",
                columns: table => new
                {
                    MaTt = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: false),
                    TenTt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangThais", x => x.MaTt);
                });

            migrationBuilder.CreateTable(
                name: "VanPhongs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DienThoai = table.Column<string>(maxLength: 15, nullable: true),
                    ChiNhanhId = table.Column<int>(nullable: false),
                    KhuVuc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VanPhongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VanPhongs_ChiNhanhs_ChiNhanhId",
                        column: x => x.ChiNhanhId,
                        principalTable: "ChiNhanhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CapThes",
                columns: table => new
                {
                    MaCapThe = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    NguoiCap = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayCap = table.Column<DateTime>(nullable: true),
                    MaCN = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    VanPhongId = table.Column<int>(nullable: false),
                    NguoiNhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TongSoLuong = table.Column<int>(nullable: true),
                    Httt = table.Column<string>(nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    HoaDon = table.Column<bool>(nullable: true),
                    MayTinh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CapThes", x => x.MaCapThe);
                    table.ForeignKey(
                        name: "FK_CapThes_VanPhongs_VanPhongId",
                        column: x => x.VanPhongId,
                        principalTable: "VanPhongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Hoten = table.Column<string>(maxLength: 50, nullable: false),
                    Chinhanh = table.Column<string>(nullable: true),
                    VanPhongId = table.Column<int>(nullable: false),
                    Doimatkhau = table.Column<bool>(nullable: false),
                    Ngaydoimk = table.Column<DateTime>(nullable: true),
                    Trangthai = table.Column<bool>(nullable: false),
                    Khoi = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Nguoitao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ngaytao = table.Column<DateTime>(nullable: true),
                    Nguoicapnhat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ngaycapnhat = table.Column<DateTime>(nullable: true),
                    Nhom = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_VanPhongs_VanPhongId",
                        column: x => x.VanPhongId,
                        principalTable: "VanPhongs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietCapThes",
                columns: table => new
                {
                    STT = table.Column<decimal>(nullable: false),
                    MaCapThe = table.Column<string>(nullable: false),
                    SoSeriTu = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    SoSeriDen = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: true),
                    MenhGia = table.Column<decimal>(nullable: true),
                    SoLuong = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietCapThes", x => new { x.STT, x.MaCapThe });
                    table.ForeignKey(
                        name: "FK_ChiTietCapThes_CapThes_MaCapThe",
                        column: x => x.MaCapThe,
                        principalTable: "CapThes",
                        principalColumn: "MaCapThe",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThongTinThes",
                columns: table => new
                {
                    SoSeri = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    MaCapThe = table.Column<string>(nullable: true),
                    Gia = table.Column<decimal>(nullable: true),
                    NguoiPhatHanh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayPhatHanh = table.Column<DateTime>(nullable: true),
                    NoiNhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NguoiNhan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Trangthai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hinh1 = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Hinh2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GhiChu = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThongTinThes", x => x.SoSeri);
                    table.ForeignKey(
                        name: "FK_ThongTinThes_CapThes_MaCapThe",
                        column: x => x.MaCapThe,
                        principalTable: "CapThes",
                        principalColumn: "MaCapThe",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CapThes_VanPhongId",
                table: "CapThes",
                column: "VanPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietCapThes_MaCapThe",
                table: "ChiTietCapThes",
                column: "MaCapThe");

            migrationBuilder.CreateIndex(
                name: "IX_ThongTinThes_MaCapThe",
                table: "ThongTinThes",
                column: "MaCapThe");

            migrationBuilder.CreateIndex(
                name: "IX_Users_VanPhongId",
                table: "Users",
                column: "VanPhongId");

            migrationBuilder.CreateIndex(
                name: "IX_VanPhongs_ChiNhanhId",
                table: "VanPhongs",
                column: "ChiNhanhId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietCapThes");

            migrationBuilder.DropTable(
                name: "HTTTs");

            migrationBuilder.DropTable(
                name: "LoaiThes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "ThongTinThes");

            migrationBuilder.DropTable(
                name: "TrangThais");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CapThes");

            migrationBuilder.DropTable(
                name: "VanPhongs");

            migrationBuilder.DropTable(
                name: "ChiNhanhs");
        }
    }
}
