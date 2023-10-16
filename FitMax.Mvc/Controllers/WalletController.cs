using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FitMax.Mvc.Controllers
{
    [Authorize]
    public class WalletController : Controller
	{
		private readonly IWalletService _walletService;
		private readonly IWalletDetailService _walletDetailService;

		public WalletController(IWalletService walletService, IWalletDetailService walletDetailService)
		{
			_walletService = walletService;
			_walletDetailService = walletDetailService;
		}

		public async Task<IActionResult> Index()
		{
			string userId =  User.FindFirstValue(ClaimTypes.NameIdentifier);   //oturum açmış olan kullanıcının Id si bulunur
			
			return View(await _walletService.GetUserWallet(Convert.ToInt32(userId)));  //Id ye ait cüzdan bulunur
		}
		[HttpGet]
		public IActionResult AddMoney(int id) //para yükle butonundan buraya cüzdan id si gönderilir
		{
			BankCartViewModel model = new BankCartViewModel()  
			{
				Id=id,  //cüzdan Id modelle birlikte view'e gönderilir
			};

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> AddMoney(BankCartViewModel model)
		{
			WalletDetailViewModel walletDetail = new WalletDetailViewModel()  //yeni bir cüzdan hareketi oluşturulur ve içeriği viewden gelen bilgilerle doldurulur
			{
				ActivityDate = DateTime.Now,
				Amount = model.Amount,
				ActivityType = "Yatırma", // iki işlem olabilir "Yatırma" ve "Harcama"
				WalletId = model.Id
			};
			await _walletDetailService.AddDetailToWallet(walletDetail);  //detay kaydedilir
			

			return RedirectToAction("Index", "Wallet");
		}
	}
}
