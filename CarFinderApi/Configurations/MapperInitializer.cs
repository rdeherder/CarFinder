using AutoMapper;
using CarFinderApi.Models;

namespace CarFinderApi.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<ExternalCarModel, CarDTO>().ReverseMap();
        }
    }
}
