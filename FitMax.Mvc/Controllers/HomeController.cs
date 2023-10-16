using FitMax.Entity.IService;
using FitMax.Mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FitMax.Mvc.Controllers
{
    public class HomeController : Controller
    {

        private readonly ITrainerService _trainerService;

        public HomeController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PrivateTrainer()
        {
            return View();
        }
		public IActionResult FreeExercise()
		{
			return View();
		}
		public IActionResult CrossFit()
		{
			return View();
		}
		public IActionResult BoxingTraining()
		{
			return View();
		}
        public async Task<IActionResult> Trainers()
        {
           
            return View(await _trainerService.GetAllTrainer());
        }


    }
}