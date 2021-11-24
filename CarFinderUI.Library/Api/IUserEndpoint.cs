using CarFinderUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarFinderUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task AddUserToRoleAsync(string userId, string roleName);
        Task CreateUserAsync(CreateUserModel user);
        Task<List<UserModel>> GetAllAsync();
        Task<Dictionary<string, string>> GetAllRolesAsync();
        Task RemoveUserFromRoleAsync(string userId, string roleName);
    }
}