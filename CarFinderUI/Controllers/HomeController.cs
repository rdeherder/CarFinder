using CarFinderUI.Library.Api;
using CarFinderUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CarFinderUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICarsEndpoint _carsEndpoint;

        public HomeController(ICarsEndpoint carsEndpoint)
        {
            _carsEndpoint = carsEndpoint;
        }

        public async Task<IActionResult> Index(int page = 1, int maxRows = 10)
        {
            var model = await GetCars(page, maxRows);
            return View(model);
        }

        private async Task<CarDTO> GetCars(int currentPageIndex, int maxRows)
        {
            var cars = await _carsEndpoint.GetCars();

            var pageCount = cars.Count / maxRows;
            var retVal = new CarDTO();

            retVal.CurrentPageIndex = currentPageIndex;
            retVal.PageCount = (int)Math.Ceiling((decimal)pageCount);
            retVal.Cars = cars.OrderBy(o => o.Id)
              .Skip((currentPageIndex - 1) * maxRows)
              .Take(maxRows)
              .ToList();

            return retVal;
        }
    }
}
