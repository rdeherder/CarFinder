using CarFinderUI.Library.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFinderUI.Search.Services
{
    public class SearchService : ISearchService
    {
        public SearchResult GetSearchResult(List<CarModel> searchData, string query, int page, int pageSize)
        {
            var searchHits = searchData.Where(x => x.Make.Contains(query, StringComparison.CurrentCultureIgnoreCase));

            var searchResult = new SearchResult()
            {
                SearchHits = new StaticPagedList<CarModel>(searchHits.Skip((page - 1) * pageSize).Take(pageSize), page, pageSize, searchHits.Count()),
                SearchQuery = query
            };

            return searchResult;
        }
    }
}
