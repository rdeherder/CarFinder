using CarFinderUI.Blazor.Pages;
using CarFinderUI.Library.Api;
using CarFinderUI.Library.Models;
using GridMvc.Server;
using GridShared;
using GridShared.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarFinderUI.Blazor.Data
{
    public class CarService : ICarService
    {
        private readonly ICarsEndpoint _carsEndpoint;

        public CarService(ICarsEndpoint carsEndpoint)
        {
            _carsEndpoint = carsEndpoint;
        }

        public async Task<ItemsDTO<CarModel>> GetCarsGridRowsAsync(Action<IGridColumnCollection<CarModel>> columns,
                QueryDictionary<StringValues> query)
        {
            var items = await _carsEndpoint.GetAllAsync();

            var server = new GridServer<CarModel>(items,
                            new QueryCollection(query),
                            true,
                            "carsGrid",
                            columns,
                            10)
                            .Filterable()
                            .Searchable()
                            .Selectable(set: true)
                            .Sortable()
                            .WithMultipleFilters();

            return server.ItemsToDisplay;
        }

        public async Task<CarModel> GetCarByIdAsync(int id)
        {
            var car = await _carsEndpoint.GetAsync(id);
            return car;
        }
    }
}
