using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class NhanVien
    {
        [Key]
        public int IdNV { get; set; }
        public string NameNV { get; set; }
        public string DiaChi {  get; set; }
        public int Tuoi {  get; set; }
        public string Email { get; set; }
        public DateTime NgayLamViec { get; set; }
        public string TrangThai { get; set; }
        public string Role { get; set; }
        public int? IdAcc { get; set; }
        public Account Account { get; set; }    

        public List<HoaDon> hoaDons { get; set; }   
    }
}
