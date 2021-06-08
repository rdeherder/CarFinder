using CarFinderApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarFinderApi.Library.ExternalDataAccess
{
    public interface IExternalCarsData
    {
        Task<List<ExternalCarModel>> GetCars();
    }
}