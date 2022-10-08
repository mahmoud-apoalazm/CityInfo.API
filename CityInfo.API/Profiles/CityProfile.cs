using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entites.City, Models.CityWithoutPointOfInterestDto>();
            CreateMap<Entites.City, Models.CityDto>();

        }
    }
}
