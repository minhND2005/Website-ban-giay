using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace WebApplication1.Models
{
    public class KhachHang
    {
        [Key]
        public int IdKH {  get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string DiaChi { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [RegularExpression(@"^(\+84|0)\d{9,10}$", ErrorMessage = "Số điện thoại không đúng định dạng.")]
        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email.")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
        public string Email { get; set; }
        [Required]
        public int Tuoi { get; set; }
        public int? IdAcc { get;set; }

        public Account? Account { get; set; }

        public List<HoaDon> hoaDons { get; set; }
    }
}
