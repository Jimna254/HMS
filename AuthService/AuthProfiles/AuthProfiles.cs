using AuthService.Models;
using AuthService.Models.DTO_s;
using AutoMapper;

namespace AuthService.AuthProfiles
{
    public class AuthProfiles:Profile

    {
        public AuthProfiles()
        {
            CreateMap<RegisterDTO, AppUser>().ForMember(dest => dest.UserName, u => u.MapFrom(reg => reg.Email));
            CreateMap<AppUser, UserDTO>().ReverseMap();

        }

    }
}
