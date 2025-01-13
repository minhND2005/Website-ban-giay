namespace WebApplication1.Models
{
    public class SanPhamCT
    {
        public int IdSPCT {  get; set; }
        public string Img { get; set; }
        public int SoluongTon { get; set; }
        public Decimal Gia { get; set; }

        public int? IdSP {  get; set; }
        public int? IdSize { get; set; }
        public int? IdMauSac {  get; set; } 
        public int? IdHSX { get; set; }
        public MauSac? MauSac { get; set; }
        public HangSanXuat? HangSanXuat { get; set; }
        public Size? size { get; set; }
        public SanPham? SanPham { get; set; }

        public List<GioHangCT> GioHangCTs { get; set; }

        public List<HoaDonCT> HoaDonCTs { get;set; }
        public List<KhuyenMaiCT> khuyenMaiCTs { get; set; }

    }
}
