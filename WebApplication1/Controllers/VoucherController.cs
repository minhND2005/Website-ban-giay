using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class VoucherController : Controller
    {
        public DuAn1DbContext _db;
        public VoucherController(DuAn1DbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var list = _db.vouChers.ToList();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Create(VouCher vouCher)
        {
            _db.vouChers.Add(vouCher);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var list = _db.vouChers.Find(id);
            return View(list);
        }

        [HttpPost]

        public IActionResult Edit(VouCher vouCher)
        {
            _db.vouChers.Update(vouCher);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
