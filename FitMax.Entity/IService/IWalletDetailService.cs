using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.IService
{
    public interface IWalletDetailService
    {
		Task AddDetailToWallet(WalletDetailViewModel model);
        Task<IEnumerable<WalletDetailViewModel>> GetDetailsToWallet(int id);
    }
}
