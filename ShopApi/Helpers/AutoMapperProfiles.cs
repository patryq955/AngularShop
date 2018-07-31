using AutoMapper;
using ShopApi.Dtos;
using ShopApi.Models;

namespace ShopApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDetailedDto>().
            ForMember(dest => dest.Age, opt =>
            {
                opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            }).
            ForMember(dest => dest.UrlPhoto, opt =>
            {
                opt.MapFrom(src => src.Url);
            });

        }
    }
}