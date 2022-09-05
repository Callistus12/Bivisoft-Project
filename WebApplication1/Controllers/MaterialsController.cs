using Calischool.Data;
using Calischool.Models;
using Calischool.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Calischool.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAccountServices _accountServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;

        public MaterialsController(ApplicationDbContext db, IAccountServices accountServices, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _db = db;
            _accountServices = accountServices;
            _userManager = userManager;
            _signManager = signManager;
        }
        [HttpGet]
        public IActionResult MaterialPosting()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MaterialPosting(Materials userDetails)
        {
            var userName = User.Identity.Name;
            var currentUser = _userManager.FindByNameAsync(userName).Result;
            userDetails.UserId = currentUser.Id;
            var TheMaterialsToUploadByUser = _accountServices.uploadFileAndMaterialCoursesToDb(userDetails);
            if (TheMaterialsToUploadByUser != null)
            {
                return RedirectToAction("StudentMaterials");
            }
            return View(userDetails);
        }
        [HttpGet]
        public IActionResult StudentMaterials()
        {
            var errorViewModel = new ErrorViewModel();
            var allTheStudentMaterials = _accountServices.GetMaterials();
            if(allTheStudentMaterials.Count() > 0)
            {
                errorViewModel.StudentMaterials = allTheStudentMaterials;
                return View (errorViewModel);
            }
            return View();
        }
        public IActionResult CloseFile(Materials fileDetails)
        {
            return View();
        }
    }
}
