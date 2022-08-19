using Calischool.Models;
using Calischool.ViewModel;

namespace Calischool.Services
{
    public interface IAccountServices
    {
        ApplicationUser Login(LoginViewModel loginViewModel);
        Task<ApplicationUser> RegisterUserDetails(RegisterViewModel obj);
        EditStudentViewModel GetUserDetails(string userName);
    }
}
