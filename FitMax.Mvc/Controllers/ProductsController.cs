using FitMax.Entity.IService;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.Controllers
{
	public class ProductsController : Controller
	{
		private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public  async Task<IActionResult> Index(string search)
		{
            if (search!=null)
            {
                var models = await _productService.Search(search);
                return PartialView("_ProductsPartialView", models);
            }          
                var model = await _productService.GetAllTrue();
            
			return View(model);
		}
        public async Task<IActionResult> Filter(string filter)
        {
            var model = await _productService.GetAllByCategory(filter);


            return PartialView("_ProductsPartialView", model);
        }
     


    }
}
