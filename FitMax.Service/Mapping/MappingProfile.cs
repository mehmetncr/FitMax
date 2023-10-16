using AutoMapper;
using FitMax.DataAccess.Identity;
using FitMax.Entity.Entities;
using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Service.DTO
{
    public  class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Cart,CartViewModel>().ReverseMap();
            CreateMap<CartLine,CartLineViewModel>().ReverseMap();
            CreateMap<Package,PackageViewModel>().ReverseMap();
            CreateMap<Product,ProductViewModel>().ReverseMap();
            CreateMap<Wallet,WalletViewModel>().ReverseMap();
            CreateMap<WalletDetail,WalletDetailViewModel>().ReverseMap();
            CreateMap<PrivateLesson,PrivateLessonViewModel>().ReverseMap();
            CreateMap<AppUser,UserViewModel>().ReverseMap();
            CreateMap<AppUser,HomeTrainerViewModel>().ReverseMap();
            CreateMap<AppRole,RoleViewModel>().ReverseMap();
            CreateMap<Contact, ContactViewModel>().ReverseMap();

        }
    }
}
