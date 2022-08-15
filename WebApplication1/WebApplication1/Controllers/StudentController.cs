using Calischool.Data;
using Calischool.Models;
using Calischool.Services;
using Microsoft.AspNetCore.Mvc;

namespace Calischool.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAccountServices _accountServices;
        public StudentController(ApplicationDbContext db, IAccountServices accountServices)
        {
            _db = db;
            _accountServices = accountServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Get
        public IActionResult Edit(int id)
        {
            var editUser = _accountServices.Edit(int.Parse(id.ToString()));
            if(editUser != null)
            {
                return View(editUser);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationUser obj)
        {
            if (ModelState.IsValid)
            {
                _db.StudentRegisters.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Updated successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
