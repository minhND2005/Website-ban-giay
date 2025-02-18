using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class HoaDonCT
    {
        [Key]
        public int IdHDCT { get; set; }
        public decimal GiaBan { get; set; }
        public int Soluong {  get; set; }   
        public string TrangThai { get; set;}
        public int? IdHD {  get; set; }
        public int? IdSPCT { get;set; }
        public HoaDon? HoaDon { get; set; }
        public SanPhamCT? SanPham { get; set; }
    }
}
