using CarFinderUI.Library.Models;
using System.Threading.Tasks;

namespace CarFinderUI.BlazorApp.Data
{
    public interface ICarService
    {
        Task<CarModel> GetCarByIdAsync(int id);
    }
}