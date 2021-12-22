using CarFinderUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
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

        public async Task<IEnumerable<CarModel>> GetAllAsync(CancellationToken token)
        {
            try
            {
                using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/cars", token);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<IEnumerable<CarModel>>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("GetAllCars cancelled by user.");
            }
            return null;
        }

        public async Task<CarModel> GetAsync(int id)
        {
            using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/cars/{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsAsync<CarModel>();
                return result;
            }
            else
            {
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
