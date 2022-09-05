using Calischool.Data;
using Calischool.Models;
using Calischool.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Calischool.Controllers
{
    public class MentorshipController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAccountServices _accountServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        public MentorshipController(ApplicationDbContext db, IAccountServices accountServices, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager)
        {
            _db = db;
            _accountServices = accountServices;
            _userManager = userManager;
            _signManager = signManager;
        }
        [HttpGet]
        public IActionResult StudentMentorship()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StudentMentorship(Mentorship mentorshipDetails)
        {
            var StudentInNeedOfMentor = _accountServices.StudentMentorship(mentorshipDetails);
            if(StudentInNeedOfMentor == null)
            {
                mentorshipDetails.Message = "Enter Email and Course for Mentorship";
                mentorshipDetails.ErrorHappened = true;
                return View(mentorshipDetails);
            }
            else
            {
                
                mentorshipDetails.Message = "You have successfully book for Mentorship";
                mentorshipDetails.ErrorHappened = false;
                return RedirectToAction("Index","Student");
            }
        }
        [HttpGet]
        public IActionResult MentorshipTable()
        {
            var newMentorshpModel = new ErrorViewModel();
            var theListOfMentee = _accountServices.GetToMentorList();
            if(theListOfMentee != null)
            {
                newMentorshpModel.StudentMentorship = theListOfMentee;
                return View(newMentorshpModel);
            }
            return View();
        }
        [HttpGet]
        public IActionResult MentorshipPosting(int? studentId)
        {
            var theCurrentUser = User.Identity.Name;
            var users = _userManager.FindByNameAsync(theCurrentUser).Result;
            if(users != null)
            {
                var currentUser = _userManager.IsInRoleAsync(users, "Admin").Result;
                if (currentUser)
                {
                    var matchingTheStudentToMentor = _accountServices.SignStudentToMentor(studentId);
                    if (matchingTheStudentToMentor != null)
                    {
                        return View(matchingTheStudentToMentor);
                    }
                }
                return RedirectToAction("Index","Student");
            }

          
            return View();
        }
        [HttpPost]
        public IActionResult MentorPosting(Mentorship mentorshipDetails)
        { 
            var newMentorship = new ErrorViewModel();
            var studentMentorshipPosting = _accountServices.MentorPosting(mentorshipDetails);
            if(studentMentorshipPosting != null)
            {
                newMentorship.Message = "you have successfully assign mentor to the Student!";
                newMentorship.ErrorHappened = false;
               
                return RedirectToAction("MentorshipTable");
            }
            else
            {
                return View();
            }
            
        }
    }
}
