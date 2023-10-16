using FitMax.Entity.Entities;
using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.IService
{
    public interface IAccountService
    {
        Task<string> CreateUserAsync(RegisterViewModel model);
        Task<string> LoginUserAsync(LoginViewModel model);
        Task LogOutUserAsync();
        Task<string> UserAddPackage(PaymentViewModel package);
        Task<int> GetLastUserId(string name);
        UserViewModel GetUserById(int id);
        List<UserViewModel> GetTrainers();
        UserViewModel GetTrainerByd(int id);
		Task<PasswordRessetViewModel> FindUser(PasswordRessetViewModel model);
		Task UpdatePassword(PasswordRessetViewModel model);
        Task<string> GetFullNameById(int id);
        List<UserViewModel> GetUsers();
        Task DeleteUser(int id);
        Task RemoveSub(int id);
        Task AddTrainer(int id);
        Task RemoveTrainer(int id);
        Task UpdatePrice(int id,string price);
        IEnumerable<RoleViewModel> GettAllRole();
        Task<UsersInOrOuytRoleViewModel> EditRoleById(int id);
        Task<string> EditRole(EditRoleViewModel model);
        Task UpdateInfo(UserViewModel model);
        Task <UserViewModel> GetUserByIdAsync(int id);
        Task <UserViewModel > GetUserByNameAsync(string name);
        Task<string> ResetPasswordNormal(PasswordRessetViewModel model);


	}
}
