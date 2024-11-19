using AutoMapper;
using UserManagement.Data;
using UserManagement.ViewModels;

namespace UserManagement.Utility;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<ApplicationUser, SignUpViewModel>().ReverseMap()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap();
    }
}
