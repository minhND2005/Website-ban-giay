namespace WebApplication1.Models
{
    public class VouCher
    {
        public int IdVouCher { get; set; }
        public string VouCherName { get; set; }

        public int SoLuong {  get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int PhanTramGiam { get; set; }

        public List<HoaDon> hoaDons { get; set; }
    }
}
