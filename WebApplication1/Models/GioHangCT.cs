namespace WebApplication1.Models
{
    public class GioHangCT
    {
        public int IdGHCT { get; set; }
        public int Soluong { get; set; }
        public decimal GiaBan { get; set; }
        public int? IdKH {  get; set; }  
        public int? IdSPCT { get; set; }
        public int? IdSP { get; set; }

        public KhachHang? KhachHang { get; set; }
        public SanPhamCT? SanPhamCT { get; set; }
        public SanPham? SanPham { get; set; }
    }
}
