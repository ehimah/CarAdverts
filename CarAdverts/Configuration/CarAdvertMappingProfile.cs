using AutoMapper;
using CarAdverts.Domain.Entity;
using CarAdverts.Models;

namespace CarAdverts.Configuration
{
    public class CarAdvertMappingProfile:Profile
    {
        public CarAdvertMappingProfile()
        {
            CreateMap<CarAdvertRequestModel, CarAdvert>().ReverseMap();
            CreateMap<CarAdvertResponseModel, CarAdvert>().ReverseMap();
            
        }

    }
}
