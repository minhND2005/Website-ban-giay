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

            var totalPrice = GHCTS.Sum(x => x.GiaBan);
            ViewData["TotalPrice"] = totalPrice;
             

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

        public IActionResult ThanhToan(int id)
        {
            try
            {
                var acc = HttpContext.Session.GetString("Account");
                if (acc == null)
                {
                    return Content("Chưa đăng nhập hoặc hết hạn phiên.");
                }

                // Lấy thông tin tài khoản từ cơ sở dữ liệu
                var Getacc = _db.Accounts.FirstOrDefault(x => x.UserName == acc);
                if (Getacc == null)
                {
                    return Content("Tài khoản không tồn tại.");
                }

                // Kiểm tra giỏ hàng rỗng
                var gioHangChiTiets = _db.gioHangCT
                                         .Where(ghct => ghct.IdAcc == Getacc.IdAcc)
                                         .Include(ghct => ghct.SanPhamCT)
                                         .ToList();

                if (!gioHangChiTiets.Any())
                {
                    TempData["ErrorMessage"] = "Giỏ hàng trống, không thể thanh toán.";
                    return RedirectToAction("Index");
                }

                // Tạo hóa đơn
                var hoaDon = new HoaDon
                {
                    HoaDonName = "Hóa đơn #" + Guid.NewGuid().ToString().Substring(0, 8),
                    TrangThai = "Chờ xử lý",
                    IdAcc = Getacc.IdAcc,
                    NgayLap = DateTime.Now
                };
                _db.hoaDons.Add(hoaDon);
                _db.SaveChanges();

                decimal totalMoney = 0m;

                foreach (var item in gioHangChiTiets)
                {
                    var sanPham = _db.sanPhamCT.FirstOrDefault(sp => sp.IdSPCT == item.IdSPCT);
                    if (sanPham != null)
                    {
                        if (sanPham.SoluongTon >= item.Soluong)
                        {
                            sanPham.SoluongTon -= item.Soluong;
                            _db.sanPhamCT.Update(sanPham);

                            var hoaDonCT = new HoaDonCT
                            {
                                IdHD = hoaDon.IdHD,
                                IdSPCT = sanPham.IdSPCT,
                                Soluong = item.Soluong,
                                TrangThai = "Chờ xử lý"
                            };
                            _db.hoaDonsCT.Add(hoaDonCT);

                            totalMoney += (sanPham.Gia ?? 0) * item.Soluong;
                        }
                        else
                        {
                            TempData["ErrorMessage"] = $"Sản phẩm {sanPham.TenSp} không đủ số lượng để thanh toán.";
                            return RedirectToAction("Index");
                        }
                    }
                }

                hoaDon.GiaBan = totalMoney;
                
                _db.hoaDons.Update(hoaDon);

                _db.SaveChanges();

                _db.gioHangCT.RemoveRange(gioHangChiTiets);
                _db.SaveChanges();

                HttpContext.Session.SetString("PaymentStatus", "Success");
                return RedirectToAction("Index", "SanPhamCT");
            }
            catch (Exception ex)
            {
                return Content($"Có lỗi xảy ra: {ex.Message}. {ex.InnerException?.Message}");
            }
        }



        public IActionResult DatHang()
        {
            var acc = HttpContext.Session.GetString("Account");
            if (acc == null)
            {
                return Content("Chưa đăng nhập hoặc hết hạn phiên.");
            }

            // Lấy thông tin tài khoản từ cơ sở dữ liệu
            var Getacc = _db.Accounts.FirstOrDefault(x => x.UserName == acc);
            if (Getacc == null)
            {
                return Content("Tài khoản không tồn tại.");
            }
            return View();
        }


        [HttpPost]
        public IActionResult DatHang(KhachHang model)
        {
            var user = HttpContext.Session.GetString("Account");
            if (user == null)
            {
                return Content("Chưa đăng nhập.");
            }

            var getAcc = _db.Accounts.FirstOrDefault(x => x.UserName == user);
            if (getAcc == null)
            {
                return Content("Tài khoản không tồn tại.");
            }

            // Kiểm tra nếu đã có thông tin khách hàng thì cập nhật
            var khachHangExist = _db.khachHang.FirstOrDefault(kh => kh.IdAcc == getAcc.IdAcc);

            if (khachHangExist != null)
            {
                // Cập nhật thông tin khách hàng
                khachHangExist.Name = model.Name;
                khachHangExist.Email = model.Email;
                khachHangExist.SDT = model.SDT;
                khachHangExist.DiaChi = model.DiaChi;

                _db.khachHang.Update(khachHangExist);
            }
            else
            {
                // Thêm mới thông tin khách hàng nếu chưa có
                model.IdAcc = getAcc.IdAcc;
                _db.khachHang.Add(model);
            }

            _db.SaveChanges();
            HttpContext.Session.SetString("PaymentStatus", "Success");
            return RedirectToAction("XacNhan", "GioHangCT");
        }


        public IActionResult ThongTin()
        {
            var user = HttpContext.Session.GetString("Account");
            if (user == null)
            {
                return Content("Chưa đăng nhập.");
            }

            var getAcc = _db.Accounts.FirstOrDefault(x => x.UserName == user);
            if (getAcc == null)
            {
                return Content("Tài khoản không tồn tại.");
            }

            // Lấy thông tin khách hàng mới nhất từ CSDL
            var thongTinKH = _db.khachHang
                                .AsNoTracking() // Đảm bảo không lưu cache
                                .FirstOrDefault(kh => kh.IdAcc == getAcc.IdAcc);

            return View(thongTinKH);
        }





        public IActionResult XacNhan()
            {
            var acc = HttpContext.Session.GetString("Account");
            if (acc == null)
            {
                return Content("Chưa đăng nhập hoặc hết hạn phiên.");
            }

            var Getacc = _db.Accounts.FirstOrDefault(x => x.UserName == acc);
            if (Getacc == null)
            {
                return Content("Tài khoản không tồn tại.");
            }

            var paymentStatus = HttpContext.Session.GetString("PaymentStatus");
            if (paymentStatus == "Success")
            {
                ViewBag.Message = "Bạn đã thanh toán thành công!";
            }
            else
            {
                ViewBag.Message = "Thanh toán thất bại. Vui lòng kiểm tra lại!";
            }
            return View();
        }


        public IActionResult UpdateQuantity(int id)
        {
            var gioHangCT = _db.gioHangCT.Include(x => x.SanPhamCT).FirstOrDefault(x => x.IdGHCT == id);
            if (gioHangCT == null)
            {
                return NotFound();
            }

            // Lấy thông tin sản phẩm để hiển thị
            var sanPham = _db.sanPhamCT.FirstOrDefault(sp => sp.IdSPCT == gioHangCT.IdSPCT);
            ViewData["SanPham"] = sanPham;

            return View(gioHangCT);
        }

        [HttpPost]
        public IActionResult UpdateQuantity(Dictionary<int, int> quantities)
        {
            foreach (var item in quantities)
            {
                int idGHCT = item.Key;
                int newQuantity = item.Value;

                // Tìm sản phẩm trong giỏ hàng chi tiết
                var gioHangCT = _db.gioHangCT.FirstOrDefault(x => x.IdGHCT == idGHCT);
                if (gioHangCT != null)
                {
                    var sanPham = _db.sanPhamCT.FirstOrDefault(sp => sp.IdSPCT == gioHangCT.IdSPCT);
                    if (sanPham != null)
                    {
                        // Kiểm tra số lượng tồn kho
                        if (newQuantity > sanPham.SoluongTon)
                        {
                            TempData["ErrorMessage"] = $"Số lượng sản phẩm '{sanPham.TenSp}' vượt quá số lượng tồn kho ({sanPham.SoluongTon}).";
                            return RedirectToAction("Index");
                        }

                        // Cập nhật số lượng trong giỏ hàng
                        gioHangCT.Soluong = newQuantity;
                        gioHangCT.GiaBan = (sanPham.Gia ?? 0) * newQuantity;

                        // Lưu thay đổi vào CSDL
                        _db.gioHangCT.Update(gioHangCT);
                    }
                }
            }

            _db.SaveChanges();
            TempData["SuccessMessage"] = "Cập nhật giỏ hàng thành công!";
            return RedirectToAction("Index");
        }


    }
}
