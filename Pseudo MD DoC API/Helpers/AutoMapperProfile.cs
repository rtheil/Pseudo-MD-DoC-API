using AutoMapper;
using Pseudo_MD_DoC_API.Applications;
using Pseudo_MD_DoC_API.Models.Applications;
using Pseudo_MD_DoC_API.Models.Users;
using Pseudo_MD_DoC_API.Users;

namespace Pseudo_MD_DoC_API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Users
            CreateMap<User, UserOutputModel>();
            CreateMap<UserRegisterModel, User>();
            CreateMap<UserUpdateModel, User>();
            CreateMap<UserForgotModel, User>();

            //Applications
            CreateMap<Application, ApplicationOutputModel>();
            CreateMap<ApplicationSaveModel, Application>();
        }
    }
}
