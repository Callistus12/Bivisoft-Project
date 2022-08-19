using Calischool.Data;
using Calischool.Models;
using Calischool.Services;
using Calischool.ViewModel;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calischool.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAccountServices _accountServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        public AccountController(ApplicationDbContext db, IAccountServices accountServices, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
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
        //GET
        public IActionResult Register()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Register(RegisterViewModel obj)
        {
            if(obj.Email == null)
            {
                ModelState.AddModelError("Email", "Email is required");
            }
            if(obj.Password != null) 
            {


                var user = _accountServices.RegisterUserDetails(obj).Result;
                if (user != null)
                {
                    var checkEmail = _userManager.FindByEmailAsync(obj.Email).Result;
                    if (checkEmail != null)
                    {
                        return null;
                    }
                    var creatUser = _userManager.CreateAsync(user, obj.Password).Result;
                    if (creatUser.Succeeded)
                    {
                        if (obj.Password == "====")
                        {
                            var registeredUser = _userManager.FindByEmailAsync(obj.Email).Result;
                              await _userManager.AddToRoleAsync(registeredUser, "Admin");
                        }
                        else
                        {
                            var registeredUser = _userManager.FindByEmailAsync(obj.Email).Result;
                            await _userManager.AddToRoleAsync(registeredUser, "Student");
                        }
                    }

                    TempData["success"] = "Your Details Are Registered successfully!";
                    return RedirectToAction("Login");
                }
            }
            //var userRegistrations = _accountServices.RegisterUserDetails(obj);
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
        public IActionResult LogOut()
        {
            //var user = _userManager.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            //var add = _userManager.AddToRoleAsync(user, "Admin").Result;

            _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
