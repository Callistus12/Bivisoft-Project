using Calischool.Data;
using Calischool.Models;
using Calischool.Services;
using Calischool.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calischool.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAccountServices _accountServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        public StudentController(ApplicationDbContext db, IAccountServices accountServices, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _db = db;
            _accountServices = accountServices;
            _userManager = userManager;
            _signManager = signManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        //Get
        public IActionResult Edit()
        {
            HttpContext.User.Identities.GetHashCode();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RegisterViewModel registerView)
        {
            if (registerView == null)
            {
                return null;
            }
            var updateUser = _userManager.FindByEmailAsync(registerView.Email).Result;
            if (updateUser != null)
            {
                _userManager.UpdateAsync(updateUser);
                return RedirectToAction("Index");
            }
            return View(registerView);
        }
    }
}
