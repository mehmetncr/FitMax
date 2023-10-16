using AutoMapper;
using FitMax.Entity.Entities;
using FitMax.Entity.IService;
using FitMax.Entity.UnitOfWorks;
using FitMax.Entity.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMax.Service.Services
{
    public class CartLineService : ICartLineService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        public CartLineService(IUnitOfWork unitOfWork, IMapper mapper, ICartService cartService, IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cartService = cartService;
            _productService = productService;
        }

        public List<CartLineViewModel> AddToCart(List<CartLineViewModel> cart, CartLineViewModel cartline)
        {
            if (cart.Any(sd => sd.ProductId == cartline.ProductId))
            {
                //Daha önceden sepette olan bir ürün sepete eklenmişse;
                foreach (CartLineViewModel sd in cart)
                {
                    if (sd.ProductId == cartline.ProductId && _productService.GetById(sd.ProductId).Stock>sd.Quantity)
                    {
                        sd.Quantity += cartline.Quantity;  //ürünün miktarını artır.
                        sd.TotalPrice = cartline.TotalPrice;
                    }

                }
            }
            else
            {
                cart.Add(cartline);  //yeni siparişi sepete ekler.
            }
            return cart;
        }


        //sepetten bir ürün silmek için
        public List<CartLineViewModel> DeleteInCart(List<CartLineViewModel> cart, int id)   
        {
            cart.RemoveAll(s => s.ProductId == id);
            return cart;
        }

        //sepetin toplam tutarı
        public decimal TotalPrice(List<CartLineViewModel> cart)
        {
            var totalPrice = cart.Sum(s => s.Quantity * s.UnitPrice);
            return totalPrice;
        }
        //sepetin toplam adeti
        public int TotalQuantity(List<CartLineViewModel> cart)
        {
            var totalQuantity = cart.Sum(s => s.Quantity);
            return totalQuantity;
        }
        //sepete yeni bir ürün ekleme
        public void CreateCartLine(List<CartLineViewModel> model, int cartId)
        {          
            List<CartLine> cartlines = new List<CartLine>();
            foreach (var item in model)
            {
                item.CartId = cartId;
                item.OrderDate=DateTime.Now;
            }
            cartlines = _mapper.Map<List<CartLine>>(model);
            foreach (var item in cartlines)
            {
                _unitOfWork.GetRepository<CartLine>().AddNoAsync(item);
            }



            _unitOfWork.Commit();
        }
        //sepetteki ürünleri tek tek bulma
        public async Task<IEnumerable<CartLineViewModel>> GetCartLineByCartId(int id)
        {
            IEnumerable<CartLine> cartLines= await _unitOfWork.GetRepository<CartLine>().GetAll(x => x.CartId == id,null,x=>x.Product);
            return _mapper.Map<IEnumerable<CartLineViewModel>>(cartLines);
        }

        //sepetteki ürünleri güncelleme
        public async Task UpdateCartLineStatus(int id)
        {
            CartLine cartline = await _unitOfWork.GetRepository<CartLine>().GetByIdAsync(x => x.Id == id);
            cartline.Status = true;
            cartline.DeliveryDate = DateTime.Now;
            _unitOfWork.GetRepository<CartLine>().Update(cartline);
            _unitOfWork.Commit();
            IEnumerable<CartLine> newcartline = await _unitOfWork.GetRepository<CartLine>().GetAll(x => x.CartId == cartline.CartId&&x.Status==false);
            if (newcartline.Count()==0)
            {
              await  _cartService.UpdateCartStatusById(cartline.CartId);
            }

        }
    }

}
