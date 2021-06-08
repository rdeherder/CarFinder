using CarFinderApi.Library.Api;
using CarFinderApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarFinderApi.Library.ExternalDataAccess
{
    public class ExternalCarsData : IExternalCarsData
    {
        private readonly IApiHelper _apiHelper;

        public ExternalCarsData(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<List<ExternalCarModel>> GetCars()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/cars"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<List<ExternalCarModel>>();
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
