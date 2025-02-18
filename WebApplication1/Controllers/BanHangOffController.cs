using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BanHangOffController : Controller
    {
        public DuAn1DbContext _db;
        public BanHangOffController(DuAn1DbContext db)
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
            if (user == null)
            {
                TempData["mess"] = "Không tìm thấy người dùng.";
                return RedirectToAction("Login", "Login");
            }

            ViewData["mess1"] = $"Chào mừng {user.UserName} đã đến xem cửa hàng TrendSneaker";

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

        [HttpPost]
        public IActionResult AddGioHang(int id, int soLuong = 1)
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

            var product = _db.sanPhamCT.FirstOrDefault(x => x.IdSPCT == id);
            if (product == null)
            {
                return Content("Sản phẩm không tồn tại.");
            }

            // Kiểm tra sản phẩm đã tồn tại trong giỏ hàng chưa
            var existingCartItem = _db.gioHangCT
                .FirstOrDefault(gh => gh.IdSPCT == id && gh.IdAcc == account.IdAcc);

            if (existingCartItem != null)
            {
                // Nếu đã tồn tại, kiểm tra số lượng sau khi cộng thêm
                int newQuantity = existingCartItem.Soluong + soLuong;
                if (newQuantity > product.SoluongTon)
                {
                    TempData["Error"] = $"Sản phẩm {product.TenSp} chỉ còn lại {product.SoluongTon} trong kho.";
                    return RedirectToAction("Index", "SanPhamCT");
                }

                // Nếu số lượng hợp lệ, cập nhật số lượng và giá bán
                existingCartItem.Soluong = newQuantity;
                existingCartItem.GiaBan = (product.Gia ?? 0) * existingCartItem.Soluong;
                _db.gioHangCT.Update(existingCartItem);
            }
            else
            {
                // Nếu chưa tồn tại, kiểm tra số lượng tồn kho trước khi thêm mới
                if (soLuong > product.SoluongTon)
                {
                    TempData["Error"] = $"Sản phẩm {product.TenSp} chỉ còn lại {product.SoluongTon} trong kho.";
                    return RedirectToAction("Index", "SanPhamCT");
                }

                // Nếu số lượng hợp lệ, thêm mới vào giỏ hàng
                var gioHang = new GioHangCT()
                {
                    IdAcc = account.IdAcc,
                    IdSPCT = product.IdSPCT,
                    Soluong = soLuong,
                    GiaBan = (product.Gia ?? 0) * soLuong
                };

                _db.gioHangCT.Add(gioHang);
            }

            try
            {
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message;
                return Content($"Có lỗi xảy ra khi lưu: {innerException}");
            }
        }

        public IActionResult HoaDonCho()
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

            var GHCTS = from ghct in _db.gioHangCT
                        join sp in _db.sanPhamCT on ghct.IdSPCT equals sp.IdSPCT into spGroup
                        from sp in spGroup.DefaultIfEmpty()

                        select new GioHangCT()
                        {
                            IdGHCT = ghct.IdGHCT,
                            Soluong = ghct.Soluong,
                            GiaBan = ghct.GiaBan,
                            IdSPCT = sp.IdSPCT,
                            SanPhamCT = new SanPhamCT()
                            {
                                TenSp = sp.TenSp,
                                Img = sp.Img
                            }
                        };

            var totalPrice = GHCTS.Sum(x => x.GiaBan);
            ViewData["TotalPrice"] = totalPrice;

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
                    TrangThai = "Chưa thanh toán",
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
                                TrangThai = "Chưa thanh toán"
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
                hoaDon.TrangThai = "Đã thanh toán";
                _db.hoaDons.Update(hoaDon);

                _db.SaveChanges();

                _db.gioHangCT.RemoveRange(gioHangChiTiets);
                _db.SaveChanges();

                HttpContext.Session.SetString("PaymentStatus", "Success");
                return RedirectToAction("XacNhan");
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
