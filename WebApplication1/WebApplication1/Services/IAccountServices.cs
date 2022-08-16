using Calischool.Models;
using Calischool.ViewModel;

namespace Calischool.Services
{
    public interface IAccountServices
    {
        ApplicationUser Login(LoginViewModel loginViewModel);
        ApplicationUser RegisterUserDetails(RegisterViewModel obj);
        //ApplicationUser Edit(int? id);
    }
}
