using FitMax.DataAccess.Contexts;
using FitMax.DataAccess.Identity;
using FitMax.DataAccess.Repositories;
using FitMax.DataAccess.UnitOfWorks;
using FitMax.Entity.Entities;
using FitMax.Entity.Interfaces;
using FitMax.Entity.IService;
using FitMax.Entity.UnitOfWorks;
using FitMax.Service.DTO;
using FitMax.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Service.Extensions
{
	public static class StartExtension
	{
		public static void AddExtension(this IServiceCollection services)
		{
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<ICartService, CartService>();
			services.AddScoped<ICartLineService, CartLineService>();
			services.AddScoped<IPackageService, PackageService>();
			services.AddScoped<IProductService, ProductService>();
			services.AddScoped<IWalletService, WalletService>();
			services.AddScoped<IWalletDetailService, WalletDetailService>();
			services.AddScoped<IPrivateLessonService, PrivateLessonService>();
			services.AddScoped<IAccountService, AccountService>();
			services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<IContactService, ContactService>();



            services.AddAutoMapper(typeof(MappingProfile));


			services.AddIdentity<AppUser, AppRole>(options =>
			{
				options.Password.RequireNonAlphanumeric = false;  //karakter istemesin
				options.Password.RequiredLength = 3;  //uzunluğu en az 3 karakter olsun
				options.Password.RequireUppercase = false; //büyük harf istemesin
				options.Password.RequireLowercase = false;  //Küçük harf istemesin
				options.Password.RequireDigit = false; //sayı istemesin
													   //options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvyzqw0123456789";  sadece bunlar kabul edilsin
				options.User.RequireUniqueEmail = false; //e mail eşsisiz olmalı
				options.Lockout.MaxFailedAccessAttempts = 3;  //3 yanlış denemeden sonra girişi altaki süre kadar durdur
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);  // üstteki sayı kadaryanlış girişten sonra 1 dk girişi engeller

			}).AddEntityFrameworkStores<FitMaxContext>()
				.AddDefaultTokenProviders();

			services.ConfigureApplicationCookie(op =>
			{
				op.LoginPath = new PathString("/Account/Login");   //giriş için sayfaya yönlendir				
				op.ExpireTimeSpan = TimeSpan.FromMinutes(10); //cookie ömrü dk
															  //op.AccessDeniedPath = new PathString("yetisi yok sayfası"); // yetkisi olmayinca yönlendirme
				op.SlidingExpiration = true; //üsstteki 10 dk dolmadan tekar login olursa tekrar süreyi başa alır
				op.Cookie = new CookieBuilder()
				{
					Name = "IdentityAppCookie", //cookie adı
					HttpOnly = true,  //sadece tarayıcıdan girilsin programlar yakalayamayacak

				};

			});
    

        }
	}
}
