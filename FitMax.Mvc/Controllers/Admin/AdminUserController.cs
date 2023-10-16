using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminUserController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IWalletService _walletService;
        private readonly IWalletDetailService _walletDetailService;



        public AdminUserController(IAccountService accountService, IWalletDetailService walletDetailService, IWalletService walletService)
        {
            _accountService = accountService;
            _walletDetailService = walletDetailService;
            _walletService = walletService;
        }

        public IActionResult Index()
        {
            return View(_accountService.GetUsers());
        }
        public async Task<IActionResult> Wallet(int id)
        {
            WalletViewModel wallet= await _walletService.GetUserWallet(id);
            ViewBag.balance = wallet;             
            return View(await _walletDetailService.GetDetailsToWallet(wallet.Id));
        }
        public async Task<IActionResult> DeleteUser(int id)
        {
           await _accountService.DeleteUser(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RemoveSub(int id)
        {
           await _accountService.RemoveSub(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddTrainer(int id)
        {
            await _accountService.AddTrainer(id);
            return RedirectToAction("Index");
        }
    }
}
