using FitMax.Entity.IService;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.ViewComponents.HomeTrainers
{
    public class HomeTrainers : ViewComponent
    {
        private readonly ITrainerService _trainerService;

        public HomeTrainers(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View( await _trainerService.GetRndmTrainer());
        }
    }
}
