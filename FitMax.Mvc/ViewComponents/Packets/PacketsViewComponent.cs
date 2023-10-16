using AutoMapper;
using FitMax.Entity.IService;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.ViewComponents.Packets
{
    public class PacketsViewComponent : ViewComponent
    {
        private readonly IPackageService _packageService;
   
        public PacketsViewComponent(IPackageService packageService)
        {
            _packageService = packageService;
            
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            return View( await _packageService.GetPackages());
        }
    }
}
