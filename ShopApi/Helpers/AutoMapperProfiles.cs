using AutoMapper;
using ShopApi.Dtos;
using ShopApi.Models;

namespace ShopApi.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDetailDto>().
            ForMember(dest => dest.Age, opt =>
            {
                opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            }).
            ForMember(dest => dest.UrlPhoto, opt =>
            {
                opt.MapFrom(src => src.Url);
            });

            CreateMap<House,HouseForListDto>().
            ForMember(dest => dest.UserName,opt =>{
                opt.MapFrom(src => src.User.UserName);
            });

            CreateMap<House,HouseDetailDto>().
            ForMember(dest => dest.UserName,opt =>{
                opt.MapFrom(src => src.User.UserName);
            });

            CreateMap<UserForUpdateDto,User>();

        }
    }
}