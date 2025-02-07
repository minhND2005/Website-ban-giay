using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace WebApplication1.Models
{
    public class KhachHang
    {
        [Key]
        public int IdKH {  get; set; }
        public string Name { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
        public int Tuoi { get; set; }
        public int? IdAcc { get;set; }

        public Account? Account { get; set; }

        public List<HoaDon> hoaDons { get; set; }
    }
}
