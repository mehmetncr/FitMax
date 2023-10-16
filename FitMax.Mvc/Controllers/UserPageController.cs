using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using FitMax.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.Controllers
{
    [Authorize]
    public class UserPageController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICartService _cartService;
        private readonly IPrivateLessonService _privateLessonService;
        private readonly ICartLineService _cartLineService;
        private readonly IPackageService _packageService;   

        public UserPageController(IAccountService accountService, ICartService cartService, IPrivateLessonService privateLessonService, ICartLineService cartLineService, IPackageService packageService)
        {
            _accountService = accountService;
            _cartService = cartService;
            _privateLessonService = privateLessonService;
            _cartLineService = cartLineService;
            _packageService = packageService;
        }

        public async Task<IActionResult> StudentOrTrainer()
        {
            string userName = User.Identity.Name;
            UserViewModel user = await _accountService.GetUserByNameAsync(userName);

            if (user != null)
            {
                if (user.UserType == "Eğitmen")
                {
                    return RedirectToAction("TrainerIndex", new { userName = userName });
                }
                else
                {
                    return RedirectToAction("Index", new { userName = userName });
                }
            }

            return NotFound();
        }


        public async Task<IActionResult> Index(string userName)
        {
            var user= await _accountService.GetUserByNameAsync(userName);
            DeliveryListViewModel delivery=await _cartService.GetCartById(user.Id);
            ViewBag.deliveryFalse = delivery.IsFalse;
            ViewBag.deliveryTrue= delivery.IsTrue;

            UserLessonListViewModel lessons = await _privateLessonService.GetUserLessons(user.Id);
            ViewBag.lessonFalse = lessons.IsFalse;
            ViewBag.lessonTrue = lessons.IsTrue;

            //Paket Adını getirme
            PackageViewModel packageViewModel = await _packageService.GetByPackage(Convert.ToInt32(user.Package));
            ViewBag.package = packageViewModel.Name;

            return View(await _accountService.GetUserByIdAsync(user.Id));
        }
        public async Task<IActionResult> TrainerIndex(string userName)
        {
            var trainer = await _accountService.GetUserByNameAsync(userName);
            UserLessonListViewModel lessons = await _privateLessonService.GetTrainerLessons(trainer.Id);
            ViewBag.lessonFalse = lessons.IsFalse;
            ViewBag.lessonTrue = lessons.IsTrue;
            DeliveryListViewModel delivery = await _cartService.GetCartById(trainer.Id);
            ViewBag.deliveryFalse = delivery.IsFalse;
            ViewBag.deliveryTrue = delivery.IsTrue;

            return View(await _accountService.GetUserByIdAsync(trainer.Id));

        }
        [HttpPost]
        public async Task<IActionResult> UpdateUserInfo(UserViewModel model, IFormFile formFile)
        {
            if (formFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                formFile.CopyTo(stream);
                model.ImgUrl = "/images/" + formFile.FileName;
            }
             await _accountService.UpdateInfo(model);


            await _accountService.UpdateInfo(model);
            {
                if (model.UserType != "Üye")
                {
                    return RedirectToAction("TrainerIndex", new { userName = model.UserName });
                }
                else
                {
                    return RedirectToAction("Index", new { userName = model.UserName });
                }
            }

        }   
        public async Task<IActionResult> UserCartLines(int id)
        {
            var model = await _cartLineService.GetCartLineByCartId(id);
            return View(model);

        }
        public async Task<IActionResult> LessonDescription(int id)
        {

            return View(await _privateLessonService.GetLessonDetails(id));
        }
        [HttpPost]
        public async Task<IActionResult> LessonDescription(PrivateLessonViewModel model)
        {
            _privateLessonService.UpdateLessonDetails(model);


            return View(model);
        }

    }
}
