using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly DuAn1DbContext _db;
        public LoginController(DuAn1DbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var session = HttpContext.Session.GetString("Account"); // lấy ra userName mà đã đc lưu
            if (session == null)  //check chưa đăng nhập
            {
                return Content("Chưa đăng nhập mà dám vào xem");
            }
            else
            {
                ViewData["message"] = $"Chào mừng {session} đến với bình nguyên vô tận";
            }
            //lấy toàn bộ user
            var accountData = _db.Accounts.ToList();
            return View(accountData);
        }
        public IActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DangKy(Account account)
        {
            try
            {
                // Kiểm tra dữ liệu đầu vào
                if (string.IsNullOrEmpty(account.UserName) || string.IsNullOrEmpty(account.Password))
                {
                    TempData["Error"] = "Tên đăng nhập và mật khẩu không được để trống.";
                    return View();
                }
                // Kiểm tra trùng lặp UserName
                var existingUser = _db.Accounts.FirstOrDefault(x => x.UserName == account.UserName);
                if (existingUser != null)
                {
                    ModelState.AddModelError("UserName", "Tên đăng nhập đã tồn tại, vui lòng chọn tên khác.");
                    return View(account);
                }
                // Thêm tài khoản mới
                _db.Accounts.Add(account);
                _db.SaveChanges(); // Lưu tài khoản để tạo `Id`
                // Kiểm tra giỏ hàng cho tài khoản vừa tạo
                var existingCart = _db.gioHangCT.FirstOrDefault(gh => gh.IdAcc == account.IdAcc);
                if (existingCart == null) // Chỉ tạo giỏ hàng nếu chưa tồn tại
                {
                    GioHangCT gioHang = new GioHangCT()
                    {
                        IdAcc = account.IdAcc,       // Sử dụng Id của tài khoản vừa tạo
                        UserName = account.UserName,
                    };
                    _db.gioHangCT.Add(gioHang);
                    _db.SaveChanges(); // Lưu giỏ hàng
                }
                // Thông báo thành công
                TempData["Status"] = "Chúc mừng bạn đã tạo tài khoản thành công!";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                // Ghi log lỗi để dễ dàng kiểm tra
                TempData["Error"] = "Đã xảy ra lỗi khi tạo tài khoản. Vui lòng thử lại.";
                return View(account);
            }
        }
        public IActionResult Login()
        {
            return View();
        }

        //đăng nhập
        [HttpPost]
        public IActionResult Login(string userName, string passWord)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                return View();
            }

            var acc = _db.Accounts.FirstOrDefault(x => x.UserName == userName && x.Password == passWord);
            if (acc == null)
            {
                TempData["Error"] = "Bạn đăng nhập sai tên đăng nhập hoặc mật khẩu!";
                return RedirectToAction("Login");
            }

            TempData.Remove("Error");
            HttpContext.Session.SetString("Account", userName);

            // Nếu có URL trước đó (ReturnUrl), chuyển hướng đến trang đó
            if (TempData["ReturnUrl"] != null)
            {
                return Redirect(TempData["ReturnUrl"].ToString());
            }

            // Nếu là Admin -> chuyển hướng đến trang Admin
            if (userName == "Admin")
            {
                return RedirectToAction("Index", "AdminSanPham");
            }
            if (userName == "NhanVien")
            {
                return RedirectToAction("Index", "NhanVienSanPham");
            }

            // Nếu là User thường -> chuyển hướng đến trang sản phẩm
            return RedirectToAction("Index", "SanPhamCT");
        }


        [HttpGet]
        public IActionResult DoiMatKhau()
        {
            var session = HttpContext.Session.GetString("Account");
            if (session == null)
            {
                TempData["Error"] = "Bạn cần đăng nhập để đổi mật khẩu.";
                TempData["ReturnUrl"] = Url.Action("DoiMatKhau", "Login");
                return RedirectToAction("Login");
            }
            var account = _db.Accounts.FirstOrDefault(a => a.UserName == session);
            if (account == null)
            {
                TempData["Error"] = "Tài khoản không tồn tại.";
                return RedirectToAction("Login");
            }
            return View(account);
        }
        [HttpPost]
        public IActionResult DoiMatKhau(int idAcc, string oldPassword, string newPassword, string confirmNewPassword)
        {
            var account = _db.Accounts.FirstOrDefault(a => a.IdAcc == idAcc);
            if (account == null)
            {
                TempData["Error"] = "Tài khoản không tồn tại.";
                return View();
            }
            if (account.Password != oldPassword)
            {
                TempData["Error"] = "Mật khẩu cũ không đúng.";
                return View();
            }
            if (newPassword != confirmNewPassword)
            {
                TempData["Error"] = "Mật khẩu mới và xác nhận mật khẩu không trùng khớp.";
                return View();
            }
            account.Password = newPassword;
            _db.SaveChanges();
            TempData.Remove("ReturnUrl");
            TempData["Status"] = "Đổi mật khẩu thành công.";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            // Lấy thông tin từ Session
            var session = HttpContext.Session.GetString("Account");

            // Kiểm tra nếu Session chưa có (tức là chưa đăng nhập)
            if (string.IsNullOrEmpty(session))
            {
                return RedirectToAction("Login");
            }

            // Nếu đã đăng nhập, tiến hành xóa Session
            HttpContext.Session.Remove("Account");

            TempData.Clear();
            TempData["Status"] = "Bạn đã đăng xuất thành công!";

            // Chuyển hướng về trang đăng nhập
            return RedirectToAction("Login");
        }

    }
}