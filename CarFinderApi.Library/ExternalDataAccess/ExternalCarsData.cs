using CarFinderApi.Library.Api;
using CarFinderApi.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarFinderApi.Library.ExternalDataAccess
{
    public class ExternalCarsData : IExternalCarsData
    {
        private readonly IApiHelper _apiHelper;
        private List<ExternalCarModel> _externalCars;

        public ExternalCarsData(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<IEnumerable<ExternalCarModel>> GetCars()
        {
            if (_externalCars == null)
            {
                await RetrieveAllFromExternalApi();
            }

            return _externalCars;
        }

        public async Task RetrieveAllFromExternalApi()
        {
            using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/cars");
            if (response.IsSuccessStatusCode)
            {
                _externalCars = await response.Content.ReadAsAsync<List<ExternalCarModel>>();
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<ExternalCarModel> GetCar(int id)
        {
            if (_externalCars == null)
            {
                var result = await RetrieveFromExternalApi(id);
                return result;
            }

            return _externalCars.Where(c => c.Id == id).SingleOrDefault();
        }

        private async Task<ExternalCarModel> RetrieveFromExternalApi(int id)
        {
            using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/cars/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<ExternalCarModel>();
                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }

    }
}
