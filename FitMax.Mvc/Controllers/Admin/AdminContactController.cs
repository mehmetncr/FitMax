using FitMax.Entity.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.Controllers.Admin
{
    [Authorize(Roles ="Admin")]
    public class AdminContactController : Controller
    {
        private readonly IContactService _contactService;

        public AdminContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IActionResult> Index(int id)
        {
            return View(await _contactService.GEtMailById(id));
        }

    }
}
