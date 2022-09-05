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
            var theCurrentUserName = User.Identity.Name;
            if (theCurrentUserName != null)
            {
                var theCurrentUserDetails = _userManager.FindByNameAsync(theCurrentUserName).Result;
                if (theCurrentUserDetails != null)
                {
                    return View(theCurrentUserDetails);
                }
            }
            return RedirectToAction("Login", "Account");
        }
        //Get
        [HttpGet]
        public IActionResult Edit()
        {
            var userName = User.Identity.Name;

            var detailsOfMyUser = _accountServices.GetUserDetailsForEdit(userName);

            return View(detailsOfMyUser);

        }
        //POST 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RegisterViewModel newlyeditedUserRecord)
        {
            if (newlyeditedUserRecord == null)
            {
               
                return View(newlyeditedUserRecord);
            }
            var editingUserDetails = _accountServices.UpdateTheEditedUserDetails(newlyeditedUserRecord);
            if (editingUserDetails)
            {
                newlyeditedUserRecord.Message = "Account Updated successfully!";
                newlyeditedUserRecord.ErrorHappened = false;
                //TempData["Success"] = "Account Updated successfully!";
                return RedirectToAction("Edit");
            }
            return View(newlyeditedUserRecord);
        }
        //[HttpGet]
        //public IActionResult Details()
        //{
        //    var userName = User.Identity.Name;

        //    var detailsOfMyUser = _accountServices.GetUserDetailsForEdit(userName);

        //    return View(detailsOfMyUser);

        //}
    }
}
