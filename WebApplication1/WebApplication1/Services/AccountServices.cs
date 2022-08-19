using Calischool.Data;
using Calischool.Models;
using Calischool.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Services.Users;

namespace Calischool.Services
{
    public class AccountServices: IAccountServices
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        
        public AccountServices(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _db = db;
            _userManager = userManager;
            _signManager = signManager;
        }
        public ApplicationUser Login(LoginViewModel loginViewModel)
        {

            //string mail = "user not find";
            if (loginViewModel.Email == null)
            {
                return null;
            }
            var getUserEmail = _userManager.FindByEmailAsync( loginViewModel.Email ).Result;
            if (getUserEmail != null)
            {
                var userCheck = _signManager.CanSignInAsync(getUserEmail).Result;
                if (userCheck)
                {
                   var checkUser = _signManager.PasswordSignInAsync(getUserEmail, loginViewModel.Password, true, false).Result;
                    return getUserEmail;
                }
            }
            return null;
        }
        public async Task <ApplicationUser> RegisterUserDetails(RegisterViewModel obj)
        {
            var newdb = new ApplicationUser
            {
                UserName = obj.Email,
                Email = obj.Email,
                Gender = obj.Gender,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                PhoneNumber = obj.PhoneNumber,
            };
          
           return newdb;
        }
        public EditStudentViewModel GetUserDetails(string userName)
        {
            var userDetail = _userManager.FindByNameAsync(userName).Result;
            if (userDetail != null)
            {
                var newStudent = new EditStudentViewModel
                {
                    Email = userDetail.Email,
                    Gender = userDetail.Gender,
                    FirstName = userDetail.FirstName,
                    LastName = userDetail.LastName,
                    MiddleName = userDetail.MiddleName,
                    DateOfBirth = userDetail.DateOfBirth,
                    RedsidentAddress = userDetail.RedsidentAddress,
                    Country = userDetail.Country,
                    State = userDetail.State,
                    LGA = userDetail.LGA,
                    Qualification = userDetail.Qualification,
                    Displine = userDetail.Displine,
                };
                 return newStudent;
            }
            return null;
        }
    }

}

    //    {
    //        if (user.Password != user.ConfirmPassword)
    //        {
    //            SetMessage("Password and Confirm Password not matched", Message.Category.Error);
    //            return View(user);
    //        }
    //        var validateEmail = await _userHelper.FindByEmailAsync(user.Email);
    //        if (validateEmail != null)
    //        {
    //            SetMessage("A user with this email already exist", Message.Category.Error);
    //            return View(user);
    //        }
    //        user.UserName = user.Email;
    //        var result = await _userHelper.CreateAdminAsync(user);
    //        var newUser = _userHelper.FindByEmailAsync(user.Email).Result;
    //        if (result == true)
    //        {
    //            await _userManager.AddToRoleAsync(user, "Admin");
    //            await _signInManager.SignInAsync(user, isPersistent: true);
    //            string linkToClick = HttpContext.Request.Scheme.ToString() + "://" +
    //                    HttpContext.Request.Host.ToString() + "/Account/Login";

//            _emailHelper.EmployeeRegistrationTemplateEmailer(newUser, linkToClick);
//            return RedirectToAction("Index", "SuperAdmin");
//        }
//    }
//    catch (Exception ex)
//    {

//        throw ex;
//    }

//    return View();
