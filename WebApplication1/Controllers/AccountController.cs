﻿using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AccountController : Controller
    {
         public  DuAn1DbContext _db;

         public AccountController(DuAn1DbContext db)
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
                return Content("Bạn không có quyền sửa thông tin của khách.");
            }
            var list = _db.Accounts.ToList();
            return View(list);
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
                return Content("Bạn không có quyền sửa thông tin của khách.");
            }
            var list = _db.Accounts.Find(id);
            return View(list);
        }

        [HttpPost]

        public IActionResult Edit(Account account)
        {
            _db.Accounts.Update(account);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
