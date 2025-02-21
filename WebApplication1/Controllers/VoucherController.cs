using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class VoucherController : Controller
    {
        public DuAn1DbContext _db;
        public VoucherController(DuAn1DbContext db)
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
            if (account == null || account.UserName != "Admin")
            {
                return Content("Bạn không có quyền xem thông tin của khách.");
            }
            var list = _db.vouChers.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            var user = HttpContext.Session.GetString("Account");
            if (user == null)
            {
                return Content("Vui lòng đăng nhập.");
            }

            // Tìm thông tin người dùng từ cơ sở dữ liệu
            var account = _db.Accounts.FirstOrDefault(x => x.UserName == user);
            if (account == null || account.UserName != "Admin")
            {
                return Content("Bạn không có quyền xem thông tin của khách.");
            }
            return View();
        }

        [HttpPost]

        public IActionResult Create(VouCher vouCher)
        {
            _db.vouChers.Add(vouCher);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var user = HttpContext.Session.GetString("Account");
            if (user == null)
            {
                return Content("Vui lòng đăng nhập.");
            }

            // Tìm thông tin người dùng từ cơ sở dữ liệu
            var account = _db.Accounts.FirstOrDefault(x => x.UserName == user);
            if (account == null || account.UserName != "Admin")
            {
                return Content("Bạn không có quyền xem thông tin.");
            }
            var list = _db.vouChers.Find(id);
            return View(list);
        }

        [HttpPost]

        public IActionResult Edit(VouCher vouCher)
        {
            _db.vouChers.Update(vouCher);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
