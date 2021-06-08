using CarFinderUI.Library.Api;
using CarFinderUI.Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarFinderUI.Library.Api
{
    public class CarsEndpoint : ICarsEndpoint
    {
        private readonly IApiHelper _apiHelper;

        public CarsEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<CarModel>> GetCars()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/api/cars"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<CarModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }
    }
}
