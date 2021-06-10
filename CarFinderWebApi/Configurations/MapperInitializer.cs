using AutoMapper;
using CarFinderApi.Models;
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
