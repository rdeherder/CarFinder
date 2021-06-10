using AutoMapper;
using CarFinderApi.Library.Models;
using CarFinderWebApi.Models;

namespace CarFinderWebApi.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<ExternalCarModel, CarDTO>().ReverseMap();
        }
    }
}
