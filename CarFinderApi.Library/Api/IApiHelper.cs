using System.Net.Http;

namespace CarFinderApi.Library.Api
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }
    }
}