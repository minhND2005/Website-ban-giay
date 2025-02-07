using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class KhuyenMaiCT
    {
        [Key]
        public int IdKMCT { get; set; }
        public int PhanTramGiam { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKT { get; set; }
        public string TrangThai {  get; set; }  

        public int? IdKhuyenMai {  get; set; }
        public int? IdSPCT { get; set; }
        public ChuongTrinhKM? ChuongTrinhKM { get; set; }
        public SanPhamCT? SanPhamCT { get; set; }
    }
}
