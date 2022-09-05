using Calischool.Data;
using Calischool.Models;
using Calischool.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace Calischool.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AccountServices(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signManager, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _userManager = userManager;
            _signManager = signManager;
            _webHostEnvironment = webHostEnvironment;
        }
        // login the user
        public ApplicationUser Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (loginViewModel.Email == null)
                {
                    return null;
                }
                var getUserEmail = _userManager.FindByEmailAsync(loginViewModel.Email).Result;
                if (getUserEmail != null)
                {
                    var userCheck = _signManager.CanSignInAsync(getUserEmail).Result;
                    if (userCheck)
                    {
                        var checkUser = _signManager.PasswordSignInAsync(getUserEmail, loginViewModel.Password, true, false).Result;
                        if (checkUser.Succeeded)
                        {
                            return getUserEmail;
                        }
                        return null;
                    }
                }

                return null;
            }
            catch (Exception)
            {
                throw;
            }


        }
        // registering the user as a new member
        public ApplicationUser RegisterUserDetails(RegisterViewModel obj)
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
        // Getting existing User from the usermanager.
        public RegisterViewModel GetUserDetailsForEdit(string userName)
        {
            var userDetail = _userManager.FindByNameAsync(userName).Result;
            if (userDetail != null)
            {
                var newStudent = new RegisterViewModel
                {
                    Email = userDetail.Email,
                    Gender = userDetail.Gender,
                    FirstName = userDetail.FirstName,
                    MiddleName = userDetail.MiddleName,
                    LastName = userDetail.LastName,
                    PhoneNumber = userDetail.PhoneNumber,
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
        // Searching student from the student table.
        public IQueryable<ApplicationUser> SearchUserFromTheTable(string userDetailToFetch)
        {
            var SearchUserFromTheTable = from A in _db.StudentRegisters
                                         select A;
            if (!string.IsNullOrEmpty(userDetailToFetch))
            {
                SearchUserFromTheTable = SearchUserFromTheTable.Where(a => a.FirstName.Contains(userDetailToFetch) && a.Deactiveted != true);

                return SearchUserFromTheTable;
            }

            return null;
        }
        public List<ApplicationUser> GetUserFromTheTable()
        {
            var users = _db.StudentRegisters.Where(a => a.FirstName != null && a.Email != null && a.Deactiveted != true).ToList();
            if (users.Any())
            {
                return users;
            }
            return null;
        }

        // updating the ApplicationUser Details
        public bool UpdateTheEditedUserDetails(RegisterViewModel newlyeditedUserRecord)
        {
            try
            {
                var usersOldRecord = _userManager.FindByEmailAsync(newlyeditedUserRecord.Email).Result;
                if (usersOldRecord != null)
                {
                    usersOldRecord.Country = newlyeditedUserRecord.Country;
                    usersOldRecord.Displine = newlyeditedUserRecord.Displine;
                    usersOldRecord.DateOfBirth = newlyeditedUserRecord.DateOfBirth;
                    usersOldRecord.FirstName = newlyeditedUserRecord.FirstName;
                    usersOldRecord.MiddleName = newlyeditedUserRecord.MiddleName;
                    usersOldRecord.PhoneNumber = newlyeditedUserRecord.PhoneNumber;
                    usersOldRecord.LastName = newlyeditedUserRecord.LastName;
                    usersOldRecord.Email = newlyeditedUserRecord.Email;
                    usersOldRecord.Gender = newlyeditedUserRecord.Gender;
                    usersOldRecord.State = newlyeditedUserRecord.State;
                    usersOldRecord.Qualification = newlyeditedUserRecord.Qualification;
                    usersOldRecord.RedsidentAddress = newlyeditedUserRecord.RedsidentAddress;
                    usersOldRecord.LGA = newlyeditedUserRecord.LGA;
                    usersOldRecord.PhoneNumber = newlyeditedUserRecord.PhoneNumber;

                    _db.StudentRegisters.Update(usersOldRecord);
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public Materials GetUserNameForPosting(string theUserForThePosting)
        //{
        //    var theUserDetailsForThePosting = _userManager.FindByNameAsync(theUserForThePosting).Result;
        //    if (theUserDetailsForThePosting != null)
        //    {
        //        var theActualUser = new Materials
        //        {
        //            UserId = theUserDetailsForThePosting.Id,
        //        };
        //        return theActualUser;

        //    }
        //    return null;
        //}

        public string UploadFile(Materials fileToSend)
        {
           
            var uniqueFileName = string.Empty;
            if (fileToSend.FileUploadedFromDevice != null)
            {
                string UploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Materialspdf");
                string pathString = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Materialspdf");
                if (!Directory.Exists(pathString))
                {
                    Directory.CreateDirectory(pathString);
                }
                uniqueFileName = Guid.NewGuid().ToString() + "- " + fileToSend.FileUploadedFromDevice.FileName;
                string filepath = Path.Combine(pathString, uniqueFileName);
                using (var fileStream = new FileStream(filepath,FileMode.Create))
                {
                    fileToSend.FileUploadedFromDevice.CopyTo(fileStream);
                }
                var generatedPdfFilePath = "/Materialspdf/" + uniqueFileName;
                return generatedPdfFilePath;
            }
            return null;
        }
        public Materials uploadFileAndMaterialCoursesToDb(Materials userDetails) 
        {
            string uniqueFileFromUser = string.Empty;
            if (userDetails.FileUploadedFromDevice != null )
            {
                uniqueFileFromUser = UploadFile(userDetails);
                var collectingUserInfo = new Materials
                {
                    UserId = userDetails.UserId,
                    FileUrl = uniqueFileFromUser,
                    Description = userDetails.Description,
                    CourseTitle = userDetails.CourseTitle
                };
                _db.StudentMaterials.Add(collectingUserInfo);
                _db.SaveChanges();

                return collectingUserInfo;
            }
            else
            {
                return null;
            }
        }
        public Mentorship StudentMentorship(Mentorship mentorshipDetails)
        {
            if(mentorshipDetails.CourseToMentor == null && mentorshipDetails.Email == null)
            {
                return null;
            }
            else
            {
                var StudentInNeedOfMentor = new Mentorship
                {
                    FirstName = mentorshipDetails.FirstName,
                    LastName = mentorshipDetails.LastName,
                    CourseToMentor = mentorshipDetails.CourseToMentor,
                    Email = mentorshipDetails.Email,
                    CourseDuration = mentorshipDetails.CourseDuration,
                };
                _db.CourseMentorship.Add(StudentInNeedOfMentor);
                _db.SaveChanges();
                return StudentInNeedOfMentor;
            }

        }

        public List<Materials> GetMaterials()
        {
            var getAllTheMaterials = _db.StudentMaterials.Where(a => a.UserId != null && a.CourseTitle != null).ToList();
            if (getAllTheMaterials.Any())
            {
                return getAllTheMaterials;
            }
            return null;
       }
        public IQueryable<Mentorship> GetToMentorList()
        {
            var theUserThatNeedsMentor = _db.CourseMentorship.Where(a => a.CourseToMentor != null);
            if (theUserThatNeedsMentor.Any())
            {
                return theUserThatNeedsMentor;
            }
            return null;
        }
        public Mentorship SignStudentToMentor(int? studentId)
        {
            var getTheUserToPost = _db.CourseMentorship.Find(studentId);
            if (getTheUserToPost != null)
            {
                var StudentDetailsForMentorsip = new Mentorship
                {
                    Id = getTheUserToPost.Id,
                    FirstName = getTheUserToPost.FirstName,
                    LastName = getTheUserToPost.LastName,
                    Email = getTheUserToPost.Email,
                    CourseToMentor = getTheUserToPost.CourseToMentor,
                };
                return StudentDetailsForMentorsip;
            }
            return null;
        }
        public Mentorship MentorPosting(Mentorship mentorshipDetails)
        {
            try
            {
                if(mentorshipDetails.NameOfTheMentor != null && mentorshipDetails.PhoneNumberOfMentor != null)
                {
                    var userToAssign = _db.CourseMentorship.Find(mentorshipDetails.Id);
                    if(userToAssign != null)
                    {
                        userToAssign.Email = mentorshipDetails.Email;
                        userToAssign.CourseDuration = mentorshipDetails.CourseDuration;
                        userToAssign.CourseToMentor = mentorshipDetails.CourseToMentor;
                        userToAssign.PhoneNumberOfMentor = mentorshipDetails.PhoneNumberOfMentor;
                        userToAssign.LastName = mentorshipDetails.LastName;
                        userToAssign.FirstName = mentorshipDetails.FirstName;
                        userToAssign.NameOfTheMentor = mentorshipDetails.NameOfTheMentor;
                        //userToAssign.Id = mentorshipDetails.Id;
                        userToAssign.IsAssigned = true;

                        _db.CourseMentorship.Update(userToAssign);
                        _db.SaveChanges();
                        return userToAssign;
                    }
                }
                return null;
            }
            catch(Exception)
            {
                throw;
            }
        }

    }

}

   