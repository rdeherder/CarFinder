using CarFinderUI.Library.Api;
using CarFinderUI.Library.Models;
using GridMvc.Server;
using GridShared;
using GridShared.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarFinderUI.Blazor.Data
{
    public class CarService : ICarService
    {
        private readonly ICarsEndpoint _carsEndpoint;

        public CancellationTokenSource GetCarsTokenSource { get; set; } = new CancellationTokenSource();

        public CarService(ICarsEndpoint carsEndpoint)
        {
            _carsEndpoint = carsEndpoint;
        }

        public async Task<ItemsDTO<CarModel>> GetCarsGridRowsAsync(Action<IGridColumnCollection<CarModel>> columns,
                                                                   QueryDictionary<StringValues> query,
                                                                   CancellationToken token)
        {
            var items = await _carsEndpoint.GetAllAsync(token);

            var server = new GridServer<CarModel>(items,
                                                  new QueryCollection(query),
                                                  true,
                                                  "carsGrid",
                                                  columns,
                                                  10)
                                                  .ChangePageSize(enable: true)
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
