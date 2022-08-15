using Calischool.Data;
using Calischool.Models;
using Calischool.Services;
using Calischool.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calischool.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAccountServices _accountServices;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(ApplicationDbContext db, IAccountServices accountServices, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _accountServices = accountServices;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET
        public IActionResult Register()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel obj)
        {
            if(obj.Email == null)
            {
                ModelState.AddModelError("Email", "Email is required");
            }
            if(obj.Password != null) 
            {
                var userRegistration = _accountServices.RegisterUserDetails(obj);
               
                if (userRegistration != null)
                {
                    TempData["success"] = "Your Details Are Registered successfully!";
                    return RedirectToAction("Login","Account");
                }
            }
            return View(obj);
        }
        //GET
        public IActionResult Login()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
           
            if (loginViewModel != null)
            {
                var checkStudent = _accountServices.Login(loginViewModel);
                var isAdmin = _userManager.IsInRoleAsync(checkStudent, "Admin").Result;
                if (isAdmin)
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                 return RedirectToAction("Index", "Student");
            }
            return View(loginViewModel);
           
        }
      
    }
}
