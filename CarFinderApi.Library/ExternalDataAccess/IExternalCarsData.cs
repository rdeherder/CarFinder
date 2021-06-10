using CarFinderApi.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarFinderApi.Library.ExternalDataAccess
{
    public interface IExternalCarsData
    {
        Task<ExternalCarModel> GetCar(int id);
        Task<IEnumerable<ExternalCarModel>> GetCars();
    }
}