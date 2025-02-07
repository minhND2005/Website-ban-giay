using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class MauSac
    {
        [Key]
        public int IdMauSac { get; set; }
        public string TenMau { get; set; }
        public List<SanPhamCT> SanPhamCTs { get; set; }
    }
}
