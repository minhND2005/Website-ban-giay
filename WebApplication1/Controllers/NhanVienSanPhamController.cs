using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class NhanVienSanPhamController : Controller
    {
        private readonly DuAn1DbContext _db;

        public NhanVienSanPhamController(DuAn1DbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string name, string sortOrder, int page = 1)
        {
            var sessionData = HttpContext.Session.GetString("Account");
            if (sessionData == null)
            {
                TempData["mess"] = "Chưa đăng nhập, bạn ơi!";
                return RedirectToAction("Login", "Login");
            }

            var user = _db.Accounts.FirstOrDefault(x => x.UserName == sessionData);
            if (user == null || user.UserName != "NhanVien")
            {
                return Content("Bạn không có quyền xem thông tin Nhan Vien.");
            }



            var query = from sp in _db.sanPhamCT
                        join ms in _db.mauSacs on sp.IdMauSac equals ms.IdMauSac into msGroup
                        from ms in msGroup.DefaultIfEmpty()
                        join sz in _db.sizes on sp.IdSize equals sz.IdSize into szGroup
                        from sz in szGroup.DefaultIfEmpty()
                        join hsx in _db.hangSanXuats on sp.IdHSX equals hsx.IdHSX into hsxGroup
                        from hsx in hsxGroup.DefaultIfEmpty()
                        select new SanPhamCT
                        {
                            IdSPCT = sp.IdSPCT,
                            TenSp = sp.TenSp,
                            Img = sp.Img,
                            Gia = sp.Gia,
                            SoluongTon = sp.SoluongTon,
                            MauSac = ms,
                            size = sz,
                            HangSanXuat = hsx
                        };

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.TenSp.ToLower().Contains(name.ToLower()));
            }

            query = sortOrder == "desc"
                    ? query.OrderByDescending(x => x.TenSp)
                    : query.OrderBy(x => x.TenSp);

            int pageSize = 6;
            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var list = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.Name = name;

            if (list.Count == 0 && !string.IsNullOrEmpty(name))
            {
                ViewData["NotFound"] = $"Không tìm thấy sản phẩm nào với tên: '{name}'";
            }

            return View(list);
        }

       
    }
}
