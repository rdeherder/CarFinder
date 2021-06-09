using CarFinderApi.Library.Api;
using CarFinderApi.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
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

        public async Task<List<ExternalCarModel>> GetCars()
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();
            }

            if (_externalCars == null || _stopwatch.Elapsed.TotalMinutes >= _timeOutInMinutes)
            {
                await RetrieveAllFromExternalApi();

                _stopwatch = new Stopwatch();
                _stopwatch.Start();
            }

            return _externalCars;
        }

        private async Task RetrieveAllFromExternalApi()
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

        public async Task<ExternalCarModel> GetCar(int id)
        {
            if (_externalCars == null)
            {
                var result = await RetrieveFromExternalApi(id);
                return result;
            }

            return _externalCars.Where(c => c.Id == id).FirstOrDefault();
        }

        private async Task<ExternalCarModel> RetrieveFromExternalApi(int id)
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
