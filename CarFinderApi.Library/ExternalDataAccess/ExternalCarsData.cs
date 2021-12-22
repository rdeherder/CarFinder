using CarFinderApi.Library.Api;
using CarFinderApi.Library.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ExternalCarsData> _logger;
        private List<ExternalCarModel> _externalCars;

        public ExternalCarsData(IApiHelper apiHelper, ILogger<ExternalCarsData> logger)
        {
            _apiHelper = apiHelper;
            _logger = logger;
        }

        public async Task<IEnumerable<ExternalCarModel>> GetCarsAsync()
        {
            if (_externalCars == null)
            {
                await RetrieveAllFromExternalApiAsync();
            }

            return _externalCars;
        }

        public async Task LoadBufferFromExternalApiAsync()
        {
            await RetrieveAllFromExternalApiAsync();
        }

        private async Task RetrieveAllFromExternalApiAsync()
        {
            using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/cars");
            if (response.IsSuccessStatusCode)
            {
                _externalCars = await response.Content.ReadAsAsync<List<ExternalCarModel>>();
            }
            else
            {
                _logger.LogError($"Error retrieving cars from remote api: {response.ReasonPhrase}");
                throw new Exception(response.ReasonPhrase);
            }
        }

        public async Task<ExternalCarModel> GetCarAsync(int id)
        {
            if (_externalCars == null)
            {
                var result = await RetrieveFromExternalApiAsync(id);
                return result;
            }

            return _externalCars.Where(c => c.Id == id).SingleOrDefault();
        }

        private async Task<ExternalCarModel> RetrieveFromExternalApiAsync(int id)
        {
            using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/cars/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<ExternalCarModel>();
                return result;
            }
            else
            {
                _logger.LogError($"Error retrieving car {id} from remote api: {response.ReasonPhrase}");
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
