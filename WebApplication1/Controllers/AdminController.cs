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
        [HttpGet]
        public IActionResult Index()
        {
            var theCurrentUserName = User.Identity.Name;
            if(theCurrentUserName != null)
            {
                var theCurrentUserDetails = _userManager.FindByNameAsync(theCurrentUserName).Result;
                if (theCurrentUserDetails != null)
                {
                    return View(theCurrentUserDetails);
                }
            }
            return RedirectToAction("Login","Account");
        }
        
      //GET (the ApplicationUser table)
        [HttpGet]
        public IActionResult Dashboard(string searchParams, ErrorViewModel message)
        {
            var errorViewModel = new ErrorViewModel();
            //searching for ApplicationUser from the table
            ViewData["currentfilter"] = searchParams;
            var findUserFromTable = _accountServices.SearchUserFromTheTable(searchParams);
            if (findUserFromTable != null)
            {
                errorViewModel.Users = findUserFromTable;
               
                return View(errorViewModel);
            }
            // default table view if search is empty
            //var applicationUser = new List<ApplicationUser>();

            var getUsersFromTable = _accountServices.GetUserFromTheTable();
            if (getUsersFromTable.Count() > 0)
            {

                if (message.Message != null)
                {
                    errorViewModel.Message = message.Message;
                    errorViewModel.ErrorHappened = message.ErrorHappened;
                }
                errorViewModel.Users = getUsersFromTable;
                return View(errorViewModel);
            }
            return View(errorViewModel);
        }
        // GET (the Admin edit form)
        [HttpGet]
        public IActionResult Edit()
        {
            var userName = User.Identity.Name;

            var detailsOfMyUser = _accountServices.GetUserDetailsForEdit(userName);

            return View(detailsOfMyUser);

        }
        // POST (the Admin Edited details)
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
                newlyeditedUserRecord.ErrorHappened = true;
                //TempData["Success"] = "Account Updated successfully!";
                return RedirectToAction("Edit");
             }
            return View(newlyeditedUserRecord);
        }
        // Deleting the ApplicationUser from the Table
        
        public IActionResult Delete(string userId)
        {
            var errorViewModel = new ErrorViewModel();

            var userDetail = _userManager.FindByIdAsync(userId).Result;

            if (userDetail != null)
            {
                userDetail.Deactiveted = true;
                _db.Update(userDetail);
                _db.SaveChanges();
                errorViewModel.Message = "Account Updated successfully!";
                errorViewModel.ErrorHappened = false;
                
                return RedirectToAction("Dashboard",new { searchParams = "", message = errorViewModel});
            }
            errorViewModel.Message = "An error occured please try again !";
            errorViewModel.ErrorHappened = true;

            return RedirectToAction("Dashboard",errorViewModel);
        }

        //Assigning ApplicationUser To Admin Roles
        public async Task<IActionResult> AddAdminToRole(string userToAdmin)
        {
            
            var userDetail = _userManager.FindByIdAsync(userToAdmin).Result;
            if (userDetail != null)
            {
                
                   await _userManager.AddToRoleAsync(userDetail, "Admin");
                    userDetail.IsAdmin = true;
                    _db.Update(userDetail);
                
               
                _db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
           return View();
        }
        public async Task<IActionResult> RemoveUserToRole(string userToAdmin)
        {
            
            var userDetail = _userManager.FindByIdAsync(userToAdmin).Result;
            if (userDetail != null)
            {
               await _userManager.RemoveFromRoleAsync(userDetail, "Admin");
                    userDetail.IsAdmin = false;
                    _db.Update(userDetail);
               
                _db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            return View();
        }

    }
}
