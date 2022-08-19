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
        [HttpGet]
        public IActionResult Edit()
        {
            var userName = User.Identity.Name;

            var detailsOfMyUser = _accountServices.GetUserDetails(userName);

            return View(detailsOfMyUser);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditStudentViewModel obj)
        {
            if (obj == null)
            {
                return null;
            }
            var updateUser = _userManager.FindByEmailAsync(obj.Email).Result;
            if (updateUser != null)
            {
                updateUser.Country = obj.Country;
                updateUser.Displine = obj.Displine;
                updateUser.DateOfBirth = obj.DateOfBirth;
                updateUser.FirstName = obj.FirstName;
                updateUser.MiddleName = obj.MiddleName;
                updateUser.LastName = obj.LastName;
                updateUser.Email = obj.Email;
                updateUser.Gender = obj.Gender;
                updateUser.State = obj.State;
                updateUser.Qualification = obj.Qualification;
                updateUser.RedsidentAddress = obj.RedsidentAddress;
                updateUser.LGA = obj.LGA;



                   
                _db.StudentRegisters.Update(updateUser);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
    }
}
