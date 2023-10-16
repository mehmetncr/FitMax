using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.Controllers
{
	public class ContactController : Controller
	{
		private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
        public async Task<IActionResult> Index(ContactViewModel model)
        {
          await  _contactService.SendMail(model);
			return RedirectToAction("Index", "Home");
        }
    }
}
