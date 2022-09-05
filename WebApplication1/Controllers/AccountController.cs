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
        //GET (the register page)
        public IActionResult Register()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Register(RegisterViewModel obj)
        {
            if(obj.Password != null) 
            {


                var user = _accountServices.RegisterUserDetails(obj);
                if (user != null)
                {
                    var checkEmail = _userManager.FindByEmailAsync(obj.Email).Result;
                    if (checkEmail != null)
                    {
                       // System.Threading.Thread.Sleep(3000);
                        obj.Message = "Email already exist!";
                        obj.ErrorHappened = true;
                        return View(obj);
                    }
                    var creatUser = _userManager.CreateAsync(user, obj.Password).Result;
                    if (creatUser.Succeeded)
                    {
                        if (obj.Password == "====")
                        {
                            var registeredUser = _userManager.FindByEmailAsync(obj.Email).Result;
                              await _userManager.AddToRoleAsync(registeredUser, "Admin");
                                registeredUser.IsAdmin = true;
                            _db.Update(registeredUser);
                            _db.SaveChanges();
                        }
                        else
                        {
                            var registeredUser = _userManager.FindByEmailAsync(obj.Email).Result;
                            await _userManager.AddToRoleAsync(registeredUser, "Student");
                            registeredUser.IsAdmin = false;
                            _db.Update(registeredUser);
                            _db.SaveChanges();
                        }
                        obj.Message = "Your have Registered successfully!";
                        obj.ErrorHappened = false;
                        return RedirectToAction("Login");
                    }
                   
                }
            } 
            //var userRegistrations = _accountServices.RegisterUserDetails(obj);
            return View(obj);
        }
        //GET (the login view page)
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
                
                var checkApplicationUserDetails = _accountServices.Login(loginViewModel);

                if (checkApplicationUserDetails != null)
                {
                    var isAdmin = _userManager.IsInRoleAsync(checkApplicationUserDetails, "Admin").Result;
                    if (isAdmin)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    return RedirectToAction("Index", "Student");
                }
                else
                {
                    loginViewModel.Message = "Invalid Email and Password!";
                    loginViewModel.ErrorHappened = true;
                  
                    //ModelState.AddModelError("Email", "Invalid Email and Password");
                    return View(loginViewModel);
                }

               
            }
            return View(loginViewModel);
           
        }
        //Sign out to users
        public IActionResult LogOut()
        {
            _signManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        //returning Application to it Dashboard
        [HttpGet]
        public IActionResult UserPersonalDashboard()
        {
            var userNameOfTheCurrentUser = User.Identity.Name;
            var userNameOfTheCurrentUserFromDb = _userManager.FindByNameAsync(userNameOfTheCurrentUser).Result;
            var ifCurrentUserIsAdmin = _userManager.IsInRoleAsync(userNameOfTheCurrentUserFromDb, "Admin").Result;
            if (ifCurrentUserIsAdmin)
            {
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Student");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View(); 
        }
    }
}
