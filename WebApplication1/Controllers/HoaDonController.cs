using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HoaDonController : Controller
    {
        DuAn1DbContext _db;
        public HoaDonController(DuAn1DbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("Account");
            if (user == null)
            {
                return Content("Vui lòng đăng nhập.");
            }

            // Tìm thông tin người dùng từ cơ sở dữ liệu
            var account = _db.Accounts.FirstOrDefault(x => x.UserName == user);
            if (account == null)
            {
                return Content("Không tìm thấy tài khoản.");
            }

            // Lọc hóa đơn theo IdAcc của người dùng
            var list = _db.hoaDons
                .Where(hd => hd.IdAcc == account.IdAcc) // Lọc theo IdAcc
                .ToList();

            if (!list.Any())  // Nếu không có hóa đơn
            {
                return Content("Không có hóa đơn nào.");
            }

            // Trả danh sách hóa đơn cho View
            return View(list);
        }


        public IActionResult Delete(int id)
        {
            var list = _db.hoaDons.Find(id);
            _db.hoaDons.Remove(list);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
