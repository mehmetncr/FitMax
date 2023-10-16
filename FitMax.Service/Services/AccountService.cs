using AutoMapper;
using FitMax.DataAccess.Identity;
using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FitMax.Service.Services
{
	public class AccountService : IAccountService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly SignInManager<AppUser> _signInManager;
		private readonly IMapper _mapper;

		public AccountService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IMapper mapper)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
			_mapper = mapper;
		}

		public async Task<string> CreateUserAsync(RegisterViewModel model)
		{
			string message = string.Empty;
			var user = new AppUser() //yeni bir kullanıcı gelen model verileri ile oluşturulur
			{
				FirstName = model.FirstName,
				Lastname = model.LastName,
				Email = model.Email,
				PhoneNumber = model.PhoneNumber,
				UserName = model.UserName,
				UserType = "Üye",     //önce üye olur trainer yetkisi admin tarafından verilecek
				DataConfirm = true,  //modelden true gelmesi gerekiyor
				Status = true,   //onaylı müşteri olur
				CreateDate = DateTime.Now,

			};
			var identityresult = await _userManager.CreateAsync(user, model.Password);
			if (identityresult.Succeeded)    //başarılı ise
			{

				message = "Ok";

			}
			else
			{
				foreach (var error in identityresult.Errors)  //değilse
				{
					message = error.Description;
				}
			}
			return message;
		}

		public async Task<int> GetLastUserId(string name)
		{
			var user = await _userManager.FindByNameAsync(name);

			return user.Id;
		}

		public async Task<string> LoginUserAsync(LoginViewModel model)
		{
			string message = string.Empty;
			var user = await _userManager.FindByNameAsync(model.UserName);  //username ile kullanıcı aranır
			if (user == null)  //kullanıcı bulunamazsa
			{
				message = "user not fount";
				return message;
			}
			var identityresult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);   //kullanıcı bulunursa şifre kontrolu yapılır, beni hatırla demişse, kilitlenme açık
			if (identityresult.Succeeded)  //giriş başarılı ise
			{
				message = "ok";
			}
			else if (identityresult.IsLockedOut)  //girişler kilitlenmiş ise
			{
				message = "lock";
			}
			else
			{
				message = "wrong user";
			}
			return message;

		}

		public async Task LogOutUserAsync()
		{
			await _signInManager.SignOutAsync();  //hesaptan çıkış
		}
		public async Task<string> UserAddPackage(PaymentViewModel package) //kullanıcıya üyelik tanımlama
		{
			string message = string.Empty;
			var user = await _userManager.FindByIdAsync(package.UserId.ToString());
			if (user.Package == null)
			{
				user.Package = package.PackageId.ToString();
				user.PackageStartDate = DateTime.Now;
				user.PackageEndDate = DateTime.Now.AddMonths(Convert.ToInt32(package.Name.Split(" ")[0]));
				await _userManager.UpdateAsync(user);
				message = "ok";
				return message;
			}
			else
			{
				message = "no";
				return message;
			}

		}
		public List<UserViewModel> GetTrainers()
		{
			var trainers = _userManager.Users.ToList().Where(x => x.UserType == "Eğitmen");
			return _mapper.Map<List<UserViewModel>>(trainers);
		}
		public UserViewModel GetTrainerByd(int id)
		{
			AppUser trainers = new AppUser();

			trainers = _userManager.Users.ToList().FirstOrDefault(x => x.Id == id);

			return _mapper.Map<UserViewModel>(trainers);
		}
		public List<UserViewModel> GetUsers()
		{
			var users = _userManager.Users.ToList().Where(x => x.UserType == "Üye");
			return _mapper.Map<List<UserViewModel>>(users);
		}
		public async Task DeleteUser(int id)
		{
			var user = await _userManager.FindByIdAsync(id.ToString());
			if (user.Status == false)
			{
				user.Status = true;
			}
			else
			{
				user.Status = false;
			}

			await _userManager.UpdateAsync(user);

		}
		public async Task RemoveSub(int id)
		{
			var users = await _userManager.FindByIdAsync(id.ToString());
			users.Package = null;
			users.PackageEndDate = null;
			users.PackageStartDate = null;
			await _userManager.UpdateAsync(users);

		}
		public async Task AddTrainer(int id)
		{
			var users = await _userManager.FindByIdAsync(id.ToString());
			users.UserType = "Eğitmen";
			await _userManager.UpdateAsync(users);

		}
		public async Task RemoveTrainer(int id)
		{
			var users = await _userManager.FindByIdAsync(id.ToString());
			users.UserType = "Üye";
			users.TrainerPrice = 0;
			await _userManager.UpdateAsync(users);

		}

		public async Task UpdatePrice(int id, string price)
		{
			var users = await _userManager.FindByIdAsync(id.ToString());
			users.TrainerPrice = Convert.ToDecimal(price);
			await _userManager.UpdateAsync(users);

		}
		public async Task<string> GetFullNameById(int id)
		{
			var users = await _userManager.FindByIdAsync(id.ToString());
			return users.FirstName + " " + users.Lastname;

		}
		public async Task<PasswordRessetViewModel> FindUser(PasswordRessetViewModel model) //modelden username ve email gelir
		{
			PasswordRessetViewModel resModel = new PasswordRessetViewModel();
			if (model.EMail != null && model.UserName != null)
			{
				var user = await _userManager.FindByEmailAsync(model.EMail);
				if (user != null && user.UserName == model.UserName)  //eğer email ve şifre doğru ise
				{

					resModel.Token = await _userManager.GeneratePasswordResetTokenAsync(user);  //doğrulamak için bir token oluşturulur
					resModel.Id = user.Id;


					return resModel;
				}
			}
			return resModel;
		}

		public async Task UpdatePassword(PasswordRessetViewModel model)
		{
			var user = await _userManager.FindByIdAsync(model.Id.ToString());  //kullanıcı bulunur
            if (user !=null)
            {
				await _userManager.ResetPasswordAsync(user, model.Token, model.Password);  //gelen bilgilerle şifre güncellenir
			}
           

		}

        public UserViewModel GetUserById(int id)
        {
            var users = _userManager.Users.FirstOrDefault(x => x.Id==id);
            return _mapper.Map<UserViewModel>(users);
        }

        public IEnumerable<RoleViewModel> GettAllRole()
        {
			return  _mapper.Map<IEnumerable<RoleViewModel>>(_roleManager.Roles.ToList());
        }

        public async Task<UsersInOrOuytRoleViewModel> EditRoleById(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());  //hangi rol olduğu bulundu

            var UserInRole = new List<AppUser>();
            var UserOutRole = new List<AppUser>();

            var users = _userManager.Users.ToList();



            foreach (var item in users)
            {
                if (await _userManager.IsInRoleAsync(item, role.Name))  //rolde mi değil mi
                {

                    UserInRole.Add(item); //rolde olanlar
                }
                else
                {
                    UserOutRole.Add(item); //rolde olmayanlar
                }

            }


            UsersInOrOuytRoleViewModel model = new UsersInOrOuytRoleViewModel()  //modele ekledi hepsini
            {
                Role =_mapper.Map<RoleViewModel>(role),
                UsersInRole =_mapper.Map<List<UserViewModel>>(UserInRole),
                UsersOutRole = _mapper.Map<List<UserViewModel>>(UserOutRole)
            };
            return model;

        }

        public async Task<string> EditRole(EditRoleViewModel model)
        {
			string msg = string.Empty;
            foreach (var userId in model.UserOutsToAdd ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, model.roleName);
                    if (!result.Succeeded)
                    {
						msg = "error";
                        return msg;

                    }
                }
            }
            foreach (var userId in model.UserIdsToDelete ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, model.roleName);
                    if (!result.Succeeded)
                    {
                        msg = "error";
                        return msg;
                    }
                }
            }
			return "Ok";
        }
        public async Task<UserViewModel> GetUserByIdAsync(int id)
        {
            var users = await _userManager.FindByIdAsync(id.ToString());
            return _mapper.Map<UserViewModel>(users);

        }
        public async Task UpdateInfo(UserViewModel model)
        {
            AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());
            user.Height = model.Height;
            user.Weight = model.Weight;
            user.Instagram = model.Instagram;
            user.Youtube = model.Youtube;
            user.Facebook = model.Facebook;
            user.Twitter = model.Twitter;
            user.ImgUrl = model.ImgUrl;
            user.Description = model.Description;
            await _userManager.UpdateAsync(user);

        }

        public async Task<UserViewModel> GetUserByNameAsync(string name)
        {
            var user=await _userManager.FindByNameAsync(name);

			return _mapper.Map<UserViewModel>(user);
        }
		public async Task<string> ResetPasswordNormal(PasswordRessetViewModel model)
		{
			AppUser user = await _userManager.FindByNameAsync(model.UserName);
			if (user!=null)
			{
				var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
				if (result.Succeeded)
				{
					return  "Ok";
				}
				else
				{
					return  "No";
				}
			}
			return "No";
		}
    }
}
