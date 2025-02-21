using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using WebApplication1.Models;

    namespace WebApplication1.Controllers
    {
        public class SanPhamCTController : Controller
        {
            private readonly DuAn1DbContext _db;

            public SanPhamCTController(DuAn1DbContext db)
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
            [HttpGet]
            public IActionResult Create()
            {
                ViewData["MauSacList"] = _db.mauSacs?.ToList() ?? new List<MauSac>();
                ViewData["SizeList"] = _db.sizes?.ToList() ?? new List<Size>();
                ViewData["HangSanXuatList"] = _db.hangSanXuats?.ToList() ?? new List<HangSanXuat>();

                return View(new SanPhamCT());
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(SanPhamCT sanPham, IFormFile Img)
            {
                if (Img != null)
                {
                    string fileName = Path.GetFileName(Img.FileName);
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Img.CopyTo(stream);
                    }

                    sanPham.Img = fileName;
                }

                if (sanPham.IdMauSac.HasValue)
                {
                    sanPham.MauSac = _db.mauSacs.Find(sanPham.IdMauSac);
                }

                if (sanPham.IdSize.HasValue)
                {
                    sanPham.size = _db.sizes.Find(sanPham.IdSize);
                }

                if (sanPham.IdHSX.HasValue)
                {
                    sanPham.HangSanXuat = _db.hangSanXuats.Find(sanPham.IdHSX);
                }

                _db.sanPhamCT.Add(sanPham);
                _db.SaveChanges();

                TempData["mess"] = "Tạo sản phẩm thành công!";

                return RedirectToAction("Index");
            }

            public IActionResult Delete(int id)
            {
                var list = _db.sanPhamCT.Find(id);
                _db.sanPhamCT.Remove(list);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            [HttpGet]
            public IActionResult Edit(int id)
            {
                var sp = _db.sanPhamCT.Find(id);
                if (sp == null)
                {
                    return NotFound(); // Trả về lỗi nếu sản phẩm không tồn tại
                }

                // Lấy các danh sách liên quan để hiển thị trong View
                ViewData["MauSacList"] = _db.mauSacs?.ToList() ?? new List<MauSac>();
                ViewData["SizeList"] = _db.sizes?.ToList() ?? new List<Size>();
                ViewData["HangSanXuatList"] = _db.hangSanXuats?.ToList() ?? new List<HangSanXuat>();

                return View(sp);
            }

            [HttpPost]
            public IActionResult Edit(SanPhamCT sp, IFormFile Img)
            {
                var spEdit = _db.sanPhamCT.Find(sp.IdSPCT);
                if (spEdit == null)
                {
                    TempData["mess"] = "Sản phẩm không tồn tại!";
                    return RedirectToAction("Index");
                }

                // Update basic info
                spEdit.TenSp = sp.TenSp;
                spEdit.SoluongTon = sp.SoluongTon;
                spEdit.Gia = sp.Gia;

                // Process Image
                if (Img != null && Img.Length > 0)
                {
                    string fileName = Path.GetFileName(Img.FileName);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        Img.CopyTo(stream);
                    }
                    spEdit.Img = fileName;
                }

                // Update relations
                if (sp.IdMauSac.HasValue)
                    spEdit.IdMauSac = sp.IdMauSac;
                if (sp.IdSize.HasValue)
                    spEdit.IdSize = sp.IdSize;
                if (sp.IdHSX.HasValue)
                    spEdit.IdHSX = sp.IdHSX;

                // Save changes to DB
                _db.sanPhamCT.Update(spEdit);
                _db.SaveChanges();

                TempData["mess"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction("Index");
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
                        TempData["Error1"] = $"Sản phẩm {product.TenSp} chỉ còn lại {product.SoluongTon} trong kho.";
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
                        TempData["Error2"] = $"Sản phẩm {product.TenSp} chỉ còn lại {product.SoluongTon} trong kho.";
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

        [HttpPost]
        public IActionResult ThanhToanTrucTiep(int id, int soLuong)
        {
            try
            {
                // Kiểm tra người dùng đã đăng nhập chưa
                var user = HttpContext.Session.GetString("Account");
                if (user == null)
                {
                    return RedirectToAction("Login", "Account");
                }

                // Lấy thông tin tài khoản từ Session
                var account = _db.Accounts.FirstOrDefault(x => x.UserName == user);
                if (account == null)
                {
                    return Content("Tài khoản không tồn tại.");
                }

                // Lấy thông tin sản phẩm
                var product = _db.sanPhamCT.FirstOrDefault(x => x.IdSPCT == id);
                if (product == null)
                {
                    return Content("Sản phẩm không tồn tại.");
                }

                // Kiểm tra số lượng hợp lệ
                if (soLuong <= 0)
                {
                    TempData["Error"] = "Số lượng không hợp lệ.";
                    return RedirectToAction("Index");
                }

                if (soLuong > product.SoluongTon)
                {
                    TempData["Error"] = $"Sản phẩm {product.TenSp} chỉ còn lại {product.SoluongTon} trong kho.";
                    return RedirectToAction("Index");
                }

                // Tạo hóa đơn
                var hoaDon = new HoaDon()
                {
                    IdAcc = account.IdAcc,
                    HoaDonName = "Hóa đơn #" + Guid.NewGuid().ToString().Substring(0, 8),
                    NgayLap = DateTime.Now,
                    GiaBan = (product.Gia ?? 0) * soLuong,
                    TrangThai = "Chờ xử lý"
                };
                _db.hoaDons.Add(hoaDon);
                _db.SaveChanges();

                var hoaDonCT = new HoaDonCT()
                {
                    IdHD = hoaDon.IdHD,
                    IdSPCT = product.IdSPCT,
                    Soluong = soLuong,
                    GiaBan = product.Gia ?? 0,
                    TrangThai = "Đã Thanh Toán"
                };
                _db.hoaDonsCT.Add(hoaDonCT);
                _db.SaveChanges();

                // Cập nhật số lượng tồn kho
                product.SoluongTon -= soLuong;
                _db.sanPhamCT.Update(product);
                _db.SaveChanges();

                // Trả về trang xác nhận đơn hàng
                HttpContext.Session.SetString("PaymentStatus", "Success");
                return RedirectToAction("DatHangThanhToan", new { id = hoaDon.IdHD });
            }
            catch (DbUpdateException ex)
            {
                return Content($"Có lỗi xảy ra: {ex.Message}. {ex.InnerException?.Message}");
            }
        }

        public IActionResult DatHangThanhToan()
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
        public IActionResult DatHangThanhToan(KhachHang model)
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
            return RedirectToAction("XacNhanThanhToan");
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

        public IActionResult XacNhanThanhToan()
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

    }
}


