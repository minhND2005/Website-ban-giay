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

                var gioHang = _db.gioHangCT.FirstOrDefault(x => x.IdAcc == Getacc.IdAcc);
                if (gioHang == null)
                {
                    return Content("Không tìm thấy giỏ hàng.");
                }

                var gioHangChiTiets = _db.gioHangCT
                     .Where(ghct => ghct.IdAcc == Getacc.IdAcc)
                     .Include(ghct => ghct.SanPhamCT)
                     .ToList();


                if (!gioHangChiTiets.Any())
                {
                    return Content("Giỏ hàng trống, không thể thanh toán.");
                }

                var hoaDon = new HoaDon
                {
                    HoaDonName = "Hóa đơn #" + Guid.NewGuid().ToString().Substring(0, 8),
                    TrangThai = "Chua thanh toan",
                    IdAcc = Getacc.IdAcc,// Đảm bảo rằng IdAcc được gán đúng
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
                        // Tạo chi tiết hóa đơn
                        var hoaDonCT = new HoaDonCT
                        {
                            IdHD = hoaDon.IdHD,  // Liên kết với hóa đơn vừa tạo
                            IdSPCT = sanPham.IdSPCT,
                            Soluong = item.Soluong,
                            TrangThai = "Chưa thanh toán"
                        };
                        _db.hoaDonsCT.Add(hoaDonCT);

                        _db.SaveChanges();


                        // Cập nhật số lượng sản phẩm trong kho
                        if (sanPham.SoluongTon >= item.Soluong)
                        {
                            sanPham.SoluongTon -= item.Soluong;  // Trừ số lượng sản phẩm trong kho
                            _db.sanPhamCT.Update(sanPham);  // Cập nhật lại sản phẩm trong kho

                            // Tính giá trị cho sản phẩm trong hóa đơn
                            totalMoney += (sanPham.Gia ?? 0) * item.Soluong;

                        }
                        else
                        {
                            return Content($"Sản phẩm {sanPham.TenSp} không đủ số lượng để thanh toán.");
                        }
                    }
                }

                hoaDon.GiaBan = totalMoney;
                _db.hoaDons.Update(hoaDon);

                // Lưu thay đổi vào cơ sở dữ liệu
                _db.SaveChanges();

                _db.gioHangCT.RemoveRange(gioHangChiTiets);
                _db.SaveChanges();

                hoaDon.TrangThai = "Đã thanh toán";
                _db.hoaDons.Update(hoaDon);
                // Xóa chi tiết giỏ hàng sau khi thanh toán thành công


                var hoaDonCTs = _db.hoaDonsCT.Where(hdct => hdct.IdHD == hoaDon.IdHD).ToList();
                foreach (var hdct in hoaDonCTs)
                {
                    hdct.TrangThai = "Đã thanh toán";
                }
                _db.hoaDonsCT.UpdateRange(hoaDonCTs);

                _db.SaveChanges();

                return RedirectToAction("DatHang");

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
            try
            {


                var list = _db.khachHang.ToList();
                _db.khachHang.Add(model);
                _db.SaveChanges();
                HttpContext.Session.SetString("PaymentStatus", "Success");
                return RedirectToAction("XacNhan");
            }
            catch (Exception ex)
            {
                // Ghi log hoặc hiển thị lỗi chi tiết để dễ debug
                return Content($"Có lỗi xảy ra: {ex.Message}. {ex.InnerException?.Message}");
            }
          
        }

        public IActionResult XacNhan()
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
            var paymentStatus = HttpContext.Session.GetString("PaymentStatus");
            if (paymentStatus == "Success")
            {
                ViewBag.Message = "Bạn đã thanh toán thành công!";
            }
            else
            {
                ViewBag.Message = "Thanh toán thất bại";
            }
            return View();
        }

    }
}
