
using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace FitMax.Mvc.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class TrainerController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IPrivateLessonService _privateLesson;

        public TrainerController(IAccountService accountService, IPrivateLessonService privateLesson)
        {
            _accountService = accountService;
            _privateLesson = privateLesson;
        }
        [HttpGet]
        public IActionResult Index()  //User tablosunda UserType değeri "Eğitmen" olan kullanıcılar gelir
        {
            return View(_accountService.GetTrainers());
        }
   
        public async Task<IActionResult> Lessons(int id)  //2 haftalık ders programını görüntülemek için
        {
           

            return View(await _privateLesson.GetAllLessonsByTrainer(id));
        }
  
        public async Task<IActionResult> LessonUpdate(int id,string date)  //2 haftalık ders programını güncellemek için iptaller kapamalar
        {
            
          await  _privateLesson.AdminUpdateLessonByTrainer(id,Convert.ToDateTime(date));
            return RedirectToAction("Lessons", new
            {
                id = id,
               
            });

        }
        public async Task<IActionResult> LessonDetails(int id)    //eğitmenin tüm verdiği dersler için tüm veriler
        {

            return View(await _privateLesson.GetAllLessonsByTrainerWithUsers(id));
        }
        public async Task<IActionResult> RemoveTrainer(int id) //Usertype "Üye" Olarak Değiştirilir
        {
           await _accountService.RemoveTrainer(id);
            return RedirectToAction("Index");
        }
        public IActionResult UpdatePrice(int id)  //Eğitmenin ders ücreti güncellemek için inputu olan bir sayfa getirir
        {
            return View(id);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePrice(int id,string price)  //Eğitmenin yeni ders ücretini işler
        {
            await _accountService.UpdatePrice(id, price);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> TotalBalance(int id)  //eğitmenin yıl-ay bazında her ay ne kadar kazandığı bilgisi gelir
        {                     

            return View(await _privateLesson.TotalBalance(id));
        }
        public async Task<IActionResult> UpdateCalendar() //User tablosunda her eğitmen İçin aktif tabloyu 14 güne tamamlar ve eklediği günleri "empty" olarak işaretler
        {
            await _privateLesson.NewWeek();
            TempData["info"] = "Takvim Güncellendi";
            return RedirectToAction("Index");
        }
    }
    

}