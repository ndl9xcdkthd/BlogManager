using BlogManager.Infrastructure.Identity.Models;
using BlogManager.Web.Areas.Admin.Models;
using AutoMapper;

namespace BlogManager.Web.Areas.Admin.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}