using System.Net.Http;

namespace CarFinderUI.Library.Api
{
    public interface IApiHelper
    {
        HttpClient ApiClient { get; }
    }
}