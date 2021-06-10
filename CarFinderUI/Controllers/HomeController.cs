using PagedList.Core.Mvc;
using CarFinderUI.Library.Api;
using CarFinderUI.Library.Models;
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

        //public async Task<IActionResult> Index(int page = 1, int maxRows = 10)
        //{
        //    try
        //    {
        //        var model = await GetCars(page, maxRows);
        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", ex.Message);
        //        return View("Index");
        //    }
        //}

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                var model = await GetCars(sortOrder, currentFilter, searchString, page);

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                //return View(students.ToPagedList(pageNumber, pageSize));

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View("Index");
            }
        }

        private async Task<CarDTO> GetCars(string sortOrder, string currentFilter, string searchString, int? currentPage, int maxRows = 10)
        {
            var cars = await _carsEndpoint.GetAllAsync();
            var pageCount = cars.Count / maxRows;
            var retVal = new CarDTO();
            int page = currentPage ?? 1;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.IdSortParm = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.MakeSortParm = sortOrder == "Make" ? "make_desc" : "Make";
            ViewBag.MakeSortParm = sortOrder == "Model" ? "model_desc" : "Model";
            ViewBag.MakeSortParm = sortOrder == "Year" ? "year_desc" : "Year";
            ViewBag.MakeSortParm = sortOrder == "HP" ? "hp_desc" : "HP";
            ViewBag.MakeSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (string.IsNullOrEmpty(searchString) == false)
            {
                cars = cars.Where(c => c.Make.Contains(searchString) || c.Model.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "id_desc":
                    cars = cars.OrderByDescending(c => c.Id).ToList();
                    break;
                case "Make":
                    cars = cars.OrderBy(c => c.Make).ToList();
                    break;
                case "make_desc":
                    cars = cars.OrderByDescending(c => c.Make).ToList();
                    break;
                case "Model":
                    cars = cars.OrderBy(c => c.Model).ToList();
                    break;
                case "model_desc":
                    cars = cars.OrderByDescending(c => c.Model).ToList();
                    break;
                case "Year":
                    cars = cars.OrderBy(c => c.Year).ToList();
                    break;
                case "year_desc":
                    cars = cars.OrderByDescending(c => c.Year).ToList();
                    break;
                case "HP":
                    cars = cars.OrderBy(c => c.HP).ToList();
                    break;
                case "hp_desc":
                    cars = cars.OrderByDescending(c => c.HP).ToList();
                    break;
                case "Price":
                    cars = cars.OrderBy(c => c.Price).ToList();
                    break;
                case "price_desc":
                    cars = cars.OrderByDescending(c => c.Price).ToList();
                    break;
                default:
                    cars = cars.OrderBy(c => c.Id).ToList();
                    break;
            }

            retVal.CurrentPageIndex = page;
            retVal.PageCount = (int)Math.Ceiling((decimal)pageCount);
            retVal.Cars = cars.Skip((page - 1) * maxRows)
              .Take(maxRows)
              .ToList();

            return retVal;
        }

        //private async Task<CarDTO> GetCars(int currentPageIndex, int maxRows)
        //{
        //    var cars = await _carsEndpoint.GetCars();
        //    var pageCount = cars.Count / maxRows;
        //    var retVal = new CarDTO();

        //    //throw new Exception("poep");

        //    retVal.CurrentPageIndex = currentPageIndex;
        //    retVal.PageCount = (int)Math.Ceiling((decimal)pageCount);
        //    retVal.Cars = cars.OrderBy(o => o.Id)
        //      .Skip((currentPageIndex - 1) * maxRows)
        //      .Take(maxRows)
        //      .ToList();

        //    return retVal;
        //}
    }
}
