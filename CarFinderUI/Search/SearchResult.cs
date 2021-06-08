using CarFinderUI.Library.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFinderUI.Search
{
    public class SearchResult
    {
        public IPagedList<CarModel> SearchHits { get; set; }

        public string SearchQuery { get; set; }
    }
}
