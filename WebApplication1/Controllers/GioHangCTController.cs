using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GioHangCTController : Controller
    {
        private readonly DuAn1DbContext _db;

        public GioHangCTController(DuAn1DbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("Account");
            if (user == null)
            {
                return Content("Vui lòng đăng nhập để xem giỏ hàng.");
            }

            var getAcc = _db.Accounts.FirstOrDefault(x => x.UserName == user);
            if (getAcc == null)
            {
                return Content("Tài khoản không tồn tại hoặc bạn đăng nhập sai!");
            }

            bool isCartEmpty = !_db.gioHangCT.Any(x => x.IdAcc == getAcc.IdAcc);
            if (isCartEmpty)
            {
                ViewData["mess1"] = "Giỏ hàng của bạn đang trống.";
                return View(new List<GioHangCT>());
            }

            var GHCTS = from ghct in _db.gioHangCT
                        join sp in _db.sanPhamCT on ghct.IdSPCT equals sp.IdSPCT into spGroup
                        from sp in spGroup.DefaultIfEmpty()

                        join ms in _db.mauSacs on sp.IdMauSac equals ms.IdMauSac into msGroup
                        from ms in msGroup.DefaultIfEmpty()

                        join sz in _db.sizes on sp.IdSize equals sz.IdSize into szGroup
                        from sz in szGroup.DefaultIfEmpty()

                        where ghct.IdAcc == getAcc.IdAcc




                        select new GioHangCT()
                        {
                           IdGHCT = ghct.IdGHCT,
                           Soluong = ghct.Soluong,
                           GiaBan = ghct.GiaBan,
                           IdSPCT = sp.IdSPCT,
                           //SanPhamCT = sp,
                            SanPhamCT = new SanPhamCT()
                            {
                                TenSp = sp.TenSp,
                                Img = sp.Img,
                                MauSac = ms != null ? new MauSac { TenMau = ms.TenMau } : null,
                                size = sz != null ? new Size { SizeName = sz.SizeName } : null
                            }
                           
                        };

            ViewData["mess1"] = "Mời bạn xem giỏ hàng.";
            return View(GHCTS.ToList());




        }

        public IActionResult Delete(int id)
        {
            var list = _db.gioHangCT.Find(id);
            _db.gioHangCT.Remove(list);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
