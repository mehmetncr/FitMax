using FitMax.Entity.IService;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.ViewComponents.AdminEmails
{
    public class AdminEmailsViewComponent : ViewComponent
    {
        private readonly IContactService _contactService;

        public AdminEmailsViewComponent(IContactService contactService)
        {
            _contactService = contactService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        { 
            return View(await _contactService.GetAllMail());
        }
    }
}
