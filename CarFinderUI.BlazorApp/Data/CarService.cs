using CarFinderUI.Library.Api;
using CarFinderUI.Library.Models;
using System.Threading.Tasks;

namespace CarFinderUI.BlazorApp.Data
{
    public class CarService : ICarService
    {
        private readonly ICarsEndpoint _carsEndpoint;

        public CarService(ICarsEndpoint carsEndpoint)
        {
            _carsEndpoint = carsEndpoint;
        }

        public async Task<CarModel> GetCarByIdAsync(int id)
        {
            var car = await _carsEndpoint.GetAsync(id);
            return car;
        }
    }
}
