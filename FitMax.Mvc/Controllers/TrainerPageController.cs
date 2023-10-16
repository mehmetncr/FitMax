using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

namespace FitMax.Mvc.Controllers
{
    public class TrainerPageController : Controller
    {
        private readonly IPrivateLessonService _lessonService;
        private readonly IAccountService _accountService;

		public TrainerPageController(IPrivateLessonService lessonService, IAccountService accountService)
		{
			_lessonService = lessonService;
			_accountService = accountService;
		}
		[HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.trainer = _accountService.GetTrainerByd(id);          
            return View(await _lessonService.GetAllLessonsByTrainer(id));  //eğitmen sayfasına eğitmenin takvimi gönderilir
        }
        [HttpGet]
        public IActionResult BuyLesson(int id,string date)
        {
            PrivateLessonBuyViewModel model = new PrivateLessonBuyViewModel()
            {
                date = Convert.ToDateTime(date),
                TrainerId = id
            };
            return  RedirectToAction("Payment", "Cart", model);  //ödeme ekranına seçilen gün ve eğitmen bilgisi gönderilir

           

        }
    }
}

