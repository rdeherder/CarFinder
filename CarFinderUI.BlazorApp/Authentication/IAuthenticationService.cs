using CarFinderUI.BlazorApp.Models;
using System.Threading.Tasks;

namespace CarFinderUI.BlazorApp.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel userForAuthentication);
        Task Logout();
    }
}