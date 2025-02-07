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

        public IActionResult Index(string name)
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

            ViewData["mess1"] = $"Mời bạn xem sản phẩm của {user.UserName}";

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

            var list = query.ToList();

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

        public IActionResult AddGioHang(int id, int soLuong)
        {
            try
            {
                // Kiểm tra người dùng đăng nhập
                var acc = HttpContext.Session.GetString("Account");
                if (acc == null)
                {
                    return Content("Chưa đăng nhập hoặc hết hạn");
                }

                var getAcc = _db.Accounts.FirstOrDefault(x => x.UserName == acc);
                if (getAcc == null)
                {
                    return Content("Tài khoản không tồn tại");
                }

                // Kiểm tra sản phẩm có tồn tại không
                var sanPham = _db.sanPhamCT.FirstOrDefault(sp => sp.IdSPCT == id);
                if (sanPham == null)
                {
                    return Content("Sản phẩm không tồn tại");
                }

                if (soLuong <= 0)
                {
                    return Content("Số lượng sản phẩm phải lớn hơn 0.");
                }

                // Lấy danh sách sản phẩm trong giỏ hàng của user
                var userCart = _db.gioHangCT.Where(x => x.IdAcc == getAcc.IdAcc).ToList();

                // Kiểm tra sản phẩm đã có trong giỏ hàng chưa
                var ghctUpdate = userCart.FirstOrDefault(x => x.IdSPCT == id);

                // Nếu sản phẩm đã có trong giỏ hàng, cập nhật số lượng
                if (ghctUpdate != null)
                {
                    if ((ghctUpdate.Soluong + soLuong) > sanPham.SoluongTon)
                    {
                        return Content($"Số lượng sản phẩm trong kho không đủ (Tồn: {sanPham.SoluongTon}).");
                    }

                    ghctUpdate.Soluong += soLuong;
                    ghctUpdate.GiaBan = (sanPham.Gia ?? 0) * ghctUpdate.Soluong;

                }
                else
                {
                    // Kiểm tra tồn kho trước khi thêm mới
                    if (soLuong > sanPham.SoluongTon)
                    {
                        return Content($"Số lượng sản phẩm trong kho không đủ (Tồn: {sanPham.SoluongTon}).");
                    }

                    // Thêm sản phẩm mới vào giỏ hàng
                    GioHangCT ghct = new GioHangCT()
                    {
                        IdSPCT = id,
                        IdAcc = getAcc.IdAcc, // Liên kết với tài khoản user
                        Soluong = soLuong,
                        GiaBan = (sanPham.Gia ?? 0) * soLuong
                    };
                    _db.gioHangCT.Add(ghct);

                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }

                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Content($"Lỗi không xác định: {ex.Message}");
            }
        }




    }
}


