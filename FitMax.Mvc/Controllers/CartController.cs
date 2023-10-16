using FitMax.Entity.Entities;
using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using FitMax.Mvc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using System.Security.Claims;

namespace FitMax.Mvc.Controllers

{
    public class CartController : Controller
    {
        private readonly IWalletService _walletService;
        private readonly IWalletDetailService _walletDetailService;
        private readonly IPackageService _packageService;
        private readonly IAccountService _accountService;
        private readonly IPrivateLessonService _lessonService;
        private readonly IProductService _productService;
        private readonly ICartLineService _cartLineService;
        private readonly ICartService _cartService;



        public CartController(IWalletService walletService, IWalletDetailService walletDetailService, IPackageService packageService, IAccountService accountService, IPrivateLessonService lessonService, ICartLineService cartLineService, IProductService productService, ICartService cartService)
        {
            _walletService = walletService;
            _walletDetailService = walletDetailService;
            _packageService = packageService;
            _accountService = accountService;
            _lessonService = lessonService;
            _cartLineService = cartLineService;
            _productService = productService;
            _cartService = cartService;

        }

        List<CartLineViewModel> cart;


        public IActionResult Index()
        {
            cart = TakeCart();
            return View(cart);
        }
        public List<CartLineViewModel> TakeCart()
        {
            var cart = HttpContext.Session.GetJson<List<CartLineViewModel>>("cart") ?? new List<CartLineViewModel>();
            return cart;
        }

        public int AddInCart(int Id)
        {
            ProductViewModel product = _productService.GetById(Id);
            cart = TakeCart();
            if (cart.FirstOrDefault(x => x.ProductId == product.Id) != null)
            {                


                if (cart.FirstOrDefault(x => x.ProductId == product.Id).Quantity < product.Stock)
                {
                    CartLineViewModel cartline = new CartLineViewModel();
                    cartline.ProductId = product.Id;
                    cartline.CartId = 0;
                    cartline.Quantity = 1;
                    cartline.UnitPrice = product.Price;
                    cartline.TotalPrice = (cart.FirstOrDefault(x => x.ProductId == product.Id).Quantity+1) * product.Price;
                    cartline.Name = product.Name;
                    cart = _cartLineService.AddToCart(cart, cartline);
                    SaveCart(cart);

                    return cart.Count();

                }
                else
                {
                    return 5000;
                }
            }
            else
            {
                CartLineViewModel cartline = new CartLineViewModel();
                cartline.ProductId = product.Id;
                cartline.CartId = 0;
                cartline.Quantity = 1;
                cartline.UnitPrice = product.Price;
                cartline.TotalPrice = 1 * product.Price;
                cartline.Name = product.Name;
                cart = _cartLineService.AddToCart(cart, cartline);
                SaveCart(cart);

                return cart.Count();
            }
            

        }
        public void SaveCart(List<CartLineViewModel> cart)
        {
            HttpContext.Session.SetJson("cart", cart);
        }
        public int DeleteFromCart(int id)
        {
            cart = TakeCart();
            cart = _cartLineService.DeleteInCart(cart, id);
            SaveCart(cart);
            return cart.Count();

        }
        public void DeleteCart()
        {
            //HttpContext.Session.Clear();    //Oturumda bulunan tüm session'ları siler.
            HttpContext.Session.Remove("cart"); //sadece ismi belirtilen session'ı siler.

        }
        // Sepet işlemleri

        //ürünü arttır
        public void IncreaseProduct(int id)
        {
            if (id != 0)
            {
                List<CartLineViewModel> cart = TakeCart();
                var ob = cart.Where(x => x.ProductId == id).FirstOrDefault();
                int stock = _productService.GetById(id).Stock;

                if (cart != null && stock - ob.Quantity > 0)
                {
                    ob.Quantity += 1;
                    ob.TotalPrice = ob.UnitPrice * ob.Quantity;
                }

                SaveCart(cart);

            }
        }
        //ürünü azalt
        public void DecreaseProduct(int id)
        {
            if (id != 0)
            {


                List<CartLineViewModel> cart = TakeCart();
                var ob = cart.Where(x => x.ProductId == id).FirstOrDefault();


                ob.Quantity -= 1;
                if (ob.Quantity == 0)
                {
                    DeleteFromCart(id);

                }
                else
                {
                    SaveCart(cart);
                }
            }

        }

