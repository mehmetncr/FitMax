using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(int code)
        {
            
            return View();
        }
    }
}
