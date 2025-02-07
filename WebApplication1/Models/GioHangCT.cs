using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class GioHangCT
    {
        [Key]
        public int IdGHCT { get; set; }

        public string? UserName { get; set; }  
        public int Soluong { get; set; }
        public decimal GiaBan { get; set; }
        public int? IdKH { get; set; }
        public int? IdSPCT { get; set; }
        public int? IdSP { get; set; }
        public int? IdAcc { get; set; }

        // Foreign key attribute để xác định mối quan hệ
        [ForeignKey("IdAcc")]
        public Account? Account { get; set; }
        public KhachHang? KhachHang { get; set; }
        public SanPhamCT? SanPhamCT { get; set; }
        public SanPham? SanPham { get; set; }
    }
}
