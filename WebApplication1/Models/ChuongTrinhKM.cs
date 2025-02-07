using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class ChuongTrinhKM
    {
        [Key]
        public int IdKhuyenMai {  get; set; }
        public string NameKM { get; set; }
        public int SoLuong { get; set; }
        public string TrangThai { get; set; }

        public List<KhuyenMaiCT> KhuyenMaiCT { get; set; }

    }
}
