using CarFinderUI.Library.Models;
using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace CarFinderUI.Blazor.Data
{
    public interface ICarService
    {
        Task<CarModel> GetCarByIdAsync(int id);
        Task<ItemsDTO<CarModel>> GetCarsGridRowsAsync(Action<IGridColumnCollection<CarModel>> columns, QueryDictionary<StringValues> query);
    }
}