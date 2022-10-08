using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile :Profile
    {
        public PointOfInterestProfile()
        {
            CreateMap<Entites.PointOfInterest,Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestForCreationDto , Entites.PointOfInterest>();
            CreateMap<Models.PointOfInterestForUpdateDto, Entites.PointOfInterest>();
            CreateMap<Entites.PointOfInterest, Models.PointOfInterestForUpdateDto>();

        }
    }
}
