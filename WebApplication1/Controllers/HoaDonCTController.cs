using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HoaDonCTController : Controller
    {
        private readonly DuAn1DbContext _db;

        public HoaDonCTController(DuAn1DbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("Account");
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var account = _db.Accounts.FirstOrDefault(x => x.UserName == user);
            if (account == null)
            {
                return Content("Tài khoản không tồn tại.");
            }
            return View();
        }
    }
}
