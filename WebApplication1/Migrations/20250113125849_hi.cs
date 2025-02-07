using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class hi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chuongTrinhKMs",
                columns: table => new
                {
                    IdKhuyenMai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameKM = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chuongTrinhKMs", x => x.IdKhuyenMai);
                });

            migrationBuilder.CreateTable(
                name: "hangSanXuats",
                columns: table => new
                {
                    IdHSX = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameHSX = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hangSanXuats", x => x.IdHSX);
                });

            migrationBuilder.CreateTable(
                name: "mauSacs",
                columns: table => new
                {
                    IdMauSac = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMau = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mauSacs", x => x.IdMauSac);
                });

            migrationBuilder.CreateTable(
                name: "sanPhams",
                columns: table => new
                {
                    IdSP = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameSP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanPhams", x => x.IdSP);
                });

            migrationBuilder.CreateTable(
                name: "sizes",
                columns: table => new
                {
                    IdSize = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sizes", x => x.IdSize);
                });

            migrationBuilder.CreateTable(
                name: "vouChers",
                columns: table => new
                {
                    IdVouCher = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VouCherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vouChers", x => x.IdVouCher);
                });

            migrationBuilder.CreateTable(
                name: "sanPhamCT",
                columns: table => new
                {
                    IdSPCT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoluongTon = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdSP = table.Column<int>(type: "int", nullable: true),
                    IdSize = table.Column<int>(type: "int", nullable: true),
                    IdMauSac = table.Column<int>(type: "int", nullable: true),
                    IdHSX = table.Column<int>(type: "int", nullable: true),
                    MauSacIdMauSac = table.Column<int>(type: "int", nullable: true),
                    HangSanXuatIdHSX = table.Column<int>(type: "int", nullable: true),
                    sizeIdSize = table.Column<int>(type: "int", nullable: true),
                    SanPhamIdSP = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sanPhamCT", x => x.IdSPCT);
                    table.ForeignKey(
                        name: "FK_sanPhamCT_hangSanXuats_HangSanXuatIdHSX",
                        column: x => x.HangSanXuatIdHSX,
                        principalTable: "hangSanXuats",
                        principalColumn: "IdHSX");
                    table.ForeignKey(
                        name: "FK_sanPhamCT_mauSacs_MauSacIdMauSac",
                        column: x => x.MauSacIdMauSac,
                        principalTable: "mauSacs",
                        principalColumn: "IdMauSac");
                    table.ForeignKey(
                        name: "FK_sanPhamCT_sanPhams_SanPhamIdSP",
                        column: x => x.SanPhamIdSP,
                        principalTable: "sanPhams",
                        principalColumn: "IdSP");
                    table.ForeignKey(
                        name: "FK_sanPhamCT_sizes_sizeIdSize",
                        column: x => x.sizeIdSize,
                        principalTable: "sizes",
                        principalColumn: "IdSize");
                });

            migrationBuilder.CreateTable(
                name: "khuyenMaiCT",
                columns: table => new
                {
                    IdKMCT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhanTramGiam = table.Column<int>(type: "int", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdKhuyenMai = table.Column<int>(type: "int", nullable: true),
                    IdSPCT = table.Column<int>(type: "int", nullable: true),
                    ChuongTrinhKMIdKhuyenMai = table.Column<int>(type: "int", nullable: true),
                    SanPhamCTIdSPCT = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khuyenMaiCT", x => x.IdKMCT);
                    table.ForeignKey(
                        name: "FK_khuyenMaiCT_chuongTrinhKMs_ChuongTrinhKMIdKhuyenMai",
                        column: x => x.ChuongTrinhKMIdKhuyenMai,
                        principalTable: "chuongTrinhKMs",
                        principalColumn: "IdKhuyenMai");
                    table.ForeignKey(
                        name: "FK_khuyenMaiCT_sanPhamCT_SanPhamCTIdSPCT",
                        column: x => x.SanPhamCTIdSPCT,
                        principalTable: "sanPhamCT",
                        principalColumn: "IdSPCT");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    IdAcc = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioHangCTIdGHCT = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.IdAcc);
                });

            migrationBuilder.CreateTable(
                name: "khachHang",
                columns: table => new
                {
                    IdKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tuoi = table.Column<int>(type: "int", nullable: false),
                    IdAcc = table.Column<int>(type: "int", nullable: true),
                    AccountIdAcc = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khachHang", x => x.IdKH);
                    table.ForeignKey(
                        name: "FK_khachHang_Accounts_AccountIdAcc",
                        column: x => x.AccountIdAcc,
                        principalTable: "Accounts",
                        principalColumn: "IdAcc");
                });

            migrationBuilder.CreateTable(
                name: "nhanViens",
                columns: table => new
                {
                    IdNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameNV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tuoi = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayLamViec = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdAcc = table.Column<int>(type: "int", nullable: true),
                    AccountIdAcc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhanViens", x => x.IdNV);
                    table.ForeignKey(
                        name: "FK_nhanViens_Accounts_AccountIdAcc",
                        column: x => x.AccountIdAcc,
                        principalTable: "Accounts",
                        principalColumn: "IdAcc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gioHangCT",
                columns: table => new
                {
                    IdGHCT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Soluong = table.Column<int>(type: "int", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdKH = table.Column<int>(type: "int", nullable: true),
                    IdSPCT = table.Column<int>(type: "int", nullable: true),
                    IdSP = table.Column<int>(type: "int", nullable: true),
                    KhachHangIdKH = table.Column<int>(type: "int", nullable: true),
                    SanPhamCTIdSPCT = table.Column<int>(type: "int", nullable: true),
                    SanPhamIdSP = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gioHangCT", x => x.IdGHCT);
                    table.ForeignKey(
                        name: "FK_gioHangCT_khachHang_KhachHangIdKH",
                        column: x => x.KhachHangIdKH,
                        principalTable: "khachHang",
                        principalColumn: "IdKH");
                    table.ForeignKey(
                        name: "FK_gioHangCT_sanPhamCT_SanPhamCTIdSPCT",
                        column: x => x.SanPhamCTIdSPCT,
                        principalTable: "sanPhamCT",
                        principalColumn: "IdSPCT");
                    table.ForeignKey(
                        name: "FK_gioHangCT_sanPhams_SanPhamIdSP",
                        column: x => x.SanPhamIdSP,
                        principalTable: "sanPhams",
                        principalColumn: "IdSP");
                });

            migrationBuilder.CreateTable(
                name: "hoaDons",
                columns: table => new
                {
                    IdHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoaDonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdNV = table.Column<int>(type: "int", nullable: true),
                    IdKH = table.Column<int>(type: "int", nullable: true),
                    IdVouCher = table.Column<int>(type: "int", nullable: true),
                    NhanVienIdNV = table.Column<int>(type: "int", nullable: true),
                    KhachHangIdKH = table.Column<int>(type: "int", nullable: true),
                    VouCherIdVouCher = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoaDons", x => x.IdHD);
                    table.ForeignKey(
                        name: "FK_hoaDons_khachHang_KhachHangIdKH",
                        column: x => x.KhachHangIdKH,
                        principalTable: "khachHang",
                        principalColumn: "IdKH");
                    table.ForeignKey(
                        name: "FK_hoaDons_nhanViens_NhanVienIdNV",
                        column: x => x.NhanVienIdNV,
                        principalTable: "nhanViens",
                        principalColumn: "IdNV");
                    table.ForeignKey(
                        name: "FK_hoaDons_vouChers_VouCherIdVouCher",
                        column: x => x.VouCherIdVouCher,
                        principalTable: "vouChers",
                        principalColumn: "IdVouCher");
                });

            migrationBuilder.CreateTable(
                name: "hoaDonsCT",
                columns: table => new
                {
                    IdHDCT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiaBan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdHD = table.Column<int>(type: "int", nullable: true),
                    IdSPCT = table.Column<int>(type: "int", nullable: true),
                    HoaDonIdHD = table.Column<int>(type: "int", nullable: true),
                    SanPhamIdSPCT = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hoaDonsCT", x => x.IdHDCT);
                    table.ForeignKey(
                        name: "FK_hoaDonsCT_hoaDons_HoaDonIdHD",
                        column: x => x.HoaDonIdHD,
                        principalTable: "hoaDons",
                        principalColumn: "IdHD");
                    table.ForeignKey(
                        name: "FK_hoaDonsCT_sanPhamCT_SanPhamIdSPCT",
                        column: x => x.SanPhamIdSPCT,
                        principalTable: "sanPhamCT",
                        principalColumn: "IdSPCT");
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "IdAcc", "ConfirmPassword", "Email", "GioHangCTIdGHCT", "Password", "Sdt", "UserName" },
                values: new object[,]
                {
                    { 1, "123", "A@gmail.com", null, "123", "0943921328", "Admin" },
                    { 2, "123", "NV1@gmail.com", null, "123", "0987654321", "NhanVien1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_GioHangCTIdGHCT",
                table: "Accounts",
                column: "GioHangCTIdGHCT");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangCT_KhachHangIdKH",
                table: "gioHangCT",
                column: "KhachHangIdKH");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangCT_SanPhamCTIdSPCT",
                table: "gioHangCT",
                column: "SanPhamCTIdSPCT");

            migrationBuilder.CreateIndex(
                name: "IX_gioHangCT_SanPhamIdSP",
                table: "gioHangCT",
                column: "SanPhamIdSP");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_KhachHangIdKH",
                table: "hoaDons",
                column: "KhachHangIdKH");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_NhanVienIdNV",
                table: "hoaDons",
                column: "NhanVienIdNV");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDons_VouCherIdVouCher",
                table: "hoaDons",
                column: "VouCherIdVouCher");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonsCT_HoaDonIdHD",
                table: "hoaDonsCT",
                column: "HoaDonIdHD");

            migrationBuilder.CreateIndex(
                name: "IX_hoaDonsCT_SanPhamIdSPCT",
                table: "hoaDonsCT",
                column: "SanPhamIdSPCT");

            migrationBuilder.CreateIndex(
                name: "IX_khachHang_AccountIdAcc",
                table: "khachHang",
                column: "AccountIdAcc");

            migrationBuilder.CreateIndex(
                name: "IX_khuyenMaiCT_ChuongTrinhKMIdKhuyenMai",
                table: "khuyenMaiCT",
                column: "ChuongTrinhKMIdKhuyenMai");

            migrationBuilder.CreateIndex(
                name: "IX_khuyenMaiCT_SanPhamCTIdSPCT",
                table: "khuyenMaiCT",
                column: "SanPhamCTIdSPCT");

            migrationBuilder.CreateIndex(
                name: "IX_nhanViens_AccountIdAcc",
                table: "nhanViens",
                column: "AccountIdAcc");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhamCT_HangSanXuatIdHSX",
                table: "sanPhamCT",
                column: "HangSanXuatIdHSX");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhamCT_MauSacIdMauSac",
                table: "sanPhamCT",
                column: "MauSacIdMauSac");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhamCT_SanPhamIdSP",
                table: "sanPhamCT",
                column: "SanPhamIdSP");

            migrationBuilder.CreateIndex(
                name: "IX_sanPhamCT_sizeIdSize",
                table: "sanPhamCT",
                column: "sizeIdSize");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_gioHangCT_GioHangCTIdGHCT",
                table: "Accounts",
                column: "GioHangCTIdGHCT",
                principalTable: "gioHangCT",
                principalColumn: "IdGHCT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_gioHangCT_GioHangCTIdGHCT",
                table: "Accounts");

            migrationBuilder.DropTable(
                name: "hoaDonsCT");

            migrationBuilder.DropTable(
                name: "khuyenMaiCT");

            migrationBuilder.DropTable(
                name: "hoaDons");

            migrationBuilder.DropTable(
                name: "chuongTrinhKMs");

            migrationBuilder.DropTable(
                name: "nhanViens");

            migrationBuilder.DropTable(
                name: "vouChers");

            migrationBuilder.DropTable(
                name: "gioHangCT");

            migrationBuilder.DropTable(
                name: "khachHang");

            migrationBuilder.DropTable(
                name: "sanPhamCT");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "hangSanXuats");

            migrationBuilder.DropTable(
                name: "mauSacs");

            migrationBuilder.DropTable(
                name: "sanPhams");

            migrationBuilder.DropTable(
                name: "sizes");
        }
    }
}
