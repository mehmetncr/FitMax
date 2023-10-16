using FitMax.Entity.IService;
using FitMax.Entity.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using System.Security.Claims;

namespace FitMax.Mvc.Controllers.Admin
{
    public class AdminProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly ICartLineService _cartLineService;

        public AdminProductController(IProductService productService, ICartService cartService, ICartLineService cartLineService)
        {
            _productService = productService;
            _cartService = cartService;
            _cartLineService = cartLineService;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {

            return View(await _productService.GetAll());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {

            return View(_productService.GetById(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Details(ProductViewModel model, IFormFile formFile)
        {
            if (formFile != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                formFile.CopyTo(stream);
                model.ImgUrl = "/images/" + formFile.FileName;
            }
            _productService.UpdateProduct(model);

            return RedirectToAction("Index");

        }
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model, IFormFile formFile)
        {


            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", formFile.FileName);
            var stream = new FileStream(path, FileMode.Create);
            formFile.CopyTo(stream);
            model.ImgUrl = "/images/" + formFile.FileName;



            await _productService.AddProduct(model);
            return RedirectToAction("Index");

        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            await _productService.DeleteProduct(id);
            var product = await _productService.GetAll();
            return PartialView("_AdminProductPartialView", product);
        }
        [Authorize(Roles = "Admin,Personel")]
        public async Task<IActionResult> Delivery()
        {
           
            return View(await _cartService.GetAllCartAsync());
        }
        [Authorize(Roles = "Admin,Personel")]
        public async Task<IActionResult> DeliveryDetail(int id)
        {
          
            return View(await _cartLineService.GetCartLineByCartId(id));
        }
        [Authorize(Roles = "Admin,Personel")]
        public async Task<IActionResult> DeliveryUpdate(int id,int cartId)
        {
             await _cartLineService.UpdateCartLineStatus(id);

            return PartialView("_DeliveryPartialView", await _cartLineService.GetCartLineByCartId(cartId));
        }
       
    }
}
