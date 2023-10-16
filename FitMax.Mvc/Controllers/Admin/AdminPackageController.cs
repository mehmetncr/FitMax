using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminPackageController : Controller
    {
        private readonly IPackageService _packageService;

        public AdminPackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _packageService.GetPackages());
        }
        [HttpGet]
        public IActionResult Details(int id)
        {

            return View(_packageService.GetPackagesById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Details(PackageViewModel model)
        {
            _packageService.UpdatePackage(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult NewPackage()
        {

            return View();
        }
        [HttpPost]
        public IActionResult NewPackage(PackageViewModel model)
        {            
            _packageService.AddPackage(model);
            return RedirectToAction("Index");
        }
        public IActionResult DeletePackage(int id)
        {
            _packageService.DeletePackage(id);
            return RedirectToAction("Index");
        }
    }
}
