﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using QLThe.Data;

namespace QLThe.Migrations
{
    [DbContext(typeof(QLTheDbContext))]
    [Migration("20200229052336_addMaCNKey")]
    partial class addMaCNKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("QLThe.Data.Models.CapThe", b =>
                {
                    b.Property<string>("MaCapThe")
                        .HasColumnType("varchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<bool?>("HoaDon")
                        .HasColumnType("bit");

                    b.Property<string>("Httt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaCN")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("MayTinh")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime?>("NgayCap")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCap")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NguoiNhan")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("TongSoLuong")
                        .HasColumnType("int");

                    b.Property<int>("VanPhongId")
                        .HasColumnType("int");

                    b.HasKey("MaCapThe");

                    b.HasIndex("VanPhongId");

                    b.ToTable("CapThes");
                });

            modelBuilder.Entity("QLThe.Data.Models.ChiNhanh", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("DienThoai")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("MaCN")
                        .IsRequired()
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("ChiNhanhs");
                });

            modelBuilder.Entity("QLThe.Data.Models.ChiTietCapThe", b =>
                {
                    b.Property<decimal>("STT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("MaCapThe")
                        .HasColumnType("varchar(20)");

                    b.Property<decimal?>("MenhGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("SoSeriDen")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("SoSeriTu")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("STT", "MaCapThe");

                    b.HasIndex("MaCapThe");

                    b.ToTable("ChiTietCapThes");
                });

            modelBuilder.Entity("QLThe.Data.Models.HTTT", b =>
                {
                    b.Property<string>("MaHT")
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("TenHT")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("MaHT");

                    b.ToTable("HTTTs");
                });

            modelBuilder.Entity("QLThe.Data.Models.LoaiThe", b =>
                {
                    b.Property<string>("MaLoai")
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Ghichu")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TenLoai")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("MaLoai");

                    b.ToTable("LoaiThes");
                });

            modelBuilder.Entity("QLThe.Data.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MieuTa")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("QLThe.Data.Models.ThongTinThe", b =>
                {
                    b.Property<string>("SoSeri")
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<decimal?>("Gia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Hinh1")
                        .HasColumnType("varchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Hinh2")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("MaCapThe")
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime?>("NgayPhatHanh")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiNhan")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NguoiPhatHanh")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NoiNhan")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Trangthai")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("SoSeri");

                    b.HasIndex("MaCapThe");

                    b.ToTable("ThongTinThes");
                });

            modelBuilder.Entity("QLThe.Data.Models.TrangThai", b =>
                {
                    b.Property<string>("MaTt")
                        .HasColumnType("varchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("GhiChu")
                        .HasColumnType("nvarchar(150)")
                        .HasMaxLength(150);

                    b.Property<string>("TenTt")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("MaTt");

                    b.ToTable("TrangThais");
                });

            modelBuilder.Entity("QLThe.Data.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("DoiMK")
                        .HasColumnType("bit");

                    b.Property<string>("HoTen")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Khoi")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("MaCN")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("NgayDoiMK")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Ngaycapnhat")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Ngaytao")
                        .HasColumnType("datetime2");

                    b.Property<string>("NguoiCapNhat")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NguoiTao")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PhongBan")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<bool>("TrangThai")
                        .HasColumnType("bit");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("VanPhongId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("VanPhongId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("QLThe.Data.Models.VanPhong", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChiNhanhId")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("DienThoai")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("ChiNhanhId");

                    b.ToTable("VanPhongs");
                });

            modelBuilder.Entity("QLThe.Models.LoginViewModel", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Doimk")
                        .HasColumnType("bit");

                    b.Property<string>("Mact")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Trangthai")
                        .HasColumnType("bit");

                    b.HasKey("Username");

                    b.ToTable("LoginViewModels");
                });

            modelBuilder.Entity("QLThe.Data.Models.CapThe", b =>
                {
                    b.HasOne("QLThe.Data.Models.VanPhong", "VanPhong")
                        .WithMany()
                        .HasForeignKey("VanPhongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QLThe.Data.Models.ChiTietCapThe", b =>
                {
                    b.HasOne("QLThe.Data.Models.CapThe", "CapThe")
                        .WithMany()
                        .HasForeignKey("MaCapThe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QLThe.Data.Models.ThongTinThe", b =>
                {
                    b.HasOne("QLThe.Data.Models.CapThe", "CapThe")
                        .WithMany()
                        .HasForeignKey("MaCapThe");
                });

            modelBuilder.Entity("QLThe.Data.Models.User", b =>
                {
                    b.HasOne("QLThe.Data.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("QLThe.Data.Models.VanPhong", "VanPhong")
                        .WithMany()
                        .HasForeignKey("VanPhongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QLThe.Data.Models.VanPhong", b =>
                {
                    b.HasOne("QLThe.Data.Models.ChiNhanh", "ChiNhanh")
                        .WithMany()
                        .HasForeignKey("ChiNhanhId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