        public IActionResult UpdatedCart()
        {
            var cart = HttpContext.Session.GetJson<List<CartLineViewModel>>("cart") ?? new List<CartLineViewModel>();
            return PartialView("_CartPartialView", cart);
        }








        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Payment(PrivateLessonBuyViewModel? Pmodel, int id)
        {
            PaymentViewModel model = new PaymentViewModel();

            //Salon üyeliği işlemleri (paket no larına göre buraya girebilir)
            //**************************************************************************************************
            if (id != 0) //paket satın alma işlemi için
            {

                var package = _packageService.GetPackagesById(id);
                model.Name = package.Name;
                model.Price = package.Price;
                model.PackageId = package.Id;
                model.Type = "package";
            }

            //Özel ders işlemleri için
            //***************************************************************************************************
            else if (Pmodel.TrainerId != 0)
            {
                var trainer = await _lessonService.GetLessonByTrainer(Pmodel.TrainerId, Pmodel.date);
                model.Price = trainer.TrainerPrice;
                model.Name = Pmodel.date.ToShortDateString() + " - Tarihinde Özel Ders Eğitmen : " + trainer.TrainerName;
                model.TrainerId = Pmodel.TrainerId;
                model.Type = "lesson";
                model.Day = Pmodel.date;
            }
            //Sepet işlemleri için
            //***************************************************************************************************
            else if (id == 0)
            {
                cart = TakeCart();
                model.Price = cart.Sum(x => x.TotalPrice);
                model.Name = "Alış Veriş";
                model.Type = "cart";
            }

            //Kullanıcının cüzdan bilgileri için
            //*****************************************************************************************************
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);   //oturum açmış olan kullanıcının Id si bulunur
            var wallet = await _walletService.GetUserWallet(Convert.ToInt32(userId)); //Id ye ait cüzdan bulunur
            model.Balance = wallet.Balance;
            model.WalletId = wallet.Id;
            model.UserId = Convert.ToInt32(userId);
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Payment(PaymentViewModel model)
        {
            //Bakiye kontrolü
            //**********************************************************************************
            if (model.Balance < model.Price)
            {
                ModelState.AddModelError("", "Lütfen Hesabınıza Para Yüklenmesi Yapın");
                return View(model);
            }
            //Paket işlemleri
            //*****************************************************************************
            if (model.Type == "package")
            {
                string message = await _accountService.UserAddPackage(model);
                if (message == "no")
                {

                    ModelState.AddModelError("", "Aktif Bir Üyeliğiniz Bulunmakta");
                    return RedirectToAction("StudentOrTrainer", "UserPage");
                }
            }
            //Özel ders işlemleri
            //**************************************************************************
            else if (model.Type == "lesson")
            {
                await _lessonService.UpdateLessonByTrainer(model);
                return RedirectToAction("StudentOrTrainer", "UserPage");
            }
            else if (model.Type == "cart")
            {
                cart = TakeCart();
                int cartId = await _cartService.CreateCart(model.UserId, cart.Sum(x => x.TotalPrice));
                _cartLineService.CreateCartLine(cart, cartId);
                await _productService.UpdateStock(cart);
                DeleteCart();
                return RedirectToAction("StudentOrTrainer", "UserPage");


            }


            //Cüzdan hareketi ekleme
            WalletDetailViewModel walletDetail = new WalletDetailViewModel()
            {
                ActivityDate = DateTime.Now,
                WalletId = model.WalletId,
                ActivityType = "Harcama",
                Amount = model.Price
            };
            await _walletDetailService.AddDetailToWallet(walletDetail);

            return RedirectToAction("StudentOrTrainer", "UserPage");
        }
    }
}
