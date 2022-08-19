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
                return RedirectToAction("Dashboard");
            }
            return View(obj);
        }
    }
}

   



