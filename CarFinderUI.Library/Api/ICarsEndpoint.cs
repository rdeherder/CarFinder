using CarFinderUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarFinderUI.Library.Api
{
    public interface ICarsEndpoint
    {
        Task<List<CarModel>> GetCars();
    }
}