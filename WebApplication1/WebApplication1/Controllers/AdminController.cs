using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Calischool.Data;
using Calischool.Models;
using Microsoft.AspNetCore.Authorization;
using Calischool.Services;
using Microsoft.AspNetCore.Identity;
using Calischool.ViewModel;

namespace Calischool.Controllers
{

    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAccountServices _accountServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        public AdminController(ApplicationDbContext db, IAccountServices accountServices, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _db = db;
            _accountServices = accountServices;
            _userManager = userManager;
            _signManager = signManager;
        }
        // [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Dashboard()
        {
            var applicationUser = new List<ApplicationUser>();
            var users = _db.StudentRegisters.Where(a => a.RegNumber != null && a.Email != null/* && a.Deleted == false*/).ToList();
            if (users.Count > 0)
            {
                return View(users);
            }
            return View(applicationUser);
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
                return RedirectToAction("Dashboard");
            }
            return View(registerView);
        }
    }


}
