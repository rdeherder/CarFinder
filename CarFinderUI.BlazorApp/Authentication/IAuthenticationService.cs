using CarFinderUI.BlazorApp.Models;
using System.Threading.Tasks;

namespace CarFinderUI.BlazorApp.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> LoginAsync(AuthenticationUserModel userForAuthentication);
        Task LogoutAsync();
    }
}