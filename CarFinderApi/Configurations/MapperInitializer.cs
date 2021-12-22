using AutoMapper;
using CarFinderApi.Extensions;
using CarFinderApi.Library.Models;
using CarFinderApi.Models;

namespace CarFinderApi.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<ExternalCarModel, CarDTO>()
                .ForMember(c => c.Make, c => c.MapFrom(c => c.Make.Capitalize()))
                .ForMember(c => c.Model, c => c.MapFrom(c => c.Model.Capitalize()));
        }
    }
}
