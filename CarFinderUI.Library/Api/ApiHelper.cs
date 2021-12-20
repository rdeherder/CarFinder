using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;

namespace CarFinderUI.Library.Api
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;

        public HttpClient ApiClient => _apiClient;

        public ApiHelper(IConfiguration config)
        {
            InitializeClient(config);
        }

        private void InitializeClient(IConfiguration config)
        {
            string api = config.GetValue<string>("Api");
            _apiClient = new();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
