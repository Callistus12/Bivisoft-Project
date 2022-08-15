using Calischool.Data;
using Calischool.Models;
using Calischool.ViewModel;
using Microsoft.AspNetCore.Identity;

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
                    _signManager.PasswordSignInAsync(getUserEmail, loginViewModel.Password, true, false);
                    return getUserEmail;
                }
            }
            return null;
        }
        public ApplicationUser RegisterUserDetails(RegisterViewModel obj)
        {
            var newdb = new ApplicationUser
            {
                UserName = obj.Email,
                Email = obj.Email,
                Gender = obj.Gender,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                MiddleName = obj.MiddleName,
                DateOfBirth = obj.DateOfBirth,
                RedsidentAddress = obj.RedsidentAddress,
                Country = obj.Country,
                State = obj.State,
                LGA = obj.LGA,
                Qualification = obj.Qualification,
                Displine = obj.Displine,
            };
          var checkEmail = _userManager.FindByEmailAsync(obj.Email).Result;
            if (checkEmail != null)
            {
                return null;
            }
            var creatUser = _userManager.CreateAsync(newdb, obj.Password).Result;
            if (creatUser.Succeeded)
            { 

               //var registeredUserIsRole = _db.StudentRegisters.Where(a => a.Password == "====");
                if (obj.Password == "====")
                {
                    var registeredUser = _userManager.FindByEmailAsync(obj.Email).Result;
                    _userManager.AddToRoleAsync(registeredUser, "Admin");
                    return registeredUser;
                }
                else
                {
                    var registeredUser = _userManager.FindByEmailAsync(obj.Email).Result;
                    _userManager.AddToRoleAsync(registeredUser, "Student");
                    return registeredUser;
                }
            }
           return null;
        }
        public ApplicationUser Edit(int? id)
        {
            if(id == 0 || id == null)
            {
                return null;
            }
           
            var userIdFromDb = _db.StudentRegisters.Find(id);
            if(userIdFromDb == null)
            {
                return null;
            }
            return userIdFromDb;
            
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
