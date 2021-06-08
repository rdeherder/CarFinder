using CarFinderUI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFinderUI.Models
{
    public class CarDTO
    {
        public List<CarModel> Cars { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
