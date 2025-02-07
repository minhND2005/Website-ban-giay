using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class HoaDon
    {
        [Key] 
        public int IdHD { get; set; }
        public string HoaDonName { get; set; }
        public decimal GiaBan { get; set; }
        public string TrangThai { get;set; }
        public DateTime NgayLap { get;set;}
        public int? IdNV { get; set; }
        public int? IdKH { get; set; }
        public int? IdVouCher { get; set; }

        public NhanVien? NhanVien { get; set; }
        public KhachHang? KhachHang { get; set; }
        public VouCher? VouCher { get; set; }   

        public List<HoaDonCT> hoaDons { get; set; }
    }
}
