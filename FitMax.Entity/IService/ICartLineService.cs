using FitMax.Entity.Entities;
using FitMax.Entity.Interfaces;
using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Entity.IService
{
    public interface ICartLineService
    {


        List<CartLineViewModel> AddToCart(List<CartLineViewModel> sepet, CartLineViewModel siparis);

        List<CartLineViewModel> DeleteInCart(List<CartLineViewModel> sepet, int id);
        Task<IEnumerable<CartLineViewModel>> GetCartLineByCartId(int id);
        Task UpdateCartLineStatus(int id);

        int TotalQuantity(List<CartLineViewModel> sepet);

        decimal TotalPrice(List<CartLineViewModel> sepet);
        void CreateCartLine(List<CartLineViewModel> model,int id);
    }
}
