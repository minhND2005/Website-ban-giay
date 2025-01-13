namespace WebApplication1.Models
{
    public class Size
    {
        public int IdSize { get; set; }

        public string SizeName { get; set; }
        public string MoTa { get; set; }

        public List<SanPhamCT> SanPhamCT { get; set; }  

    }
}
