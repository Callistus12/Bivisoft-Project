using Calischool.Models;
using Calischool.ViewModel;

namespace Calischool.Services
{
    public interface IAccountServices
    {
        ApplicationUser Login(LoginViewModel loginViewModel);
        ApplicationUser RegisterUserDetails(RegisterViewModel obj);
        List<ApplicationUser> GetUserFromTheTable();
        RegisterViewModel GetUserDetailsForEdit(string userName);
        IQueryable<ApplicationUser> SearchUserFromTheTable(string userDetailToFetch);
        bool UpdateTheEditedUserDetails(RegisterViewModel newlyeditedUserRecord);
        //Materials GetUserNameForPosting(string theUserForThePosting);
        string UploadFile(Materials fileToSend);
        Materials uploadFileAndMaterialCoursesToDb(Materials userDetails);
        Mentorship StudentMentorship(Mentorship mentorshipDetails);
        List<Materials> GetMaterials();
        IQueryable<Mentorship> GetToMentorList();
        Mentorship SignStudentToMentor(int? studentId);
        public Mentorship MentorPosting(Mentorship mentorshipDetails);

    }
}
