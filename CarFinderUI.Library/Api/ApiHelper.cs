using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CarFinderUI.Library.Api
{
    public class ApiHelper : IApiHelper
    {
        private HttpClient _apiClient;
        private readonly IConfiguration _config;

        public HttpClient ApiClient => _apiClient;

        public ApiHelper(IConfiguration config)
        {
            _config = config;

            InitializeClient();
        }

        private void InitializeClient()
        {
            string api = _config.GetValue<string>("Api");
            _apiClient = new();
            _apiClient.BaseAddress = new Uri(api);
            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
