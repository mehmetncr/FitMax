using FitMax.Entity.IService;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.ViewComponents.WalletDetails
{
    public class WalletDetailsViewComponent : ViewComponent
    {
        private readonly IWalletDetailService _walletDetailService;

        public WalletDetailsViewComponent(IWalletDetailService walletDetailService)
        {
            _walletDetailService = walletDetailService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var details = await _walletDetailService.GetDetailsToWallet(Convert.ToInt32(id));
            details = details.OrderByDescending(x=>x.Id);
           
            return View(details.Take(10));
        }
    }
}
