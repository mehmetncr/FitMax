using FitMax.Entity.Entities;
using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.IService
{
    public interface IWalletService
    {
        Task<WalletViewModel> GetUserWallet(int id);
        Task GetCreateWallet(WalletViewModel model);
        Task UpdateBalancerWallet(int walletId, decimal amount, string ActiviyType);


	}
}
