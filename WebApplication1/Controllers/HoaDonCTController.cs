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

            var hoaDonChiTiets = from hd in _db.hoaDons
                                 join hdct in _db.hoaDonsCT on hd.IdHD equals hdct.IdHD
                                 join sp in _db.sanPhamCT on hdct.IdSPCT equals sp.IdSPCT
                                 join ms in _db.mauSacs on sp.IdMauSac equals ms.IdMauSac into msGroup
                                 from ms in msGroup.DefaultIfEmpty()
                                 join sz in _db.sizes on sp.IdSize equals sz.IdSize into szGroup
                                 from sz in szGroup.DefaultIfEmpty()
                                 join vc in _db.vouChers on hd.IdVouCher equals vc.IdVouCher into vcGroup
                                 from vc in vcGroup.DefaultIfEmpty()

                                 where hd.IdAcc == account.IdAcc

                                 select new HoaDonCT()
                                 {
                                    IdHDCT = hdct.IdHDCT,
                                    Soluong = hdct.Soluong,
                                    GiaBan = hdct.GiaBan,
                                    TrangThai = hdct.TrangThai,
                                    IdHD = hd.IdHD,

                                     SanPham = new SanPhamCT()
                                     {
                                         TenSp = sp.TenSp,
                                         Img = sp.Img,
                                         MauSac = ms != null ? new MauSac { TenMau = ms.TenMau } : null,
                                         size = sz != null ? new Size { SizeName = sz.SizeName } : null
                                     }
                                      
 
                                 };

            return View(hoaDonChiTiets.ToList());
        }
    }
}
