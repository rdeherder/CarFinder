using CarFinderApi.Library.Api;
using CarFinderApi.Library.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CarFinderApi.Library.ExternalDataAccess
{
    public class ExternalCarsData : IExternalCarsData
    {
        private readonly IApiHelper _apiHelper;
        private readonly IConfiguration _config;
        private List<ExternalCarModel> _externalCars;
        private Stopwatch _stopwatch;
        private int _timeOutInMinutes = 0;

        public ExternalCarsData(IApiHelper apiHelper, IConfiguration config)
        {
            _apiHelper = apiHelper;
            _config = config;

            Initialize();
        }

        private void Initialize()
        {
            _stopwatch = new Stopwatch();
            _timeOutInMinutes = _config.GetValue<int>("ExternalCarsApiRefreshTimeOutInMinutes");
        }

        public async Task<IEnumerable<ExternalCarModel>> GetCarsAsync()
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();
            }

            if (_externalCars == null || _stopwatch.Elapsed.TotalMinutes >= _timeOutInMinutes)
            {
                await RetrieveAllFromExternalApiAsync();

                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }

            return _externalCars;
        }

        private async Task RetrieveAllFromExternalApiAsync()
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/cars"))
            {
                if (response.IsSuccessStatusCode)
                {
                    _externalCars = await response.Content.ReadAsAsync<List<ExternalCarModel>>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
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
            using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/cars/{id}"))
            {
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
}
