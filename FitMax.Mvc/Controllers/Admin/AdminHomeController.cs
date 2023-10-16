using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IPrivateLessonService _privateLessonService;
        private readonly IProductService _productService;

        public AdminHomeController(IAccountService accountService, IPrivateLessonService privateLessonService, IProductService productService)
        {
            _accountService = accountService;
            _privateLessonService = privateLessonService;
            _productService = productService;
        }


        public async Task<IActionResult> Index()
        {

            //Aktif Üyelerin sayısı Gelir.
            List<UserViewModel> activeUsers = _accountService.GetUsers();
            ViewBag.countActiveUsers = activeUsers.Where(x => x.Status == true).Count();

            //Son 14 günlük aktif Derslerin Sayısı

            IEnumerable<PrivateLessonViewModel> privatelessons = await _privateLessonService.GetAllPrivateLesson();

            ViewBag.countPriLes = privatelessons.Where(x => x.Date > DateTime.Now).Count();

            //Toplam Ürün Stokları

            IEnumerable<ProductViewModel> product = await _productService.GetAllStocks();
            var productStock = product.Select(p => p.Stock).ToList();

            int sayı = productStock.Count();
            int toplam = 0;

            for (int i = 0; i < sayı; i++)
            {
                toplam = toplam + productStock[i];
            }

            ViewBag.Toplam = toplam;
            return View();
        }
     
    }
}
