﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Models;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(DuAn1DbContext))]
    [Migration("20250211073358_minh02")]
    partial class minh02
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication1.Models.Account", b =>
                {
                    b.Property<int>("IdAcc")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAcc"));

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAcc");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            IdAcc = 1,
                            ConfirmPassword = "123",
                            Email = "A@gmail.com",
                            Password = "123",
                            Sdt = "0943921328",
                            UserName = "Admin"
                        },
                        new
                        {
                            IdAcc = 2,
                            ConfirmPassword = "123",
                            Email = "NV1@gmail.com",
                            Password = "123",
                            Sdt = "0987654321",
                            UserName = "NhanVien1"
                        });
                });

            modelBuilder.Entity("WebApplication1.Models.ChuongTrinhKM", b =>
                {
                    b.Property<int>("IdKhuyenMai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdKhuyenMai"));

                    b.Property<string>("NameKM")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdKhuyenMai");

                    b.ToTable("chuongTrinhKMs");
                });

            modelBuilder.Entity("WebApplication1.Models.GioHang", b =>
                {
                    b.Property<int>("GHID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GHID"));

                    b.Property<int?>("AccountIdAcc")
                        .HasColumnType("int");

                    b.Property<int?>("IdAcct")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GHID");

                    b.HasIndex("AccountIdAcc");

                    b.ToTable("GioHang");
                });

            modelBuilder.Entity("WebApplication1.Models.GioHangCT", b =>
                {
                    b.Property<int>("IdGHCT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdGHCT"));

                    b.Property<decimal>("GiaBan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("GioHangGHID")
                        .HasColumnType("int");

                    b.Property<int?>("IdAcc")
                        .HasColumnType("int");

                    b.Property<int?>("IdGH")
                        .HasColumnType("int");

                    b.Property<int?>("IdKH")
                        .HasColumnType("int");

                    b.Property<int?>("IdSP")
                        .HasColumnType("int");

                    b.Property<int?>("IdSPCT")
                        .HasColumnType("int");

                    b.Property<int?>("KhachHangIdKH")
                        .HasColumnType("int");

                    b.Property<int?>("SanPhamCTIdSPCT")
                        .HasColumnType("int");

                    b.Property<int?>("SanPhamIdSP")
                        .HasColumnType("int");

                    b.Property<int>("Soluong")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdGHCT");

                    b.HasIndex("GioHangGHID");

                    b.HasIndex("IdAcc")
                        .IsUnique()
                        .HasFilter("[IdAcc] IS NOT NULL");

                    b.HasIndex("KhachHangIdKH");

                    b.HasIndex("SanPhamCTIdSPCT");

                    b.HasIndex("SanPhamIdSP");

                    b.ToTable("gioHangCT");
                });

            modelBuilder.Entity("WebApplication1.Models.HangSanXuat", b =>
                {
                    b.Property<int>("IdHSX")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHSX"));

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameHSX")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdHSX");

                    b.ToTable("hangSanXuats");
                });

            modelBuilder.Entity("WebApplication1.Models.HoaDon", b =>
                {
                    b.Property<int>("IdHD")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHD"));

                    b.Property<decimal>("GiaBan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("HoaDonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdKH")
                        .HasColumnType("int");

                    b.Property<int?>("IdNV")
                        .HasColumnType("int");

                    b.Property<int?>("IdVouCher")
                        .HasColumnType("int");

                    b.Property<int?>("KhachHangIdKH")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayLap")
                        .HasColumnType("datetime2");

                    b.Property<int?>("NhanVienIdNV")
                        .HasColumnType("int");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VouCherIdVouCher")
                        .HasColumnType("int");

                    b.HasKey("IdHD");

                    b.HasIndex("KhachHangIdKH");

                    b.HasIndex("NhanVienIdNV");

                    b.HasIndex("VouCherIdVouCher");

                    b.ToTable("hoaDons");
                });

            modelBuilder.Entity("WebApplication1.Models.HoaDonCT", b =>
                {
                    b.Property<int>("IdHDCT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHDCT"));

                    b.Property<decimal>("GiaBan")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("HoaDonIdHD")
                        .HasColumnType("int");

                    b.Property<int?>("IdHD")
                        .HasColumnType("int");

                    b.Property<int?>("IdSPCT")
                        .HasColumnType("int");

                    b.Property<int?>("SanPhamIdSPCT")
                        .HasColumnType("int");

                    b.HasKey("IdHDCT");

                    b.HasIndex("HoaDonIdHD");

                    b.HasIndex("SanPhamIdSPCT");

                    b.ToTable("hoaDonsCT");
                });

            modelBuilder.Entity("WebApplication1.Models.KhachHang", b =>
                {
                    b.Property<int>("IdKH")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdKH"));

                    b.Property<int?>("AccountIdAcc")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdAcc")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SDT")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tuoi")
                        .HasColumnType("int");

                    b.HasKey("IdKH");

                    b.HasIndex("AccountIdAcc");

                    b.ToTable("khachHang");
                });

            modelBuilder.Entity("WebApplication1.Models.KhuyenMaiCT", b =>
                {
                    b.Property<int>("IdKMCT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdKMCT"));

                    b.Property<int?>("ChuongTrinhKMIdKhuyenMai")
                        .HasColumnType("int");

                    b.Property<int?>("IdKhuyenMai")
                        .HasColumnType("int");

                    b.Property<int?>("IdSPCT")
                        .HasColumnType("int");

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayKT")
                        .HasColumnType("datetime2");

                    b.Property<int>("PhanTramGiam")
                        .HasColumnType("int");

                    b.Property<int?>("SanPhamCTIdSPCT")
                        .HasColumnType("int");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdKMCT");

                    b.HasIndex("ChuongTrinhKMIdKhuyenMai");

                    b.HasIndex("SanPhamCTIdSPCT");

                    b.ToTable("khuyenMaiCT");
                });

            modelBuilder.Entity("WebApplication1.Models.MauSac", b =>
                {
                    b.Property<int>("IdMauSac")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMauSac"));

                    b.Property<string>("TenMau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdMauSac");

                    b.ToTable("mauSacs");
                });

            modelBuilder.Entity("WebApplication1.Models.NhanVien", b =>
                {
                    b.Property<int>("IdNV")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNV"));

                    b.Property<int?>("AccountIdAcc")
                        .HasColumnType("int");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdAcc")
                        .HasColumnType("int");

                    b.Property<string>("NameNV")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgayLamViec")
                        .HasColumnType("datetime2");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tuoi")
                        .HasColumnType("int");

                    b.HasKey("IdNV");

                    b.HasIndex("AccountIdAcc");

                    b.ToTable("nhanViens");
                });

            modelBuilder.Entity("WebApplication1.Models.SanPham", b =>
                {
                    b.Property<int>("IdSP")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSP"));

                    b.Property<string>("NameSP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TrangThai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSP");

                    b.ToTable("sanPhams");
                });

            modelBuilder.Entity("WebApplication1.Models.SanPhamCT", b =>
                {
                    b.Property<int>("IdSPCT")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSPCT"));

                    b.Property<decimal?>("Gia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("HangSanXuatIdHSX")
                        .HasColumnType("int");

                    b.Property<int?>("IdHSX")
                        .HasColumnType("int");

                    b.Property<int?>("IdMauSac")
                        .HasColumnType("int");

                    b.Property<int?>("IdSP")
                        .HasColumnType("int");

                    b.Property<int?>("IdSize")
                        .HasColumnType("int");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MauSacIdMauSac")
                        .HasColumnType("int");

                    b.Property<int?>("SanPhamIdSP")
                        .HasColumnType("int");

                    b.Property<int?>("SoluongTon")
                        .HasColumnType("int");

                    b.Property<string>("TenSp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("sizeIdSize")
                        .HasColumnType("int");

                    b.HasKey("IdSPCT");

                    b.HasIndex("HangSanXuatIdHSX");

                    b.HasIndex("MauSacIdMauSac");

                    b.HasIndex("SanPhamIdSP");

                    b.HasIndex("sizeIdSize");

                    b.ToTable("sanPhamCT");
                });

            modelBuilder.Entity("WebApplication1.Models.Size", b =>
                {
                    b.Property<int>("IdSize")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSize"));

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SizeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSize");

                    b.ToTable("sizes");
                });

            modelBuilder.Entity("WebApplication1.Models.VouCher", b =>
                {
                    b.Property<int>("IdVouCher")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVouCher"));

                    b.Property<DateTime>("NgayBatDau")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayKetThuc")
                        .HasColumnType("datetime2");

                    b.Property<int>("PhanTramGiam")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("VouCherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdVouCher");

                    b.ToTable("vouChers");
                });

            modelBuilder.Entity("WebApplication1.Models.GioHang", b =>
                {
                    b.HasOne("WebApplication1.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountIdAcc");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WebApplication1.Models.GioHangCT", b =>
                {
                    b.HasOne("WebApplication1.Models.GioHang", "GioHang")
                        .WithMany("gioHangCTs")
                        .HasForeignKey("GioHangGHID");

                    b.HasOne("WebApplication1.Models.Account", "Account")
                        .WithOne("GioHangCT")
                        .HasForeignKey("WebApplication1.Models.GioHangCT", "IdAcc");

                    b.HasOne("WebApplication1.Models.KhachHang", "KhachHang")
                        .WithMany()
                        .HasForeignKey("KhachHangIdKH");

                    b.HasOne("WebApplication1.Models.SanPhamCT", "SanPhamCT")
                        .WithMany("GioHangCTs")
                        .HasForeignKey("SanPhamCTIdSPCT");

                    b.HasOne("WebApplication1.Models.SanPham", "SanPham")
                        .WithMany("GioHangCT")
                        .HasForeignKey("SanPhamIdSP");

                    b.Navigation("Account");

                    b.Navigation("GioHang");

                    b.Navigation("KhachHang");

                    b.Navigation("SanPham");

                    b.Navigation("SanPhamCT");
                });

            modelBuilder.Entity("WebApplication1.Models.HoaDon", b =>
                {
                    b.HasOne("WebApplication1.Models.KhachHang", "KhachHang")
                        .WithMany("hoaDons")
                        .HasForeignKey("KhachHangIdKH");

                    b.HasOne("WebApplication1.Models.NhanVien", "NhanVien")
                        .WithMany("hoaDons")
                        .HasForeignKey("NhanVienIdNV");

                    b.HasOne("WebApplication1.Models.VouCher", "VouCher")
                        .WithMany("hoaDons")
                        .HasForeignKey("VouCherIdVouCher");

                    b.Navigation("KhachHang");

                    b.Navigation("NhanVien");

                    b.Navigation("VouCher");
                });

            modelBuilder.Entity("WebApplication1.Models.HoaDonCT", b =>
                {
                    b.HasOne("WebApplication1.Models.HoaDon", "HoaDon")
                        .WithMany("hoaDons")
                        .HasForeignKey("HoaDonIdHD");

                    b.HasOne("WebApplication1.Models.SanPhamCT", "SanPham")
                        .WithMany("HoaDonCTs")
                        .HasForeignKey("SanPhamIdSPCT");

                    b.Navigation("HoaDon");

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("WebApplication1.Models.KhachHang", b =>
                {
                    b.HasOne("WebApplication1.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountIdAcc");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WebApplication1.Models.KhuyenMaiCT", b =>
                {
                    b.HasOne("WebApplication1.Models.ChuongTrinhKM", "ChuongTrinhKM")
                        .WithMany("KhuyenMaiCT")
                        .HasForeignKey("ChuongTrinhKMIdKhuyenMai");

                    b.HasOne("WebApplication1.Models.SanPhamCT", "SanPhamCT")
                        .WithMany("khuyenMaiCTs")
                        .HasForeignKey("SanPhamCTIdSPCT");

                    b.Navigation("ChuongTrinhKM");

                    b.Navigation("SanPhamCT");
                });

            modelBuilder.Entity("WebApplication1.Models.NhanVien", b =>
                {
                    b.HasOne("WebApplication1.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountIdAcc");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("WebApplication1.Models.SanPhamCT", b =>
                {
                    b.HasOne("WebApplication1.Models.HangSanXuat", "HangSanXuat")
                        .WithMany("SanPhamCTs")
                        .HasForeignKey("HangSanXuatIdHSX");

                    b.HasOne("WebApplication1.Models.MauSac", "MauSac")
                        .WithMany("SanPhamCTs")
                        .HasForeignKey("MauSacIdMauSac");

                    b.HasOne("WebApplication1.Models.SanPham", "SanPham")
                        .WithMany("SanPhamCT")
                        .HasForeignKey("SanPhamIdSP");

                    b.HasOne("WebApplication1.Models.Size", "size")
                        .WithMany("SanPhamCT")
                        .HasForeignKey("sizeIdSize");

                    b.Navigation("HangSanXuat");

                    b.Navigation("MauSac");

                    b.Navigation("SanPham");

                    b.Navigation("size");
                });

            modelBuilder.Entity("WebApplication1.Models.Account", b =>
                {
                    b.Navigation("GioHangCT");
                });

            modelBuilder.Entity("WebApplication1.Models.ChuongTrinhKM", b =>
                {
                    b.Navigation("KhuyenMaiCT");
                });

            modelBuilder.Entity("WebApplication1.Models.GioHang", b =>
                {
                    b.Navigation("gioHangCTs");
                });

            modelBuilder.Entity("WebApplication1.Models.HangSanXuat", b =>
                {
                    b.Navigation("SanPhamCTs");
                });

            modelBuilder.Entity("WebApplication1.Models.HoaDon", b =>
                {
                    b.Navigation("hoaDons");
                });

            modelBuilder.Entity("WebApplication1.Models.KhachHang", b =>
                {
                    b.Navigation("hoaDons");
                });

            modelBuilder.Entity("WebApplication1.Models.MauSac", b =>
                {
                    b.Navigation("SanPhamCTs");
                });

            modelBuilder.Entity("WebApplication1.Models.NhanVien", b =>
                {
                    b.Navigation("hoaDons");
                });

            modelBuilder.Entity("WebApplication1.Models.SanPham", b =>
                {
                    b.Navigation("GioHangCT");

                    b.Navigation("SanPhamCT");
                });

            modelBuilder.Entity("WebApplication1.Models.SanPhamCT", b =>
                {
                    b.Navigation("GioHangCTs");

                    b.Navigation("HoaDonCTs");

                    b.Navigation("khuyenMaiCTs");
                });

            modelBuilder.Entity("WebApplication1.Models.Size", b =>
                {
                    b.Navigation("SanPhamCT");
                });

            modelBuilder.Entity("WebApplication1.Models.VouCher", b =>
                {
                    b.Navigation("hoaDons");
                });
#pragma warning restore 612, 618
        }
    }
}
