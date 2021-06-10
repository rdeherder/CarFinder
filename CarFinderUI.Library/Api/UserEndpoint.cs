using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarFinderUI.Library.Models;

namespace CarFinderUI.Library.Api
{
    public class UserEndpoint : IUserEndpoint
    {
        private IApiHelper _apiHelper;

        public UserEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task CreateUser(CreateUserModel user)
        {
            var data = new
            {
                user.FirstName,         // Als de propertynaam van het doelmodel (bv Voornaam) 
                user.LastName,          // niet overeen komt met die van het user-model hier,
                user.EmailAddress,      // dan dien je de naam van de doelproperty er voor te zetten, dus
                user.Password           // bv Voornaam = user.FirstName
            };

            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Register", data))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    // Zelf toegevoegd zodat de fouten worden getoond.
                    var jsonErrors = await response.Content.ReadAsStringAsync();
                    var errors = JsonSerializer.Deserialize<List<RegistrationErrorModel>>(jsonErrors, 
                                                                                          new JsonSerializerOptions 
                                                                                          { 
                                                                                              PropertyNameCaseInsensitive = true 
                                                                                          });
                    string errorMessage = string.Empty;
                    foreach (var item in errors)
                    {
                        errorMessage += item.Description;       // Een NewLine toevoegen heeft geen effect. De errors worden alsnog achter elkaar getoond.
                    }
                    throw new Exception(errorMessage);
                }
            }
        }

        public async Task<List<UserModel>> GetAll()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/User/Admin/GetAllUsers"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<UserModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task<Dictionary<string, string>> GetAllRoles()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/User/Admin/GetAllRoles"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<Dictionary<string, string>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task AddUserToRole(string userId, string roleName)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Admin/AddRole", new { userId, roleName }))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task RemoveUserFromRole(string userId, string roleName)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/User/Admin/RemoveRole", new { userId, roleName }))
            {
                if (response.IsSuccessStatusCode == false)
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
