using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Thêm namespace này để sử dụng Include
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HoaDonAdminController : Controller
    {
        DuAn1DbContext _db;
        public HoaDonAdminController(DuAn1DbContext db)
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
                return Content("Bạn không có quyền xem thông tin.");
            }
            // Dùng Include để nạp dữ liệu Account liên quan
            var list = from hd in _db.hoaDons
                       join ac in _db.Accounts on hd.IdAcc equals ac.IdAcc into msGroup
                       from ac in msGroup.DefaultIfEmpty()
                       select new HoaDon
                       {
                           IdHD = hd.IdHD,
                           HoaDonName = hd.HoaDonName,
                           GiaBan = hd.GiaBan,
                           TrangThai = hd.TrangThai,
                           NgayLap = hd.NgayLap,
                           Account = new Account()
                           {
                              UserName = ac.UserName,
                           }
                       };
            return View(list.ToList());
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
                return Content("Bạn không có quyền xem thông tin của khách.");
            }
            var hoaDon = _db.hoaDons.Find(id);
            return View(hoaDon);
        }

        [HttpPost]
        public IActionResult Edit(HoaDon model)
        {
            var hoaDon = _db.hoaDons.FirstOrDefault(h => h.IdHD == model.IdHD);
            if (hoaDon != null)
            {
                hoaDon.HoaDonName = hoaDon.HoaDonName;
                hoaDon.TrangThai = model.TrangThai;

                // Khóa cứng các giá trị không muốn thay đổi
                hoaDon.NgayLap = hoaDon.NgayLap;
                hoaDon.IdAcc = hoaDon.IdAcc;
                hoaDon.GiaBan = hoaDon.GiaBan;

                _db.hoaDons.Update(hoaDon);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}