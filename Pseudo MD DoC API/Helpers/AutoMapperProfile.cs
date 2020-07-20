using AutoMapper;
using Pseudo_MD_DoC_API.Models.Users;
using Pseudo_MD_DoC_API.Users;

namespace Pseudo_MD_DoC_API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<UpdateModel, User>();
        }
    }
}
