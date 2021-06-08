using CarFinderUI.Library.Models;
using System.Collections.Generic;

namespace CarFinderUI.Search.Services
{
    public interface ISearchService
    {
        SearchResult GetSearchResult(List<CarModel> searchData, string query, int page, int pageSize);
    }
}