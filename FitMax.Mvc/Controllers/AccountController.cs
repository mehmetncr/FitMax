using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.Controllers
{
    public class AccountController : Controller
    {
		private readonly	IAccountService _accountService;
		private readonly	IWalletService _walletService;

		public AccountController(IAccountService accountService, IWalletService walletService)
        {
            _accountService = accountService;
            _walletService = walletService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
			LoginViewModel model = new LoginViewModel();
			if (returnUrl!=null)  //yönlendirilerek login sayfasına düşmüşse
			{				
				model.ReturnUrl = returnUrl;
				
			}
            return View(model); //yönlendirme yoksa model boş olacak ve loginden sonra ansayfaya gidecek
        }
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			string message = await _accountService.LoginUserAsync(model);
			if (message=="ok")   //giriş başarılı ise
			{
                return Redirect(model.ReturnUrl ?? "~/");  //yönlenme varsa oraya yoksa anasayfaya gider
            }
			else if (message == "user not fount" || message == "wrong user") //yanlış girişlerde
			{
				ModelState.AddModelError("", "Kullanıcı Adı Veya Şifre Yanlış");
			}
			else 
			{
				ModelState.AddModelError("", "Giriş Engellendi"); //girişler kilitlendiyse
			}

			return View(model);
		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			string message = await _accountService.CreateUserAsync(model);

			if (message == "Ok")  //kayıt başarılıysa
			{
				WalletViewModel wallet = new WalletViewModel()  //kullanıcıya ait bir cüzdan oluşturulur
				{
					Balance = 0,
					CreateDate = DateTime.Now,
					UserId = await _accountService.GetLastUserId(model.UserName)  //kayıt olan kullanıcının kullanıcı adı ile atanan id alınır
				};
				 await _walletService.GetCreateWallet(wallet);  //cuzdan kayıt edilir

				return RedirectToAction("Login");
			}
			else
			{
				ModelState.AddModelError("", message);   //değilse
			}
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> LogOut()
		{
			await _accountService.LogOutUserAsync();  //hesaptan çıkış
            return RedirectToAction("Index","Home");
        }
		[HttpGet]
		public async Task<IActionResult> PasswordReset()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> PasswordReset(PasswordRessetViewModel model)
		{
			PasswordRessetViewModel user = await _accountService.FindUser(model);
			if (user.Token!=null)
			{
				TempData["token"]=user.Token;
				return RedirectToAction("PasswordNew", "Account", new {id=user.Id});
			}
			else
			{
				ModelState.AddModelError("", "Kullanıcı Bulunamadı");
				return View(model);
			}
			
		}
		[HttpGet]
		public async Task<IActionResult> PasswordNew(int id)
		{
			PasswordRessetViewModel model = new PasswordRessetViewModel()
			{
				Id = id,
				Token = TempData["token"].ToString()

			};
			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Password(PasswordRessetViewModel model)
		{

			await _accountService.UpdatePassword(model);
			return RedirectToAction("Login");
		}
		[Authorize]
        [HttpGet]
        public IActionResult PasswordNewNormal()
        {
            
            return View();
        }
        [Authorize]
        [HttpPost]
		public async Task<IActionResult> PasswordNewNormal(PasswordRessetViewModel model)
		{
			model.UserName = User.Identity.Name;
			string msg= await	_accountService.ResetPasswordNormal(model);
			if (msg=="Ok")
			{
				return RedirectToAction("StudentOrTrainer", "UserPage");
			}
			else
			{
				ModelState.AddModelError("", "Şifre Değiştirilemedi");
				return View(model);
			}
			return View();
		}
	}
}
