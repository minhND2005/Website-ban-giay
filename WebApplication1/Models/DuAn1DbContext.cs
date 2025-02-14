using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class DuAn1DbContext : DbContext
    {
        public DuAn1DbContext()
        {
        }
        public DuAn1DbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //ham  nay co nhieu cong dung nhung muon fix cuong thi fix o day
            modelBuilder.Entity<Account>().HasData(
                new Account { IdAcc = 1, UserName = "Admin", Password = "123", Sdt = "0943921328" ,Email = "A@gmail.com", ConfirmPassword = "123"},
                new Account { IdAcc  = 2, UserName = "NhanVien1", Password = "123", Sdt = "0987654321", Email = "NV1@gmail.com", ConfirmPassword = "123" }
                
            );

            modelBuilder.Entity<GioHangCT>()
                .HasIndex(gh => new { gh.IdAcc, gh.IdSPCT })
                 .IsUnique();

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ChuongTrinhKM> chuongTrinhKMs { get; set; }
        public DbSet<HoaDon> hoaDons { get; set; }
        public DbSet<HoaDonCT> hoaDonsCT { get; set; }
        public DbSet<SanPham> sanPhams { get; set; }
        public DbSet<SanPhamCT> sanPhamCT { get;set; }
        public DbSet<HangSanXuat> hangSanXuats { get; set; }
        public DbSet<KhachHang> khachHang { get; set; }
        public DbSet<VouCher> vouChers { get; set; }
        public DbSet<NhanVien> nhanViens { get; set; }
        public DbSet<KhuyenMaiCT> khuyenMaiCT { get; set; }
        public DbSet<MauSac> mauSacs { get; set; }
        public DbSet<Size> sizes { get; set; }
        public DbSet<GioHangCT> gioHangCT { get; set ; }

       

    }
}
