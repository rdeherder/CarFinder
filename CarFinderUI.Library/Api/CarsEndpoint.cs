using CarFinderUI.Library.Models;
using Microsoft.Extensions.Logging;
using Polly;
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
        private readonly ILogger<CarsEndpoint> _logger;

        public CarsEndpoint(IApiHelper apiHelper, ILogger<CarsEndpoint> logger)
        {
            _apiHelper = apiHelper;
            _logger = logger;
        }

        public async Task<IEnumerable<CarModel>> GetAllAsync(CancellationToken token)
        {
            IEnumerable<CarModel> result = null;
            var maxRetryAttempts = 5;
            var pauseBetweenFailures = TimeSpan.FromSeconds(3);
            var retryPolicy = Policy.Handle<Exception>()
                                    .WaitAndRetryAsync(maxRetryAttempts, 
                                                       p => pauseBetweenFailures);

            await retryPolicy.ExecuteAsync(async () =>
            {
                try
                {
                    using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("/cars", token);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsAsync<IEnumerable<CarModel>>();
                    }
                    else
                    {
                        _logger.LogError(response.ReasonPhrase);
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("CarFinderUI.Library-GetAllAsync() cancelled by user.");
                }
            });

            if (result is null)
            {
                _logger.LogInformation($"No data received after {maxRetryAttempts} retries.");
            }

            return result;
        }

        public async Task<CarModel> GetAsync(int id)
        {
            CarModel result = null;
            var maxRetryAttempts = 5;
            var pauseBetweenFailures = TimeSpan.FromSeconds(3);
            var retryPolicy = Policy.Handle<Exception>()
                                    .WaitAndRetryAsync(maxRetryAttempts,
                                                       p => pauseBetweenFailures);

            await retryPolicy.ExecuteAsync(async () =>
            {
                using HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync($"/cars/{id}");
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<CarModel>();
                }
                else
                {
                    _logger.LogError(response.ReasonPhrase);
                    throw new Exception(response.ReasonPhrase);
                }
            });

            if (result is null)
            {
                _logger.LogInformation($"No data received after {maxRetryAttempts} retries.");
            }

            return result;
        }
    }
}
