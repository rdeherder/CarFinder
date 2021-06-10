using CarFinderUI.Blazor.Models;
using System.Threading.Tasks;

namespace CarFinderUI.Blazor.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
        Task Logout();
    }
}