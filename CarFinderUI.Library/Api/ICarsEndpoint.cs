using CarFinderUI.Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarFinderUI.Library.Api
{
    public interface ICarsEndpoint
    {
        Task<CarModel> GetAsync(int id);
        Task<IEnumerable<CarModel>> GetAllAsync();
    }
}