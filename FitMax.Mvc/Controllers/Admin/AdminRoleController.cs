using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace FitMax.Mvc.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminRoleController : Controller
    {
        private readonly IAccountService _accountService;

        public AdminRoleController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {

            return View(_accountService.GettAllRole());
        }
        public async Task<IActionResult> UpdateRole(int id)
        {

            return View(await _accountService.EditRoleById(id));
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(EditRoleViewModel model)
        {

            string msg = await _accountService.EditRole(model);
            if (msg!="Ok")
            {
                ModelState.AddModelError("", "İşlem Başarısız");
                return View(model);
            }
            return RedirectToAction("UpdateRole", new { id = model.roleId });
        }

    }
}
