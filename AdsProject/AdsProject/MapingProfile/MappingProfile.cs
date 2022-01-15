using AutoMapper;
using Models.Models;

namespace UI.MapingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.User, User>();
            CreateMap<User, Models.User>();

            CreateMap<Models.LoginData, User>();
            CreateMap<User, Models.LoginData>();

            CreateMap<Models.ChangePasswordModel, User>();
            CreateMap<User, Models.ChangePasswordModel>();

            CreateMap<Models.Ad, Ad>();
            CreateMap<Ad, Models.Ad>();

            CreateMap<Models.AdParameter, Ad>();
            CreateMap<Ad, Models.AdParameter>();

            CreateMap<Models.Category, Parameter>();
            CreateMap<Parameter, Models.Category>();
        }
    }
}
