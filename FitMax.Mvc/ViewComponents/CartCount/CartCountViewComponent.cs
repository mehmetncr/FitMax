using FitMax.Entity.ViewModels;
using FitMax.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FitMax.Mvc.ViewComponents.CartCount
{
    public class CartCountViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
           var  cart = HttpContext.Session.GetJson<List<CartLineViewModel>>("cart") ?? new List<CartLineViewModel>();
            return View(cart.Count());
        }
    }
}
