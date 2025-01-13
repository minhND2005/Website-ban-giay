namespace WebApplication1.Models
{
    public class ChuongTrinhKM
    {
        public int IdKhuyenMai {  get; set; }
        public string NameKM { get; set; }
        public int SoLuong { get; set; }
        public string TrangThai { get; set; }

        public List<KhuyenMaiCT> KhuyenMaiCT { get; set; }

    }
}
