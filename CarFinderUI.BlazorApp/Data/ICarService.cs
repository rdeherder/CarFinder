using CarFinderUI.Library.Models;
using GridShared;
using GridShared.Utility;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace CarFinderUI.BlazorApp.Data
{
    public interface ICarService
    {
        Task<CarModel> GetCarByIdAsync(int id);
    }
}