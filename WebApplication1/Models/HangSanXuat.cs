namespace WebApplication1.Models
{
    public class HangSanXuat
    {
        public int IdHSX {  get; set; }
        
        public string NameHSX { get; set; }
        public string DiaChi { get; set; }
        public string Sdt {  get; set; }
        public string Email { get; set; }

        public List<SanPhamCT> SanPhamCTs { get; set; }
    }
}
