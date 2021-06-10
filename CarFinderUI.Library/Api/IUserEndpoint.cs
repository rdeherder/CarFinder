using System.Collections.Generic;
using System.Threading.Tasks;
using CarFinderUI.Library.Models;

namespace CarFinderUI.Library.Api
{
    public interface IUserEndpoint
    {
        Task AddUserToRole(string userId, string roleName);
        Task CreateUser(CreateUserModel user);
        Task<List<UserModel>> GetAll();
        Task<Dictionary<string, string>> GetAllRoles();
        Task RemoveUserFromRole(string userId, string roleName);
    }
}