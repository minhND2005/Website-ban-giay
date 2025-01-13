namespace WebApplication1.Models
{
    public class MauSac
    {
        public int IdMauSac { get; set; }
        public string TenMau { get; set; }
        public List<SanPhamCT> SanPhamCTs { get; set; }
    }
}
